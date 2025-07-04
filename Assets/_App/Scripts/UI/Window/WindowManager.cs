using System.Collections.Generic;
using TTR24.Structures.Collections;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace waterb.UI.Window
{
    public sealed class WindowManager : MonoBehaviour
    {
        public abstract class Factory<TWindow> : PlaceholderFactory<TWindow> where TWindow : Component, IWindow
        {
            [Inject] private WindowManager _windowManager;
            
            public override TWindow Create()
            {
                var window = CreateInternal(new List<TypeValuePair>());
                _windowManager.ActiveWindows.Add(window);
                return window;
            }
        }
        
        private IndexedSet<IWindow> _activeWindows;
        private IndexedSet<IWindow> ActiveWindows
        {
            get
            {
                if (_activeWindows == null)
                {
                    _activeWindows = new IndexedSet<IWindow>();
                    _activeWindows.OnChange += OnActiveWindowsChange;
                }

                return _activeWindows;
            }
        }

        public IReadOnlyIndexedSet<IWindow> ReadOnlyActiveWindows => ActiveWindows;

        private void OnActiveWindowsChange(IndexedSetOperation op, IWindow value)
        {
            if (op is IndexedSetOperation.Add)
            {
                value.OnDestroyEvent += OnWindowDestroy;
            }

            if (op is IndexedSetOperation.Remove)
            {
                value.OnDestroyEvent -= OnWindowDestroy;
            }
        }

        private void OnWindowDestroy(IWindow window)
        {
            ActiveWindows.Remove(window);
        }
        
        public void CloseAll()
        {
            while (ActiveWindows.Count > 0)
            {
                var window = ActiveWindows.GetByIndex(0);
                ActiveWindows.RemoveByIndex(0);
                Destroy(((Component)window).gameObject);
            }
        }

        private void OnDestroy()
        {
            CloseAll();
        }
    }
} 