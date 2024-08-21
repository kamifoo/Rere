using Microsoft.AspNetCore.Mvc;
using Rere.Core.Exceptions;
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
        var allFlightAsync = await service.GetAllFlightAsync();
        return Ok(allFlightAsync);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Flight>> GetFlightById(int id)
    {
        var flight = await service.GetFlightByIdAsync(id);
        if (flight == null) return NotFound();
        return Ok(flight);
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateFlight(Flight newFlight)
    {
        var flightId = await service.CreateFlightAsync(newFlight);
        // Return a relative URI to the new flight
        var flightUri = $"api/flights/{flightId}";
        return Created(flightUri, flightId);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult> UpdateFlight(int id, [FromBody] Flight flightToUpdate)
    {
        try
        {
            await service.UpdateFlightAsync(id, flightToUpdate);
            return Ok();
        }
        catch (ResourceNotFoundException<Flight>)
        {
            // TODO Logger can log
            return NoContent();
        }
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> DeleteFlight(int id)
    {
        try
        {
            await service.DeleteFlightAsync(id);
            return Ok();
        }
        catch (ResourceNotFoundException<Flight>)
        {
            // TODO Logger can log
            return NoContent();
        }
    }
}