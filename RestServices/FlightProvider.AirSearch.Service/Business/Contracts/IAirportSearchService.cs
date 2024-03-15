using AirportWcfService;
using FlightProvider.AirSearch.Service.Models.Responses;

namespace FlightProvider.AirSearch.Service.Business.Contracts;

public interface IAirportSearchService
{
    Task<ApiResponse<AirportSearchResult>> Search(AirportSearchRequest request);
}
