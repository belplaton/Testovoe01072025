using UnityEngine;
using Zenject;

namespace waterb.UI.TabNavigation
{
	public sealed class TabNavigationInstaller : MonoInstaller
	{
		[SerializeField] private ClickerTabPresenter _clickerTabPresenter;
		
		public override void InstallBindings()
		{
			Container.BindFactory<ClickerTabPresenter, ClickerTabPresenterFactory>()
				.FromComponentInNewPrefab(_clickerTabPresenter);
		}
	}
}