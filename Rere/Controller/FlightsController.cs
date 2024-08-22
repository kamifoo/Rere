using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetFlightDto))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<Flight>>> GetAllFlights()
    {
        using (logger.BeginScope("Get All Flights"))
        {
            try
            {
                var allFlights = await service.GetAllFlightAsync();
                var flightDtos = allFlights.Select(mapper.Map<Flight, GetFlightDto>);
                return Ok(flightDtos);
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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetFlightDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Flight>> GetFlightById([Range(1, int.MaxValue)] int id)
    {
        using (logger.BeginScope("Get Flight By Id"))
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(
                        CreateProblemDetails("Bad Request", $"{id} should be integer above 0",
                            StatusCodes.Status400BadRequest));

                var flight = await service.GetFlightByIdAsync(id);
                if (flight == null)
                    return NotFound(CreateProblemDetails("Not Found", $"Flight with ID {id} was not found.",
                        StatusCodes.Status404NotFound));

                var flightDto = mapper.Map<Flight, GetFlightDto>(flight);
                return Ok(flightDto);
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
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<int>> CreateFlight([FromBody] CreateFlightDto newCreateFlight)
    {
        using (logger.BeginScope("Create Flight"))
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(
                        CreateProblemDetails("Bad Request", "Flight information in body cannot be recognised",
                            StatusCodes.Status400BadRequest));

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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateFlight(int id, [FromBody] UpdateFlightDto updateFlightDto)
    {
        using (logger.BeginScope("Update Flight By Id with new information"))
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(
                        CreateProblemDetails("Bad Request", "Flight information in body cannot be recognised",
                            StatusCodes.Status400BadRequest));

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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteFlight([Range(1, int.MaxValue)] int id)
    {
        using (logger.BeginScope("Delete Flight By Id"))
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


    private ProblemDetails CreateProblemDetails(string title, string detail, int? status)
    {
        return new ProblemDetails
        {
            Type = "https://tools.ietf.org/html/rfc9110#section-15.5.5",
            Title = title,
            Status = status,
            Detail = detail
        };
    }
}