using AirportWcfService;
using FlightProvider.AirSearch.Service.Business.Contracts;
using FlightProvider.AirSearch.Service.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace FlightProvider.AirSearch.Service.Controllers;

[Route("airportsearchs")]
public class AirportSearchsController : ApiControllerBase
{
    private readonly IAirportSearchService _airportSearchService;

    public AirportSearchsController(IAirportSearchService airportSearchService)
    {
        _airportSearchService = airportSearchService;
    }

    [HttpGet("search")]
    public async Task<IActionResult> List(string term)
    {
        return CreateResult(await _airportSearchService.Search(new AirportSearchRequest { Condition=term}));
    }
}
