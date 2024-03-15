using FlightProvider.AirSearch.Service.Models.Requests;
using FlightProvider.AirSearch.Service.Models.Responses;
using FlightProvider.AirSearch.Service.Models.Responses.Poco;

namespace FlightProvider.AirSearch.Service.Business.Contracts;

public interface IAirSearchService
{
    Task<ApiResponse<List<ListSearchResultDto>>> AvailabilitySearchAsync(List<SearchRequestDto> request);

    Task<ApiResponse<DetailSearchResultDto>> Detail(GetByFlightNumberRequestDto request);
}
