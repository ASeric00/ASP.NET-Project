using Microsoft.Data.Sqlite;
using ASeric00_lab_07.Models;
using ASeric00_lab_07.Configuration;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace ASeric00_lab_07.Repositories
{
	public class WeatherSqlRepository : IWeatherRepository
	{
        private readonly string? _connectionString;
        private const string _dbDatetimeFormat = "yyyy-MM-ddTHH:mm:ss.fff"; // ISO8601 without timezone

        public WeatherSqlRepository(IOptions<DBConfiguration> configuration)
        {
            _connectionString = configuration.Value.ConnectionString;
        }

        //GET INFO FROM ALL STATIONS
        public IEnumerable<WeatherStation> GetAllStations()
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            @"SELECT ID, Timestamp, StationName, Conditions, Temperature, Pressure, WindSpeed, WindDirection FROM WeatherStations";

            using var reader = command.ExecuteReader();

            var results = new List<WeatherStation>();
            while (reader.Read())
            {
                var row = new WeatherStation(
                    id: reader.GetInt32(0),
                    timestamp: DateTime.ParseExact(reader.GetString(1), _dbDatetimeFormat, CultureInfo.InvariantCulture),
                    stationName: reader.GetString(2),
                    conditions: reader.GetString(3),
                    temperature: reader.GetInt32(4),
                    pressure: reader.GetInt32(5),
                    windSpeed: reader.GetInt32(6),
                    windDirection: reader.GetString(7)
                );

                results.Add(row);
            }

            return results;
        }

        //GET INFO FROM ONE STATION BY ITS NAME
        public IEnumerable<WeatherStation> GetOneStationByName(string stationName)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            @"SELECT ID, Timestamp, StationName, Conditions, Temperature, Pressure, WindSpeed, WindDirection FROM WeatherStations WHERE StationName = $stationName";

            command.Parameters.AddWithValue("$stationName", stationName);

            using var reader = command.ExecuteReader();

            var results = new List<WeatherStation>();

            while (reader.Read())
            {
                var row = new WeatherStation(
                    id: reader.GetInt32(0),
                    timestamp: DateTime.ParseExact(reader.GetString(1), _dbDatetimeFormat, CultureInfo.InvariantCulture),
                    stationName: reader.GetString(2),
                    conditions: reader.GetString(3),
                    temperature: reader.GetInt32(4),
                    pressure: reader.GetInt32(5),
                    windSpeed: reader.GetInt32(6),
                    windDirection: reader.GetString(7)
                );
                results.Add(row);
            }

            return results;
        }

        //GET WEATHER RECORD BY ID
        public WeatherStation GetRecordById(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            @"SELECT ID, Timestamp, StationName, Conditions, Temperature, Pressure, WindSpeed, WindDirection FROM WeatherStations WHERE Id = $id";

            command.Parameters.AddWithValue("id", id);

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new WeatherStation(
                    id: reader.GetInt32(0),
                    timestamp: DateTime.ParseExact(reader.GetString(1), _dbDatetimeFormat, CultureInfo.InvariantCulture),
                    stationName: reader.GetString(2),
                    conditions: reader.GetString(3),
                    temperature: reader.GetInt32(4),
                    pressure: reader.GetInt32(5),
                    windSpeed: reader.GetInt32(6),
                    windDirection: reader.GetString(7)
                );
            }

            return null;
        }


        //POST
        public IEnumerable<WeatherStation> AddNewStation(WeatherStation newStation)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();

            command.CommandText =
            @"
                INSERT INTO WeatherStations (Timestamp, StationName, Conditions, Temperature, Pressure, WindSpeed, WindDirection)
                VALUES ($timestamp, $stationName, $conditions, $temperature, $pressure, $windSpeed, $windDirection)";

            command.Parameters.AddWithValue("$timestamp", newStation.Timestamp.ToString(_dbDatetimeFormat));
            command.Parameters.AddWithValue("$stationName", newStation.StationName);
            command.Parameters.AddWithValue("$conditions", newStation.Conditions);
            command.Parameters.AddWithValue("$temperature", newStation.Temperature);
            command.Parameters.AddWithValue("$pressure", newStation.Pressure);
            command.Parameters.AddWithValue("$windSpeed", newStation.WindSpeed);
            command.Parameters.AddWithValue("$windDirection", newStation.WindDirection);

            _ = command.ExecuteNonQuery();

            return GetAllStations();
        }

        //DELETE
        public IEnumerable<WeatherStation> DeleteAStation(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();

            command.CommandText =
            @"
                DELETE FROM WeatherStations
                WHERE ID = $id";

            command.Parameters.AddWithValue("$id", id);

            _ = command.ExecuteNonQuery();


            return GetAllStations();
        }

        //UPDATE
        public IEnumerable<WeatherStation> UpdateAStation(int id, WeatherStation newInfo)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();

            command.CommandText =
            @"
                UPDATE WeatherStations
                SET
                    Timestamp = $timestamp,
                    StationName = $stationName,
                    Conditions = $conditions,
                    Temperature = $temperature,
                    Pressure = $pressure,
                    WindSpeed = $windSpeed,
                    WindDirection = $windDirection
                WHERE
                    ID = $id;";

            command.Parameters.AddWithValue("id", id);
            command.Parameters.AddWithValue("timestamp", newInfo.Timestamp.ToString(_dbDatetimeFormat));
            command.Parameters.AddWithValue("$stationName", newInfo.StationName);
            command.Parameters.AddWithValue("$conditions", newInfo.Conditions);
            command.Parameters.AddWithValue("$temperature", newInfo.Temperature);
            command.Parameters.AddWithValue("$pressure", newInfo.Pressure);
            command.Parameters.AddWithValue("$windSpeed", newInfo.WindSpeed);
            command.Parameters.AddWithValue("$windDirection", newInfo.WindDirection);

            _ = command.ExecuteNonQuery();


            return GetAllStations();
        }
    }
}