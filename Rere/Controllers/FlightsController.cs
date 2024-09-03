using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Rere.Controllers.Attributes;
using Rere.Core.Exceptions;
using Rere.Core.Models.Flight;
using Rere.Core.Services.Flight;
using Rere.DTOs.Flight;
using Rere.Repository.Flight;

namespace Rere.Controllers;

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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<FlightDto>))]
    public async Task<ActionResult<IEnumerable<FlightDto>>> GetAllFlights()
    {
        var allFlights = await service.GetAllFlightAsync();
        var flightDtos = allFlights.Select(mapper.Map<Flight, FlightDto>);
        return Ok(flightDtos);
    }

    /// <summary>
    /// GET /api/ﬂights/{id}: Retrieve a speciﬁc ﬂight by ID
    /// </summary>
    /// <param name="id">ID of the flight to be updated</param>
    /// <returns>The flight with the specified ID or Not Found</returns>
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FlightDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [GuardModelState($"Flight ID should be integer above 0")]
    public async Task<ActionResult<FlightDto>> GetFlightById(int id)
    {
        var flight = await service.GetFlightByIdOrNullAsync(id);
        if (flight == null)
            return NotFound(CreateProblemDetails("Not Found", $"Flight with ID {id} was not found.",
                StatusCodes.Status404NotFound));

        var flightDto = mapper.Map<Flight, FlightDto>(flight);
        return Ok(flightDto);
    }

    /// <summary>
    /// POST /api/ﬂights: Create a new ﬂight
    /// </summary>
    /// <param name="newFlightCreation">New flight data</param>
    /// <returns>ID of new created flight</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [GuardModelState("Flight information in body cannot be recognised")]
    public async Task<ActionResult<int>> CreateFlight([FromBody] FlightCreationDto newFlightCreation)
    {
        var newFlight = mapper.Map<FlightCreationDto, Flight>(newFlightCreation);
        var flightId = await service.CreateFlightAsync(newFlight);
        // Return a relative URI to the new flight
        var flightUri = $"api/flights/{flightId}";

        return Created(flightUri, flightId);
    }

    /// <summary>
    /// PUT /api/ﬂights/{id}: Update a speciﬁc ﬂight
    /// </summary>
    /// <param name="id">ID of the flight to be updated</param>
    /// <param name="flightUpdateDto">Update flight data</param>
    /// <returns>OK if updated or No Content</returns>
    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [GuardModelState("Flight information in body cannot be recognised")]
    public async Task<ActionResult> UpdateFlight(int id, [FromBody] FlightUpdateDto flightUpdateDto)
    {
        try
        {
            var updateFlight = mapper.Map<FlightUpdateDto, Flight>(flightUpdateDto);
            await service.UpdateFlightAsync(id, updateFlight);
            return Ok();
        }
        catch (ResourceNotFoundException<Flight> ex)
        {
            logger.LogWarning(ex.Message);
            return NotFound(CreateProblemDetails("Not Found", $"Flight cannot be found by {id}",
                StatusCodes.Status404NotFound));
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
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    public async Task<ActionResult> DeleteFlight(int id)
    {
        try
        {
            await service.DeleteFlightAsync(id);
            return NoContent();
        }
        catch (ResourceNotFoundException<Flight> ex)
        {
            logger.LogWarning(ex.Message);
            return NoContent();
        }
    }

    [HttpGet]
    [Route("search")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [GuardQueryParameters]
    [GuardModelState("Query cannot be recognised")]
    public async Task<ActionResult<IEnumerable<FlightDto>>> SearchFlights(
        [FromQuery] FlightSearchParams flightSearchParams)
    {
        try
        {
            var searchQuery = mapper.Map<FlightSearchParams, FlightSearchQuery>(flightSearchParams);
            var searchResult = await service.SearchFlights(searchQuery);
            var flightDtos = searchResult.Select(mapper.Map<Flight, FlightDto>);
            return Ok(flightDtos);
        }
        catch (AutoMapperMappingException ex)
        {
            logger.LogError(ex, ex.Message);
            return BadRequest(
                CreateProblemDetails("Bad Request", $"Query contain invalid information",
                    StatusCodes.Status400BadRequest));
        }
        catch (FormatException ex)
        {
            logger.LogError(ex, ex.Message);
            return BadRequest(
                CreateProblemDetails("Bad Request", $"Query contain invalid information",
                    StatusCodes.Status400BadRequest));
        }
    }

    private static ProblemDetails CreateProblemDetails(string title, string detail, int? status)
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