namespace waterb.Networking
{
    public sealed class WeatherResponse : IResponse
    {
        public string Name { get; set; }
        public string Temperature { get; set; }
        public string Icon { get; set; }
    }
} 