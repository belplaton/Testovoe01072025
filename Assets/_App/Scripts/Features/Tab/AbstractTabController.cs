using JetBrains.Annotations;
using UnityEngine;
using waterb.UI.Window;
using Zenject;

namespace waterb.Features.Tab
{
	public abstract class AbstractTabController<TModel, TView, TViewFactory> : MonoBehaviour, ITabController
		where TView : Component, IWindow
		where TViewFactory : WindowManager.Factory<TView>
	{
		[Inject] private TModel _model;
		protected TModel Model => _model;
		
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
					OnViewCreated(value);
				}
			}
		}
		
		protected virtual void OnViewCreated(TView next) { }
		public void ShowView(Transform parent) => (View = _viewFactory.Create()).transform.SetParent(parent, false);
		public void HideView() => View = null;
	}
}