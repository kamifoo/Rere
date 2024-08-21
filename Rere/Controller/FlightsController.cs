using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Rere.Core.Exceptions;
using Rere.Core.Models.Flight;
using Rere.Core.Services.Flight;
using Rere.DTOs.Flight;

namespace Rere.Controller;

[ApiController]
[Route("api/[controller]/")]
public class FlightsController(IFlightService service, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// GET /api/ﬂights: Retrieve all ﬂights
    /// </summary>
    /// <returns>All flights</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Flight>>> GetAllFlights()
    {
        var allFlightAsync = await service.GetAllFlightAsync();
        return Ok(allFlightAsync);
    }

    /// <summary>
    /// GET /api/ﬂights/{id}: Retrieve a speciﬁc ﬂight by ID
    /// </summary>
    /// <param name="id">ID of the flight to be updated</param>
    /// <returns>The flight with the specified ID or Not Found</returns>
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Flight>> GetFlightById(int id)
    {
        var flight = await service.GetFlightByIdAsync(id);
        if (flight == null) return NotFound();
        return Ok(flight);
    }

    /// <summary>
    /// POST /api/ﬂights: Create a new ﬂight
    /// </summary>
    /// <param name="newCreateFlight">New flight data</param>
    /// <returns>ID of new created flight</returns>
    [HttpPost]
    public async Task<ActionResult<int>> CreateFlight([FromBody] CreateFlightDto newCreateFlight)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var newFlight = mapper.Map<Flight>(newCreateFlight);
        var flightId = await service.CreateFlightAsync(newFlight);
        // Return a relative URI to the new flight
        var flightUri = $"api/flights/{flightId}";

        return Created(flightUri, flightId);
    }

    /// <summary>
    /// PUT /api/ﬂights/{id}: Update a speciﬁc ﬂight
    /// </summary>
    /// <param name="id">ID of the flight to be updated</param>
    /// <param name="updateFlightDto">Update flight data</param>
    /// <returns>OK if updated or No Content</returns>
    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult> UpdateFlight(int id, [FromBody] UpdateFlightDto updateFlightDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var updateFlight = mapper.Map<Flight>(updateFlightDto);
            await service.UpdateFlightAsync(id, updateFlight);
            return Ok();
        }
        catch (ResourceNotFoundException<Flight>)
        {
            // TODO Logger can log
            return NoContent();
        }
    }

    /// <summary>
    /// DELETE /api/ﬂights/{id}: Delete a speciﬁc ﬂight
    /// </summary>
    /// <param name="id">ID of the flight to be updated</param>
    /// <returns>Ok if deleted or No Content</returns>
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