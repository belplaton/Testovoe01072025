using UnityEngine;
using Zenject;

namespace waterb.Networking
{
    public sealed class NetworkInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<NetworkRequestQueue>().FromNew().AsSingle().NonLazy();
            Container.Bind<NetworkService>().FromNew().AsSingle().NonLazy();
            Debug.Log("aboba");
        }
    }
} 