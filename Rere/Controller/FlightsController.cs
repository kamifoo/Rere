using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Rere.Core.Exceptions;
using Rere.Core.Models.Flight;
using Rere.Core.Services.Flight;
using Rere.DTOs.Flight;

namespace Rere.Controller;

[ApiController]
[Route("api/[controller]/")]
public class FlightsController(ILogger<FlightsController> logger, IFlightService service, IMapper mapper)
    : ControllerBase
{
    /// <summary>
    /// GET /api/ﬂights: Retrieve all ﬂights
    /// </summary>
    /// <returns>All flights</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Flight>>> GetAllFlights()
    {
        using (logger.BeginScope("Get All Flights"))
        {
            try
            {
                var allFlightAsync = await service.GetAllFlightAsync();
                return Ok(allFlightAsync);
            }
            catch (HttpRequestException ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode(500, $"Internal Server Error：{ex.Message}");
            }
        }
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
        using (logger.BeginScope("Get Flight By Id"))
        {
            try
            {
                var flight = await service.GetFlightByIdAsync(id);
                if (flight == null) return NotFound();
                return Ok(flight);
            }
            catch (HttpRequestException ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode(500, $"Internal Server Error：{ex.Message}");
            }
        }
    }

    /// <summary>
    /// POST /api/ﬂights: Create a new ﬂight
    /// </summary>
    /// <param name="newCreateFlight">New flight data</param>
    /// <returns>ID of new created flight</returns>
    [HttpPost]
    public async Task<ActionResult<int>> CreateFlight([FromBody] CreateFlightDto newCreateFlight)
    {
        using (logger.BeginScope("Create Flight"))
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var newFlight = mapper.Map<Flight>(newCreateFlight);
                var flightId = await service.CreateFlightAsync(newFlight);
                // Return a relative URI to the new flight
                var flightUri = $"api/flights/{flightId}";

                return Created(flightUri, flightId);
            }
            catch (HttpRequestException ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode(500, $"Internal Server Error：{ex.Message}");
            }
        }
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
        using (logger.BeginScope("Get Flight By Id"))
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var updateFlight = mapper.Map<Flight>(updateFlightDto);
                await service.UpdateFlightAsync(id, updateFlight);
                return Ok();
            }
            catch (ResourceNotFoundException<Flight> ex)
            {
                logger.LogWarning(ex.Message);
                return NoContent();
            }
            catch (HttpRequestException ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode(500, $"Internal Server Error：{ex.Message}");
            }
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
        catch (ResourceNotFoundException<Flight> ex)
        {
            logger.LogWarning(ex.Message);
            return NoContent();
        }
        catch (HttpRequestException ex)
        {
            logger.LogError(ex, ex.Message);
            return StatusCode(500, $"Internal Server Error：{ex.Message}");
        }
    }
}