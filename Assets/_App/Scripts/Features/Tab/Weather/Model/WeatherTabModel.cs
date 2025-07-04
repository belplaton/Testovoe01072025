using System;

namespace waterb.Features.Tab.Weather.Model
{
    public sealed class WeatherTabModel
    {
        public string Name { get; private set; }
        public string Temperature { get; private set; }
        public string Icon { get; private set; }
        public event Action OnWeatherChanged;

        public void SetWeather(string name, string temperature, string icon)
        {
            Name = name;
            Temperature = temperature;
            Icon = icon;
            OnWeatherChanged?.Invoke();
        }
    }
} 