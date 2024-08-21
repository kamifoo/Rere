using Microsoft.AspNetCore.Mvc;
using Rere.Core.Models.Flight;
using Rere.Core.Services.Flight;

namespace Rere.Controller;

[ApiController]
[Route("api/[controller]/")]
public class FlightsController(IFlightService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Flight>>> GetAllFlights()
    {
        return new OkObjectResult((await service.GetAllFlightAsync()));
    }
}