using UnityEngine;
using waterb.Features.Tab.Clicker.Controller;
using waterb.Features.Tab.Clicker.Model;
using waterb.Features.Tab.Clicker.View;
using Zenject;

namespace waterb.Features.Tab.Clicker
{
	public sealed class ClickerTabInstaller : MonoInstaller
	{
		[SerializeField] private ClickerTabController _clickerTabController;
		[SerializeField] private ClickerTabModelConfig _clickerTabModelConfig;
		[SerializeField] private ClickerTabView _clickerTabViewPrefab;

		public override void InstallBindings()
		{
			Container.Bind<ClickerTabModel>()
				.FromNew().AsSingle().WithArguments(_clickerTabModelConfig).NonLazy();
			Container.BindFactory<ClickerTabView, ClickerTabViewCreator>()
				.FromComponentInNewPrefab(_clickerTabViewPrefab).AsSingle().NonLazy();
			Container.Bind<ClickerTabController>()
				.FromInstance(_clickerTabController).AsSingle().NonLazy();
			Container.Inject(_clickerTabController);
		}
	}
}