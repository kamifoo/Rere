using Microsoft.AspNetCore.Mvc;
using Rere.Core.Models.Flight;
using Rere.Core.Services.Flight;

namespace Rere.Controller;

[ApiController]
[Route("api/[controller]/")]
public class FlightController(IFlightService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Flight>>> GetAllFlights()
    {
        var allFlightAsync = await service.GetAllFlightAsync();
        return new OkObjectResult(allFlightAsync.ToList());
    }
}