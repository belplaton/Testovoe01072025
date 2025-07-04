using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using waterb.UI.Window;

namespace waterb.Features.Tab.Weather.View
{
    public sealed class WeatherTabView : MonoBehaviour, IWindow
    {
        public event Action<IWindow> OnDestroyEvent;
        public TMP_Text weatherText;
        public Image weatherIcon;

        private Sprite _iconToSet;

        private void LateUpdate()
        {
            if (_iconToSet)
            {
                weatherIcon.sprite = _iconToSet;
                _iconToSet = null;
            }
        }

        public void SetWeather(string weatherName, string temperature, Sprite icon)
        {
            weatherText.text = $"Сегодня - {temperature}";
            _iconToSet = icon;
        }
        
        private void OnDestroy()
        {
            OnDestroyEvent?.Invoke(this);
        }
    }
} 