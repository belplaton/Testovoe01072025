using UnityEngine;
using waterb.UI.Window;
using Zenject;

namespace waterb.UI.TabNavigation
{
	public abstract class TabPresenterCreator<TTabPresenter, TModel, TView, TViewFactory> : PlaceholderFactory<TTabPresenter>
		where TTabPresenter : AbstractTabPresenter<TModel, TView, TViewFactory>
		where TView : Object, IWindow
		where TViewFactory : WindowManager.Factory<TView>
	{
		
	}
}