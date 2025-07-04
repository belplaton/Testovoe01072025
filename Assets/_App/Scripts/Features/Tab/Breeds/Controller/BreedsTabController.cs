using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using waterb.Features.Tab.Breeds.View;
using waterb.Networking;
using Zenject;

namespace waterb.Features.Tab.Breeds.Controller
{
    public sealed class BreedsTabController : AbstractTabController<BreedsTabModel, BreedsTabView, BreedsTabViewCreator>
    {
        [Inject] private NetworkService _networkService;
        private CancellationTokenSource _cts;
        private bool _isActive;

        protected override void OnViewCreated(BreedsTabView view)
        {
            if (view)
            {
                view.OnBreedSelected += OnBreedSelected;
                view.OnCloseFactPopup += OnCloseFactPopup;
            }
        }

        public void OnTabActivated()
        {
            if (!_isActive)
            {
                _isActive = true;
                LoadBreedsAsync().Forget();
            }
        }

        public void OnTabDeactivated()
        {
            if (_isActive)
            {
                _isActive = false;
                CancelRequests();
            }
        }

        private void CancelRequests()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }

        private async UniTaskVoid LoadBreedsAsync()
        {
            CancelRequests();
            _cts = new CancellationTokenSource();
            Model.IsLoading = true;
            View?.ShowLoader();
            var responseChannel = await _networkService.SendAsync(new BreedsRequest());
            _cts.Token.ThrowIfCancellationRequested();
            await foreach (var response in responseChannel.ReadAllAsync())
            {
                if (response is BreedsResponse breedsResponse)
                {
                    Model.Breeds.Clear();
                    foreach (var breed in breedsResponse.Breeds)
                    {
                        Model.Breeds.Add(new DogBreed { Id = breed.Id, Name = breed.Name });
                    }
                    View?.ShowBreeds(Model.Breeds);
                }
            }
            Model.IsLoading = false;
            View?.HideLoader();
        }

        private void OnBreedSelected(DogBreed breed)
        {
            LoadBreedFactAsync(breed).Forget();
        }

        private async UniTaskVoid LoadBreedFactAsync(DogBreed breed)
        {
            CancelRequests();
            _cts = new CancellationTokenSource();
            View?.ShowFactLoader();
            var responseChannel = await _networkService.SendAsync(new BreedFactRequest(breed.Id));
            _cts.Token.ThrowIfCancellationRequested();
            await foreach (var response in responseChannel.ReadAllAsync())
            {
                if (response is BreedFactResponse factResponse)
                {
                    Model.SelectedFact = new BreedFact { Title = factResponse.Title, Description = factResponse.Description };
                    View?.ShowFactPopup(Model.SelectedFact);
                }
            }
            View?.HideFactLoader();
        }

        private void OnCloseFactPopup()
        {
            View?.HideFactPopup();
        }
    }
} 