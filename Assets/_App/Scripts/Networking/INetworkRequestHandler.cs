using System.Threading;
using Cysharp.Threading.Tasks;

namespace waterb.Networking
{
    public interface INetworkRequestHandler
    {
        public bool CanHandle(IRequest request);
        public UniTask<IResponse> HandleAsync(IRequest request, CancellationToken token);
    }

    public interface INetworkRequestHandler<in TRequest, TResponse> : INetworkRequestHandler
        where TRequest : IRequest
        where TResponse : IResponse
    {
        public bool CanHandle(TRequest request);
        public UniTask<TResponse> HandleAsync(TRequest request, CancellationToken token);
    }
} 