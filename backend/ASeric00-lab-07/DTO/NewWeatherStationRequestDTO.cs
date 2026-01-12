using System;
using System.Globalization;
using System.Text.Json.Serialization;
using ASeric00_lab_07.Models;

namespace ASeric00_lab_07.DTO
{
	public class NewWeatherStationRequestDTO
	{
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

        public WeatherStation ToModel()
        {
            return new WeatherStation
            (
                this.StationName,
                this.Conditions,
                this.Temperature,
                this.Pressure,
                this.WindSpeed,
                this.WindDirection
            );
        }

        public static NewWeatherStationRequestDTO FromModel(WeatherStation model)
        {
            return new NewWeatherStationRequestDTO
            {
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

