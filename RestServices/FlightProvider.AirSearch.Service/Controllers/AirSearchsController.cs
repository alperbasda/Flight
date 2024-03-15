using FlightProvider.AirSearch.Service.Business.Contracts;
using FlightProvider.AirSearch.Service.Controllers.Base;
using FlightProvider.AirSearch.Service.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace FlightProvider.AirSearch.Service.Controllers;

[Route("airsearchs")]
public class AirSearchsController : ApiControllerBase
{
    IAirSearchService _airSearchService;

    public AirSearchsController(IAirSearchService airSearchService)
    {
        _airSearchService = airSearchService;
    }

    [HttpPost("list")]
    public async Task<IActionResult> List(List<SearchRequestDto> searchRequests)
    {
        return CreateResult(await _airSearchService.AvailabilitySearchAsync(searchRequests));
    }

    [HttpPost("detail")]
    public async Task<IActionResult> Get(GetByFlightNumberRequestDto request)
    {
        return CreateResult(await _airSearchService.Detail(request));
    }
}
