using ASeric00_lab_07.Models;

namespace ASeric00_lab_07.Logic
{
	public interface IWeatherStationLogic
	{
        //GET INFO FROM ALL STATIONS
		public IEnumerable<WeatherStation> GetAllStations();

        //GET INFO FROM ONE STATION BY ITS NAME
        public IEnumerable<WeatherStation> GetOneStationByName(string stationName);

        //GET WEATHER RECORD BY ID
        public WeatherStation GetRecordById(int id);

        //POST
        public IEnumerable<WeatherStation> AddNewStation(WeatherStation newStation);

        //DELETE
        public IEnumerable<WeatherStation> DeleteAStation(int id);

        //UPDATE
        public IEnumerable<WeatherStation> UpdateAStation(int id, WeatherStation newInfo);

    }
}

