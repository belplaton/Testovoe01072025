using UnityEngine;
using waterb.Features.Tab.Controller;
using waterb.Features.Tab.View;
using waterb.Features.Tab.Clicker.Controller;
using waterb.Features.Tab.Weather.Controller;
using waterb.Features.Tab.Breeds.Controller;
using Zenject;

namespace waterb.Features.Tab
{
	public sealed class TabNavigationInstaller : MonoInstaller
	{
		[SerializeField] private TabNavigationController _tabNavigationController;
		[SerializeField] private TabNavigationView _tabNavigationViewPrefab;
		[SerializeField] private ClickerTabController _clickerTabControllerPrefab;
		[SerializeField] private WeatherTabController _weatherTabControllerPrefab;
		[SerializeField] private BreedsTabController _breedsTabControllerPrefab;
		
		public override void InstallBindings()
		{
			Container.Bind<TabNavigationController>()
				.FromInstance(_tabNavigationController).AsSingle().NonLazy();
			Container.Bind<TabNavigationView>()
				.FromComponentInNewPrefab(_tabNavigationViewPrefab).AsSingle().NonLazy();
			Container.BindFactory<ClickerTabController, ClickerTabControllerCreator>()
				.FromComponentInNewPrefab(_clickerTabControllerPrefab).AsSingle().NonLazy();
			Container.BindFactory<WeatherTabController, WeatherTabControllerCreator>()
				.FromComponentInNewPrefab(_weatherTabControllerPrefab).AsSingle().NonLazy();
			Container.BindFactory<BreedsTabController, BreedsTabControllerCreator>()
				.FromComponentInNewPrefab(_breedsTabControllerPrefab).AsSingle().NonLazy();
			
			Container.Inject(_tabNavigationController);
		}
	}
}