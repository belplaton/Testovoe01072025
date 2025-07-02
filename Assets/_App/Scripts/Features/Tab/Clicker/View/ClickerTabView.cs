using System;
using UnityEngine;
using waterb.UI.Window;

namespace waterb.Features.Tab.Clicker.View
{
	public sealed class ClickerTabView : MonoBehaviour, IWindow
	{
		public event Action<IWindow> OnDestroyEvent;
		
		private void OnDestroy()
		{
			OnDestroyEvent?.Invoke(this);
		}
	}
}