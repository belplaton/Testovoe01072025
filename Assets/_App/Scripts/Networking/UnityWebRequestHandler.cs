using System.Threading;
using Cysharp.Threading.Tasks;

namespace waterb.Networking
{
    public abstract class UnityWebRequestHandler<TRequest, TResponse> : INetworkRequestHandler<TRequest, TResponse>
        where TRequest : IRequest
        where TResponse : IResponse
    {
        public bool CanHandle(IRequest request) => request is TRequest typeRequest && CanHandle(typeRequest);
        public virtual bool CanHandle(TRequest request)
        {
            return true;
        }

        public UniTask<IResponse> HandleAsync(IRequest request, CancellationToken token)
        {
            if (request is not TRequest typedRequest)
            {
                throw new InvalidRequestTypeException($"Can`t cast {request} to {nameof(TRequest)}.");
            }

            return HandleAsync(typedRequest, token).ContinueWith(x => (IResponse)x);;
        }

        public UniTask<TResponse> HandleAsync(TRequest request, CancellationToken token)
        {
            return HandleRequestAsync(request, token);
        }
        
        protected abstract UniTask<TResponse> HandleRequestAsync(TRequest request, CancellationToken token);
    } 
}