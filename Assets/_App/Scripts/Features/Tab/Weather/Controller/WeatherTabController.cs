using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using waterb.Features.Tab.Weather.Model;
using waterb.Features.Tab.Weather.View;
using waterb.Networking;
using Zenject;

namespace waterb.Features.Tab.Weather.Controller
{
    public sealed class WeatherTabController : AbstractTabController<WeatherTabModel, WeatherTabView, WeatherTabViewCreator>
    {
        [SerializeField] private float requestInterval = 5f;
        
        [Inject] private NetworkService _networkService;
        private CancellationTokenSource _weatherCts;
        private bool _isActive;

        private void Start()
        {
            Model.OnWeatherChanged += UpdateView;
        }

        private void OnDestroy()
        {
            Model.OnWeatherChanged -= UpdateView;
            CancelWeatherRequest();
        }

        protected override void OnViewCreated(WeatherTabView view)
        {
            if (view)
            {
                UpdateView();
            }
        }

        public void OnTabActivated()
        {
            _isActive = true;
            StartWeatherRequests();
        }

        public void OnTabDeactivated()
        {
            _isActive = false;
            CancelWeatherRequest();
        }

        private void StartWeatherRequests()
        {
            CancelWeatherRequest();
            _weatherCts = new CancellationTokenSource();
            LoopWeatherRequest(_weatherCts.Token).Forget();
        }

        private void CancelWeatherRequest()
        {
            _weatherCts?.Cancel();
            _weatherCts?.Dispose();
            _weatherCts = null;
        }

        private async UniTaskVoid LoopWeatherRequest(CancellationToken cancellationToken)
        {
            while (_isActive && !cancellationToken.IsCancellationRequested)
            {
                await RequestWeatherAsync(cancellationToken);
                await UniTask.Delay(TimeSpan.FromSeconds(requestInterval), cancellationToken: cancellationToken);
            }
        }
        
        private async UniTask RequestWeatherAsync(CancellationToken cancellationToken)
        {
            var responseChannel = await _networkService.SendAsync(new WeatherRequest());
            cancellationToken.ThrowIfCancellationRequested();
            await foreach (var response in responseChannel.ReadAllAsync())
            {
                if (response is WeatherResponse weather)
                {
                    Model.SetWeather(weather.Name, weather.Temperature, weather.Icon);
                }
            }
        }

        private void UpdateView()
        {
            if (View != null)
            {
                View.SetWeather(Model.Name, Model.Temperature, null);
                if (!string.IsNullOrEmpty(Model.Icon))
                {
                    LoadSpriteFromUrlAsync(Model.Icon, destroyCancellationToken).Forget();
                }
            }
        }

        private async UniTaskVoid LoadSpriteFromUrlAsync(string url, CancellationToken cancellationToken)
        {
            using (var uwr = UnityEngine.Networking.UnityWebRequestTexture.GetTexture(url))
            {
                await uwr.SendWebRequest();
                cancellationToken.ThrowIfCancellationRequested();
                if (uwr.result == UnityEngine.Networking.UnityWebRequest.Result.Success)
                {
                    var texture = ((UnityEngine.Networking.DownloadHandlerTexture)uwr.downloadHandler).texture;
                    var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                    View?.SetWeather(Model.Name, Model.Temperature, sprite);
                }
            }
        }
    }
} 