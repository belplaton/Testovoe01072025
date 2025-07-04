using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using waterb.UI.Window;

namespace waterb.Features.Tab.Breeds.View
{
    public sealed class BreedsTabView : MonoBehaviour, IWindow
    {
        [Header("Breed List")] 
        [SerializeField] private Transform breedsListRoot;
        [SerializeField] private BreedButton breedButtonPrefab;
        [SerializeField] private GameObject loader;

        [Header("Popup fact")]
        [SerializeField] private float maxHeight;
        [SerializeField] private RectTransform factPopup;
        [SerializeField] private TMP_Text factTitle;
        [SerializeField] private TMP_Text factDescription;
        [SerializeField] private Button okButton;
        [SerializeField] private GameObject factLoader;

        public event Action<DogBreed> OnBreedSelected;
        public event Action OnCloseFactPopup;

        private readonly List<BreedButton> _breedButtons = new();

        public event Action<IWindow> OnDestroyEvent;
        private void OnDestroy()
        {
            OnDestroyEvent?.Invoke(this);
        }
        
        public void ShowBreeds(List<DogBreed> breeds)
        {
            ClearBreedsList();
            foreach (var breed in breeds)
            {
                var btnObj = Instantiate(breedButtonPrefab, breedsListRoot);
                if (btnObj.Text != null) btnObj.Text.text = breed.Name;
                btnObj.Button.onClick.AddListener(() => OnBreedSelected?.Invoke(breed));
                _breedButtons.Add(btnObj);
            }
        }

        public void ShowLoader() => loader?.SetActive(true);
        public void HideLoader() => loader?.SetActive(false);

        public void ShowFactLoader() => factLoader?.SetActive(true);
        public void HideFactLoader() => factLoader?.SetActive(false);

        public void ShowFactPopup(BreedFact fact)
        {
            factPopup.gameObject.SetActive(true);
            factTitle.text = fact.Title;
            factDescription.text = fact.Description;
            factDescription.ForceMeshUpdate();
            okButton.onClick.RemoveAllListeners();
            okButton.onClick.AddListener(() => OnCloseFactPopup?.Invoke());
            LayoutRebuilder.ForceRebuildLayoutImmediate(factPopup);
        }

        public void HideFactPopup() => factPopup.gameObject.SetActive(false);

        private void ClearBreedsList()
        {
            foreach (var btn in _breedButtons)
            {
                Destroy(btn);
            }
            
            _breedButtons.Clear();
        }
    }
} 