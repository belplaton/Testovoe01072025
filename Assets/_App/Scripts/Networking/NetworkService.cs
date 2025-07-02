using Cysharp.Threading.Tasks;
using UnityEngine;

namespace waterb.Networking
{
    public sealed class NetworkService
    {
        private readonly NetworkRequestQueue _queue;

        public NetworkService(NetworkRequestQueue queue)
        {
            _queue = queue;
            Debug.Log("NetworkService instantiated.");
        }

        public UniTask<ChannelReader<IResponse>> SendAsync(IRequest request)
        {
            return _queue.EnqueueAsync(request);
        }

        public void CancelAllRequests()
        {
            _queue.CancelAll();
        }
    }
} 