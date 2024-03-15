using FlightProvider.AirSearch.Service.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FlightProvider.AirSearch.Service.Controllers.Base;

[ApiController]
public class ApiControllerBase : ControllerBase
{
    protected IActionResult CreateResult<T>(ApiResponse<T> response)
    {
        return new JsonResult(response)
        {
            StatusCode = response.StatusCode
        };
    }
}
