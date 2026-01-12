using ASeric00_lab_07.Models;

namespace ASeric00_lab_07.Repositories
{
    public class WeatherRepository
    {

        public List<WeatherStation> WeatherStations;
        public WeatherRepository()
        {
            WeatherStations = new List<WeatherStation>();
            WeatherStations.Add(new WeatherStation("Split", "SUNNY", 15, 1012, 23, "NW"));
            WeatherStations.Add(new WeatherStation("Zagreb", "CLOUDY", 8, 1008, 12, "NW"));
            WeatherStations.Add(new WeatherStation("Rijeka", "RAIN", 13, 1008, 15, "N"));
        }
    }
}
