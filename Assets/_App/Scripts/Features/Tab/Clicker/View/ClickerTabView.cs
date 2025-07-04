using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using waterb.UI.Window;

namespace waterb.Features.Tab.Clicker.View
{
	public sealed class ClickerTabView : MonoBehaviour, IWindow
	{
		public event Action<IWindow> OnDestroyEvent;
		
		public Button clickButton;
		public TMP_Text currencyText;
		public TMP_Text energyText;
		public AudioSource audioSource;
		
		public event Action OnClick;

		public void SetCurrency(int value)
		{
			currencyText.text = $"Clicks: {value.ToString()}";
		}

		public void SetEnergy(int value)
		{
			energyText.text = $"Energy: {value.ToString()}";
		}

		private void Awake()
		{
			clickButton.onClick.AddListener(() => OnClick?.Invoke());
		}

		public void PlayClickVFX() { /* TODO */ }

		public void PlayClickSound()
		{
			audioSource.Play();
		}
		public void PlayAutoCollectVFX() { /* TODO */ }

		public void PlayAutoCollectSound()
		{
			audioSource.Play();
		}

		private void OnDestroy()
		{
			OnDestroyEvent?.Invoke(this);
		}
	}
}