using Microsoft.AspNetCore.Mvc;
using ASeric00_lab_07.DTO;
using ASeric00_lab_07.Filters;
using ASeric00_lab_07.Models;
using ASeric00_lab_07.Logic;

namespace ASeric00_lab_07.Controllers
{
    //[ErrorFilter] //not necessary because ErrorFilter is registered globally now
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private IWeatherStationLogic _weatherStationLogic;

        public WeatherController(IWeatherStationLogic weatherStationLogic)
        {
            _weatherStationLogic = weatherStationLogic;
        }

        // GET INFO FROM ALL STATIONS
        [HttpGet("all")]
        public IEnumerable<WeatherStationDTO> GetAllStations()
        {
            var stationsList = _weatherStationLogic.GetAllStations();
            return stationsList.Select(x => WeatherStationDTO.FromModel(x));
        }

        // GET INFO FROM ONE STATIONS BY ITS NAME
        [HttpGet("one/{stationName}")]
        public IEnumerable<WeatherStationDTO> GetOneStation([FromRoute] string stationName)
        {
            var stationInfoList = _weatherStationLogic.GetOneStationByName(stationName);

            return stationInfoList.Select(x => WeatherStationDTO.FromModel(x));
        }

        // GET WEATHER RECORD BY ITS ID
        [HttpGet("record/{id}")]
        public WeatherStationDTO GetRecordById([FromRoute] int id)
        {
            var station = _weatherStationLogic.GetRecordById(id);

            if (station == null)
            {
                return null;
            }

            return WeatherStationDTO.FromModel(station);
        }

        //POST
        [HttpPost("new")]
        public IEnumerable<WeatherStationDTO> AddNewStation([FromBody] NewWeatherStationRequestDTO newStation)
        {
            WeatherStation stationModel = newStation.ToModel();

            var stationsList = _weatherStationLogic.AddNewStation(stationModel);

            return stationsList.Select(x => WeatherStationDTO.FromModel(x));
        }

        //DELETE
        [HttpDelete("delete/{id}")]
        public IEnumerable<WeatherStationDTO> DeleteAStation([FromRoute] int id)
        {
            var stationsList = _weatherStationLogic.DeleteAStation(id);

            return stationsList.Select(x => WeatherStationDTO.FromModel(x));
        }

        //UPDATE
        [HttpPost("update/{id}")]
        public IEnumerable<WeatherStationDTO> UpdateAStation([FromRoute] int id, [FromBody] NewWeatherStationRequestDTO newInfo)
        {
            WeatherStation stationModel = newInfo.ToModel();

            var stationsList = _weatherStationLogic.UpdateAStation(id, stationModel);

            return stationsList.Select(x => WeatherStationDTO.FromModel(x));
        }
    }
}
