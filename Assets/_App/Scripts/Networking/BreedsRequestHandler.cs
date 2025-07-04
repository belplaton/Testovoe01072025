using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace waterb.Networking
{
    public sealed class BreedsRequestHandler : UnityWebRequestHandler<BreedsRequest, BreedsResponse>
    {
        private const string ApiUrl = "https://dogapi.dog/api/v2/breeds";

        [System.Serializable]
        private class BreedsApiResponse
        {
            public List<BreedData> data;
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
        }

        protected override async UniTask<BreedsResponse> HandleRequestAsync(BreedsRequest request, CancellationToken token)
        {
            using var webRequest = UnityWebRequest.Get(ApiUrl);
            var op = await webRequest.SendWebRequest().WithCancellation(token);
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                return new BreedsResponse { Breeds = new List<DogBreedDto>() };
            }
            var json = webRequest.downloadHandler.text;
            try
            {
                var data = JsonUtility.FromJson<BreedsApiResponse>(json);
                if (data?.data != null)
                {
                    var breeds = data.data.Take(10).Select(b => new DogBreedDto { Id = b.id, Name = b.attributes.name }).ToList();
                    return new BreedsResponse { Breeds = breeds };
                }
            }
            catch { }
            return new BreedsResponse { Breeds = new List<DogBreedDto>() };
        }
    }
} 