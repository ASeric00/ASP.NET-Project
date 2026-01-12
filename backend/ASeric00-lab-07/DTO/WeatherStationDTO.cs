using System.Globalization;
using System.Text.Json.Serialization;
using ASeric00_lab_07.Models;

namespace ASeric00_lab_07.DTO
{
    public class WeatherStationDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("timestamp")]
        public string Timestamp { get; set; }

        [JsonPropertyName("stationName")]
        public string? StationName { get; set; }

        [JsonPropertyName("conditions")]
        public string? Conditions { get; set; }

        [JsonPropertyName("temperature")]
        public int Temperature { get; set; }

        [JsonPropertyName("pressure")]
        public int Pressure { get; set; }

        [JsonPropertyName("windSpeed")]
        public int WindSpeed { get; set; }

        [JsonPropertyName("windDirection")]
        public string? WindDirection { get; set; }

        private const string _timestampDisplayFormat = "yyyy-MM-ddTHH:mm:ss.fff";

        public WeatherStation ToModel()
        {
            return new WeatherStation
            (
                this.Id,
                DateTime.ParseExact(Timestamp, _timestampDisplayFormat, CultureInfo.InvariantCulture),
                this.StationName,
                this.Conditions,
                this.Temperature,
                this.Pressure,
                this.WindSpeed,
                this.WindDirection
            );
        }

        public static WeatherStationDTO FromModel(WeatherStation model)
        {
            return new WeatherStationDTO
            {
                Id = model.Id,
                Timestamp = model.Timestamp.ToString(_timestampDisplayFormat),
                StationName = model.StationName,
                Conditions = model.Conditions,
                Temperature = model.Temperature,
                Pressure = model.Pressure,
                WindSpeed = model.WindSpeed,
                WindDirection = model.WindDirection
            };
        }
    }

}

