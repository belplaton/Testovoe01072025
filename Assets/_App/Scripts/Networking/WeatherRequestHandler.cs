using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;
using UnityEngine;

namespace waterb.Networking
{
    public sealed class WeatherRequestHandler : UnityWebRequestHandler<WeatherRequest, WeatherResponse>
    {
        private const string ApiUrl = "https://api.weather.gov/gridpoints/TOP/32,81/forecast";

        [System.Serializable]
        private class WeatherApiResponse
        {
            public Properties properties;
        }
        [System.Serializable]
        private class Properties
        {
            public Period[] periods;
        }
        [System.Serializable]
        private class Period
        {
            public string name;
            public string temperature;
            public string shortForecast;
            public string icon;
        }

        protected override async UniTask<WeatherResponse> HandleRequestAsync(WeatherRequest request, CancellationToken token)
        {
            using var webRequest = UnityWebRequest.Get(ApiUrl);
            var op = await webRequest.SendWebRequest().WithCancellation(token);
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                return new WeatherResponse { Name = "Error", Temperature = "--", Icon = null };
            }
            var json = webRequest.downloadHandler.text;
            try
            {
                var data = JsonUtility.FromJson<WeatherApiResponse>(json);
                if (data?.properties?.periods != null && data.properties.periods.Length > 0)
                {
                    var today = data.properties.periods[0];
                    return new WeatherResponse
                    {
                        Name = today.name,
                        Temperature = today.temperature + "F",
                        Icon = today.icon
                    };
                }
            }
            catch { }
            return new WeatherResponse { Name = "N/A", Temperature = "--", Icon = null };
        }
    }
} 