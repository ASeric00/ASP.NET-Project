using Microsoft.Extensions.Options;
using ASeric00_lab_07.Models;
using ASeric00_lab_07.Repositories;
using ASeric00_lab_07.Configuration;
using ASeric00_lab_07.Exceptions;

namespace ASeric00_lab_07.Logic
{
	public class WeatherStationLogic : IWeatherStationLogic
	{
		private IWeatherRepository _weatherRepository;
        private readonly ValidationConfiguration _validationConfiguration;

		public WeatherStationLogic(IWeatherRepository weatherRepository, IOptions<ValidationConfiguration> validationConfiguration)
		{
			_weatherRepository = weatherRepository;
            _validationConfiguration = validationConfiguration.Value;
		}

        // POMOCNE FUNKCIJE ZA DEFINIRANJE LOGIKE PROGRAMA
        private bool IsNotPrintableString(string str)
        {
            return str.Any(character => char.IsControl(character));
        }

        private void ValidateWeatherStationInfo(WeatherStation weatherStation)
        {
            if (weatherStation == null)
            {
                throw new WeatherStationAppException_UserError("Weather station Info cannot be null.");
            }
            else if (IsNotPrintableString(weatherStation.StationName))
            {
                throw new WeatherStationAppException_UserError("Weather station name contains invalid characters.");
            }
            else if (weatherStation.StationName.Length == 0)
            {
                throw new WeatherStationAppException_UserError("Weather station name cannot be empty.");
            }
            else if (!IsConditionsFieldValid(weatherStation.Conditions))
            {
                throw new WeatherStationAppException_UserError($"Conditions field cannot contain incorrect values.");
            }
            else if (weatherStation.Temperature < _validationConfiguration.MinTemperatureValue
                || weatherStation.Temperature > _validationConfiguration.MaxTemperatureValue)
            {
                throw new WeatherStationAppException_UserError($"Temperature field cannot go below " +
                    $"{_validationConfiguration.MinTemperatureValue} or above {_validationConfiguration.MaxTemperatureValue}.");
            }
            else if (weatherStation.Pressure < _validationConfiguration.MinPressureValue
                || weatherStation.Pressure > _validationConfiguration.MaxPressureValue)
            {
                throw new WeatherStationAppException_UserError($"Pressure field cannot go below " +
                    $"{_validationConfiguration.MinPressureValue} or above {_validationConfiguration.MaxPressureValue}.");
            }
            else if (weatherStation.WindSpeed < _validationConfiguration.MinWindSpeedValue
                || weatherStation.WindSpeed > _validationConfiguration.MaxWindSpeedValue)
            {
                throw new WeatherStationAppException_UserError($"Wind speed field cannot go below " +
                    $"{_validationConfiguration.MinWindSpeedValue} or above {_validationConfiguration.MaxWindSpeedValue}.");
            }
            else if (!IsWindDirectionValid(weatherStation.WindDirection))
            {
                throw new WeatherStationAppException_UserError($"Wind direction field cannot contain incorrect values.");

            }
        }

        private bool IsConditionsFieldValid(string inputCondition)
        {
            string matchedCondition = _validationConfiguration.possibleConditions.FirstOrDefault(condition =>
                condition.Equals(inputCondition, StringComparison.OrdinalIgnoreCase));

            if (inputCondition.Length == 0)
            {
                return false;
            }
            else if (matchedCondition != null)
            {
                return true;
            }

            return false;
        }

        private bool IsWindDirectionValid(string windDirection)
        {
            string matchedDirection = _validationConfiguration.possibleWindDirections.FirstOrDefault(direction =>
                direction.Equals(windDirection, StringComparison.OrdinalIgnoreCase));

            if (windDirection.Length == 0)
            {
                return false;
            }
            else if (matchedDirection != null)
            {
                return true;
            }

            return false;
        }


        // ####################

        //GET INFO FROM ALL STATIONS
        public IEnumerable<WeatherStation> GetAllStations()
        {
            return _weatherRepository.GetAllStations();
        }

        //GET INFO FROM ONE STATION BY ITS NAME
        public IEnumerable<WeatherStation> GetOneStationByName(string stationName)
        {
            return _weatherRepository.GetOneStationByName(stationName);
        }

        //GET WEATHER RECORD BY ID
        public WeatherStation GetRecordById(int id)
        {
            return _weatherRepository.GetRecordById(id);
        }

        //POST
        public IEnumerable<WeatherStation> AddNewStation(WeatherStation newStation)
        {
            ValidateWeatherStationInfo(newStation);
            newStation.Timestamp = DateTime.Now;
            return _weatherRepository.AddNewStation(newStation);
        }

        //DELETE
        public IEnumerable<WeatherStation> DeleteAStation(int id)
        {
            if (id <= 0)
            {
                throw new WeatherStationAppException_UserError("Id field must be number above 0.");
            }

            return _weatherRepository.DeleteAStation(id);
        }

        //UPDATE
        public IEnumerable<WeatherStation> UpdateAStation(int id, WeatherStation newInfo)
        {
            ValidateWeatherStationInfo(newInfo);
            newInfo.Timestamp = DateTime.Now;
            return _weatherRepository.UpdateAStation(id, newInfo);
        }


    }
}

