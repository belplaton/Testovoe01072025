using UnityEngine;

namespace waterb.UI.Window
{
	public static class WindowUtils
	{
		public static void Close<TWindow>(this TWindow window) where TWindow : Component, IWindow
		{
			Object.Destroy(window.gameObject);
		}
	}
}