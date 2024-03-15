using System.Text.Json.Serialization;

namespace FlightProvider.AirSearch.Service.Models.Responses.Poco
{
    public class DetailSearchResultDto : IFlightResponse
    {
        [JsonIgnore]
        public int GroupId { get; set; } = 0;

        public DateTime ArrivalDateTime { get; set; }

        public DateTime DepartureDateTime { get; set; }

        public string FlightNumber { get; set; }

        public decimal Price { get; set; }
    }
}
