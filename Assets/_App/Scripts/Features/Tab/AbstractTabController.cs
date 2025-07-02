using JetBrains.Annotations;
using UnityEngine;
using waterb.UI.Window;
using Zenject;

namespace waterb.Features.Tab
{
	public abstract class AbstractTabController<TModel, TView, TViewFactory> : MonoBehaviour, ITabController
		where TView : Object, IWindow
		where TViewFactory : WindowManager.Factory<TView>
	{
		[Inject] private TViewFactory _viewFactory;

		private TView _view;
		[CanBeNull] protected TView View
		{
			get => _view;
			private set
			{
				if (_view != value)
				{
					_view?.Close();
					_view = value;
				}
			}
		}
        
		public void ShowView() => View = _viewFactory.Create();
		public void HideView() => View = null;
	}
}