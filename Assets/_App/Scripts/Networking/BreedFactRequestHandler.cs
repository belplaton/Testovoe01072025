using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace waterb.Networking
{
    public sealed class BreedFactRequestHandler : UnityWebRequestHandler<BreedFactRequest, BreedFactResponse>
    {
        protected override async UniTask<BreedFactResponse> HandleRequestAsync(BreedFactRequest request, CancellationToken token)
        {
            var url = $"https://dogapi.dog/api/v2/breeds/{request.BreedId}";
            using var webRequest = UnityWebRequest.Get(url);
            var op = await webRequest.SendWebRequest().WithCancellation(token);
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                return new BreedFactResponse { Title = "Error", Description = "Failed to load breed info." };
            }
            var json = webRequest.downloadHandler.text;
            try
            {
                var data = JsonUtility.FromJson<BreedApiResponse>(json);
                if (data?.data?.attributes != null)
                {
                    return new BreedFactResponse
                    {
                        Title = data.data.attributes.name,
                        Description = data.data.attributes.description
                    };
                }
            }
            catch { }
            return new BreedFactResponse { Title = "N/A", Description = "No info." };
        }

        [System.Serializable]
        private class BreedApiResponse
        {
            public BreedData data;
        }
        [System.Serializable]
        private class BreedData
        {
            public string id;
            public BreedAttributes attributes;
        }
        [System.Serializable]
        private class BreedAttributes
        {
            public string name;
            public string description;
        }
    }
} 