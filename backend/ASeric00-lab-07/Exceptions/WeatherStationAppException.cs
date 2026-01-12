namespace ASeric00_lab_07.Exceptions
{
	public class WeatherStationAppException : Exception
	{
        public WeatherStationAppException(string? message) : base(message)
        {
        }
    }

    //user error
    public class WeatherStationAppException_UserError : WeatherStationAppException
    {
        public WeatherStationAppException_UserError(string message)
            : base(message)
        {

        }
    }

    //internal error
    public class WeatherStationAppException_InternalError : WeatherStationAppException
    {
        public WeatherStationAppException_InternalError(string message)
            : base(message)
        {

        }
    }
}

