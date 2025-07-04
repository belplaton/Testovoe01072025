using UnityEngine;
using Zenject;
using waterb.Features.Tab.Breeds.Controller;
using waterb.Features.Tab.Breeds.View;
using waterb.Features.Tab.Breeds;

namespace waterb.Features.Tab.Breeds
{
    public sealed class BreedsTabInstaller : MonoInstaller
    {
        [SerializeField] private BreedsTabController _breedsTabControllerPrefab;
        [SerializeField] private BreedsTabView _breedsTabViewPrefab;

        public override void InstallBindings()
        {
            Container.Bind<BreedsTabModel>()
                .FromNew().AsSingle().NonLazy();
            Container.BindFactory<BreedsTabController, BreedsTabControllerCreator>()
                .FromComponentInNewPrefab(_breedsTabControllerPrefab).AsSingle().NonLazy();
            Container.BindFactory<BreedsTabView, BreedsTabViewCreator>()
                .FromComponentInNewPrefab(_breedsTabViewPrefab).AsSingle().NonLazy();
        }
    }
} 