using UnityEngine;
using waterb.Features.Tab.Clicker.Controller;
using Zenject;

namespace waterb.Features.Tab
{
	public sealed class TabNavigationInstaller : MonoInstaller
	{
		[SerializeField] private ClickerTabController _clickerTabController;
		
		public override void InstallBindings()
		{
			Container.BindFactory<ClickerTabController, ClickerTabControllerFactory>()
				.FromComponentInNewPrefab(_clickerTabController);
		}
	}
}