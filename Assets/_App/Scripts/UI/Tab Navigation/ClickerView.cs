using System;
using UnityEngine;
using waterb.UI.Window;

namespace waterb.UI.TabNavigation
{
	public sealed class ClickerView : MonoBehaviour, IWindow
	{
		public event Action<IWindow> OnDestroyEvent;
		
		private void OnDestroy()
		{
			OnDestroyEvent?.Invoke(this);
		}
	}
}