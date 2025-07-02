using UnityEngine;
using Zenject;

namespace waterb.UI.Window
{
    public class GlobalUIInstaller : MonoInstaller
    {
        [SerializeField] private WindowManager _windowManager;

        public override void InstallBindings()
        {
            Container.Bind<WindowManager>().FromInstance(_windowManager).AsSingle();
        }
    }
} 