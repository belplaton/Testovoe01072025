using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace waterb.Networking
{
    public sealed class NetworkRequestQueue : IDisposable
    {
        private readonly Queue<(IRequest, UniTaskCompletionSource<ChannelReader<IResponse>>, CancellationTokenSource)> _queue = new();
        private readonly Dictionary<Type, List<INetworkRequestHandler>> _handlerInstances = new();
        private readonly CancellationTokenSource _cts = new();
        private bool _isProcessing;

        public NetworkRequestQueue()
        {
            for (var i = 0; i < NetworkHandlerRegistry.HandlersInfoArray.Count; i++)
            {
                var info = NetworkHandlerRegistry.HandlersInfoArray[i];
                var handler = (INetworkRequestHandler)Activator.CreateInstance(info.HandlerType);
                if (!_handlerInstances.TryGetValue(info.RequestType, out var list))
                {
                    _handlerInstances[info.RequestType] = list = new List<INetworkRequestHandler>();
                }

                list.Add(handler);
            }
        }

        ~NetworkRequestQueue()
        {
            ReleaseUnmanagedResources();
        }

        internal UniTask<ChannelReader<IResponse>> EnqueueAsync(IRequest request)
        {
            var tcs = new UniTaskCompletionSource<ChannelReader<IResponse>>();
            var cts = new CancellationTokenSource();
            _queue.Enqueue((request, tcs, cts));
            ProcessQueue(_cts.Token).Forget();
            return tcs.Task;
        }

        internal void CancelAll()
        {
            while (_queue.Count > 0)
            {
                var (_, tcs, cts) = _queue.Dequeue();
                cts?.Cancel();
                cts?.Dispose();
                tcs.TrySetCanceled();
            }
        }

        private async UniTaskVoid ProcessQueue(CancellationToken cancellationToken)
        {
            if (_isProcessing) return;
            _isProcessing = true;

            while (_queue.Count > 0)
            {
                cancellationToken.ThrowIfCancellationRequested();
                var (request, tcs, cts) = _queue.Dequeue();
                var channel = Channel.CreateSingleConsumerUnbounded<IResponse>();
                try
                {
                    var requestType = request.GetType();
                    if (!_handlerInstances.TryGetValue(requestType, out var handlers) || handlers.Count == 0)
                    {
                        channel.Writer.TryComplete();
                        tcs.TrySetException(new Exception($"No handlers instances found for {requestType.Name}"));
                        continue;
                    }

                    var responsesTasks = UniTask.WhenEach(handlers.Select(handler =>
                    {
                        if (handler.CanHandle(request))
                        {
                            return handler.HandleAsync(request, cts.Token);
                        }

                        return new UniTask<IResponse>();
                    }));

                    await foreach (var response in responsesTasks)
                    {
                        if (response is { IsCompletedSuccessfully: true, Result: not null })
                        {
                            channel.Writer.TryWrite(response.Result);
                        }
                    }
                    
                    channel.Writer.TryComplete();
                    tcs.TrySetResult(channel.Reader);
                }
                catch (Exception ex)
                {
                    channel.Writer.TryComplete(ex);
                    tcs.TrySetException(ex);
                }
            }

            _isProcessing = false;
        }

        private void ReleaseUnmanagedResources()
        {
            CancelAll();
            _cts.Cancel();
            _cts.Dispose();
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }
    }
} 