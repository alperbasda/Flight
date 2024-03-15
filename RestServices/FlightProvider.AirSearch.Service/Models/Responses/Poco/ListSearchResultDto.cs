namespace FlightProvider.AirSearch.Service.Models.Responses.Poco;

public class ListSearchResultDto : IFlightResponse
{
    public int GroupId { get; set; }

    public string FlightNumber { get; set; }
}
