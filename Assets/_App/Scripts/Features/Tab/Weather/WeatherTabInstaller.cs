using UnityEngine;
using waterb.Features.Tab.Weather.Controller;
using waterb.Features.Tab.Weather.Model;
using waterb.Features.Tab.Weather.View;
using Zenject;

namespace waterb.Features.Tab.Weather
{
    public sealed class WeatherTabInstaller : MonoInstaller
    {
        [SerializeField] private WeatherTabController _weatherTabController;
        [SerializeField] private WeatherTabView _weatherTabViewPrefab;

        public override void InstallBindings()
        {
            Container.Bind<WeatherTabModel>()
                .FromNew().AsSingle().NonLazy();
            Container.BindFactory<WeatherTabView, WeatherTabViewCreator>()
                .FromComponentInNewPrefab(_weatherTabViewPrefab).AsSingle().NonLazy();
            Container.Bind<WeatherTabController>()
                .FromInstance(_weatherTabController).AsSingle().NonLazy();
            Container.Inject(_weatherTabController);
        }
    }
} 