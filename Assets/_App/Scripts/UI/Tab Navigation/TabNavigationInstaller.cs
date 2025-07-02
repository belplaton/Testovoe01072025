using UnityEngine;
using Zenject;

namespace waterb.UI.TabNavigation
{
	public sealed class TabNavigationInstaller : MonoInstaller
	{
		[SerializeField] private ClickerView _clickerViewPrefab;

		public override void InstallBindings()
		{
			Container.BindFactory<ClickerView, ClickerViewCreator>().FromComponentInNewPrefab(_clickerViewPrefab);
		}
	}
}