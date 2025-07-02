using UnityEngine;
using waterb.Features.Tab.Clicker.View;
using Zenject;

namespace waterb.Features.Tab.Clicker
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