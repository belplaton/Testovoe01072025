using Zenject;

namespace waterb.Networking
{
    public sealed class NetworkInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<NetworkRequestQueue>().AsSingle();
            Container.Bind<NetworkService>().AsSingle();
        }
    }
} 