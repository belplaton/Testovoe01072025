using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace waterb.UI.Window
{
    public sealed class GlobalUIInstaller : MonoInstaller
    {
        [SerializeField] private EventSystem _eventSystem;
        [SerializeField] private WindowManager _windowManager;

        public override void InstallBindings()
        {
            Container.Bind<EventSystem>().FromInstance(_eventSystem).AsSingle().NonLazy();
            Container.Bind<WindowManager>().FromInstance(_windowManager).AsSingle().NonLazy();
        }
    }
} 