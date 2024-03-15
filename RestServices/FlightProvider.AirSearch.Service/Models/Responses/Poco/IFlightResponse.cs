namespace FlightProvider.AirSearch.Service.Models.Responses.Poco;

public interface IFlightResponse
{
    int GroupId { get; set; }
    string FlightNumber { get; set; }
}
