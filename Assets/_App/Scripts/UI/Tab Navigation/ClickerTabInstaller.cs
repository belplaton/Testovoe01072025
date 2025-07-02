using UnityEngine;
using Zenject;

namespace waterb.UI.TabNavigation
{
	public sealed class ClickerTabInstaller : MonoInstaller
	{
		[SerializeField] private ClickerTabView _clickerTabViewPrefab;

		public override void InstallBindings()
		{
			Container.BindFactory<ClickerTabView, ClickerTabViewCreator>().FromComponentInNewPrefab(_clickerTabViewPrefab);
		}
	}
}