using UnityEngine;
using waterb.UI.Window;
using Zenject;

namespace waterb.Features.Tab
{
	public abstract class TabControllerCreator<TTabController, TModel, TView, TViewFactory> : PlaceholderFactory<TTabController>
		where TTabController : AbstractTabController<TModel, TView, TViewFactory>
		where TView : Component, IWindow
		where TViewFactory : WindowManager.Factory<TView>
	{
		
	}
}