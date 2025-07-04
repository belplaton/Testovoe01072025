using UnityEngine;

namespace waterb.Features.Tab
{
	public interface ITabController
	{
		public void ShowView(Transform parent);
		public void HideView();
	}
}