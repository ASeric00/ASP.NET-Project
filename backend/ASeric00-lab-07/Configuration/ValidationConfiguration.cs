namespace ASeric00_lab_07.Configuration
{
	public class ValidationConfiguration
	{
        public int MaxStationNameLength { get; set; }

        public string[] possibleConditions { get; set; }

        public int MaxTemperatureValue { get; set; }
        public int MinTemperatureValue { get; set; }

        public int MaxPressureValue { get; set; }
        public int MinPressureValue { get; set; }

        public int MaxWindSpeedValue { get; set; }
        public int MinWindSpeedValue { get; set; }

        public string[] possibleWindDirections { get; set; }
    }
}

