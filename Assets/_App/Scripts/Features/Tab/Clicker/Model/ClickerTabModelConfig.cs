using UnityEngine;

namespace waterb.Features.Tab.Clicker.Model
{
	[CreateAssetMenu(fileName = "ClickerTabConfig", menuName = "Configs/ClickerTabConfig")]
	public sealed class ClickerTabModelConfig : ScriptableObject
	{
		public int maxEnergy = 1000;
		public int energyPerTick = 1;
		public int energyPerClick = 1;
		public int autoCollectInterval = 3;
		public int energyRestoreInterval = 10;
		public int energyRestoreAmount = 10;
	}
}