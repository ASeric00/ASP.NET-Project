using System.Text.Json.Serialization;

namespace ASeric00_lab_07.Models
{
    public class WeatherStation
    {
        // konstruktor bez parametara
        public WeatherStation()
        {
            Id = 0;
            Timestamp = DateTime.MinValue;
        }

        public WeatherStation(string stationName, string conditions, int temperature, int pressure,
            int windSpeed, string windDirection)
        {
            Id = 0;
            Timestamp = DateTime.MinValue;
            StationName = stationName;
            Conditions = conditions.ToUpper();
            Temperature = temperature;
            Pressure = pressure;
            WindSpeed = windSpeed;
            WindDirection = windDirection.ToUpper();
        }


        public WeatherStation(int id, DateTime timestamp, string stationName, string conditions, int temperature,
            int pressure, int windSpeed, string windDirection)
        {
            Id = id;
            Timestamp = timestamp;
            StationName = stationName;
            Conditions = conditions.ToUpper();
            Temperature = temperature;
            Pressure = pressure;
            WindSpeed = windSpeed;
            WindDirection = windDirection.ToUpper();
        }

        public int Id { get; set; }

        public DateTime Timestamp { get; set; }

        public string StationName { get; set; }

        public string Conditions { get; set; }

        public int Temperature { get; set; }

        public int Pressure { get; set; }

        public int WindSpeed { get; set; }

        public string WindDirection { get; set; }
    }
}
