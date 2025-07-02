using UnityEngine;

namespace waterb.UI.Window
{
	public static class WindowUtils
	{
		public static void Close<TWindow>(this TWindow window) where TWindow : Object, IWindow
		{
			Object.Destroy(window);
		}
	}
}