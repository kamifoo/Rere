using Microsoft.AspNetCore.Mvc;
using NUnit.Framework.Constraints;
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
        return new OkObjectResult(await service.GetAllFlightAsync());
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Flight>> GetFlightById(int id)
    {
        var flight = await service.GetFlightByIdAsync(id);
        if (flight == null) return new NotFoundResult();
        return new OkObjectResult(flight);
    }
}