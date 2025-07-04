using System;
using UnityEngine;

namespace waterb.Features.Tab.Clicker.Model
{
	[Serializable]
	public sealed class ClickerTabModel
	{
		public int Currency { get; private set; }
		public int Energy { get; private set; }
		public int MaxEnergy { get; }
		public int EnergyPerTick { get; }
		public int EnergyPerClick { get; }
		public int AutoCollectInterval { get; }
		public int EnergyRestoreInterval { get; }
		public int EnergyRestoreAmount { get; }

		public event Action<int> OnCurrencyChanged;
		public event Action<int> OnEnergyChanged;

		public ClickerTabModel(ClickerTabModelConfig modelConfig)
		{
			MaxEnergy = modelConfig.maxEnergy;
			Energy = modelConfig.maxEnergy;
			EnergyPerTick = modelConfig.energyPerTick;
			EnergyPerClick = modelConfig.energyPerClick;
			AutoCollectInterval = modelConfig.autoCollectInterval;
			EnergyRestoreInterval = modelConfig.energyRestoreInterval;
			EnergyRestoreAmount = modelConfig.energyRestoreAmount;
		}

		public bool TryClick()
		{
			if (Energy < EnergyPerClick) return false;
			Energy -= EnergyPerClick;
			Currency++;
			OnCurrencyChanged?.Invoke(Currency);
			OnEnergyChanged?.Invoke(Energy);
			return true;
		}

		public bool TryAutoCollect()
		{
			if (Energy < EnergyPerTick) return false;
			Energy -= EnergyPerTick;
			Currency++;
			OnCurrencyChanged?.Invoke(Currency);
			OnEnergyChanged?.Invoke(Energy);
			return true;
		}

		public void RestoreEnergy()
		{
			int prev = Energy;
			Energy = Mathf.Min(Energy + EnergyRestoreAmount, MaxEnergy);
			if (Energy != prev)
				OnEnergyChanged?.Invoke(Energy);
		}
	}
}