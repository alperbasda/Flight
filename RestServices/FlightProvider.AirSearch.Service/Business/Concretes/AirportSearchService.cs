using AirportWcfService;
using FlightProvider.AirSearch.Service.Business.Contracts;
using FlightProvider.AirSearch.Service.Models.Responses;

namespace FlightProvider.AirSearch.Service.Business.Concretes;

public class AirportSearchService : IAirportSearchService
{
    private readonly AirportSearchClient _airportSearchClient;

    public AirportSearchService(AirportSearchClient airportSearchClient)
    {
        _airportSearchClient = airportSearchClient;
    }

    public async Task<ApiResponse<AirportSearchResult>> Search(AirportSearchRequest request)
    {
        return ApiResponse<AirportSearchResult>.Success(await _airportSearchClient.SearchAsync(request), 200);
    }
}
