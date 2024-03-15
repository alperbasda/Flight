

namespace FlightProvider.AirSearch.Service.Models.Requests;

public class SearchRequestDto
{
    /// <summary>
    /// Sonuçları gruplamak için kullanılır.
    /// </summary>
    public int GroupId { get; set; } = 0;

    public DateTime DepartureDate { get; set; }

    public string Destination { get; set; }

    public string Origin { get; set; }
}

public class GetByFlightNumberRequestDto
{
    public string FlightNumber { get; set; }

    public DateTime DepartureDate { get; set; }

    public string Destination { get; set; }

    public string Origin { get; set; }


}
