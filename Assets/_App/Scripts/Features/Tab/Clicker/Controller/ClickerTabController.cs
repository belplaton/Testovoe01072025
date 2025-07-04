using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using waterb.Features.Tab.Clicker.Model;
using waterb.Features.Tab.Clicker.View;

namespace waterb.Features.Tab.Clicker.Controller
{
	public sealed class ClickerTabController : AbstractTabController<
		ClickerTabModel, ClickerTabView, ClickerTabViewCreator>
	{
		private void Start()
		{
			Model.OnCurrencyChanged += HandleCurrencyChanged;
			Model.OnEnergyChanged += HandleEnergyChanged;
			if (View != null)
			{
				View.OnClick += OnClick;
				View.SetCurrency(Model.Currency);
				View.SetEnergy(Model.Energy);
			}

			RunAutoCollect(destroyCancellationToken).Forget();
			RunEnergyRestore(destroyCancellationToken).Forget();
		}

		private void OnDestroy()
		{
			Model.OnCurrencyChanged -= HandleCurrencyChanged;
			Model.OnEnergyChanged -= HandleEnergyChanged;
			if (View != null)
			{
				View.OnClick -= OnClick;
			}
		}

		private void OnClick()
		{
			if (Model.TryClick())
			{
				View?.PlayClickVFX();
				View?.PlayClickSound();
			}
		}

		protected override void OnViewCreated(ClickerTabView view)
		{
			if (view)
			{
				view.OnClick += OnClick;
				view.SetCurrency(Model.Currency);
				view.SetEnergy(Model.Energy);
			}
		}

		private void HandleCurrencyChanged(int value) => View?.SetCurrency(value);
		private void HandleEnergyChanged(int value) => View?.SetEnergy(value);

		private async UniTaskVoid RunAutoCollect(CancellationToken cancellationToken)
		{
			while (!cancellationToken.IsCancellationRequested)
			{
				await UniTask.Delay(TimeSpan.FromSeconds(Model.AutoCollectInterval), cancellationToken: cancellationToken);
				if (Model.TryAutoCollect())
				{
					View?.PlayAutoCollectVFX();
					View?.PlayAutoCollectSound();
				}
			}
		}

		private async UniTaskVoid RunEnergyRestore(CancellationToken cancellationToken)
		{
			while (!cancellationToken.IsCancellationRequested)
			{
				await UniTask.Delay(TimeSpan.FromSeconds(Model.EnergyRestoreInterval), cancellationToken: cancellationToken);
				Model.RestoreEnergy();
			}
		}
	}
}