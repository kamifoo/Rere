using Rere.Core.Exceptions;
using Rere.Core.Repositories;
using FlightModel = Rere.Core.Models.Flight.Flight;

namespace Rere.Core.Services.Flight;

public interface IFlightService
{
    /// <summary>
    /// Retrieve all ﬂights
    /// </summary>
    /// <returns>All flights in database</returns>
    Task<IEnumerable<FlightModel>> GetAllFlightAsync();

    /// <summary>
    ///  Retrieve a speciﬁc ﬂight by ID
    /// </summary>
    /// <param name="id">ID of the flight to be updated</param>
    /// <returns>The flight with the specified ID</returns>
    Task<FlightModel?> GetFlightByIdOrNullAsync(int id);

    /// <summary>
    ///  Create a new ﬂight
    /// </summary>
    /// <param name="flight">New flight model</param>
    /// <returns>ID of new created flight</returns>
    Task<int> CreateFlightAsync(FlightModel flight);

    /// <summary>
    /// Update a speciﬁc ﬂight
    /// </summary>
    /// <param name="id">ID of the flight to be updated</param>
    /// <param name="flight">Flight model</param>
    /// <exception cref="ResourceNotFoundException{T}">Thrown when the flight with the specified ID is not found.</exception>
    Task UpdateFlightAsync(int id, FlightModel flight);

    /// <summary>
    /// Delete a speciﬁc ﬂight
    /// </summary>
    /// <param name="id">ID of the flight to be updated</param>
    /// <exception cref="ResourceNotFoundException{T}">Thrown when the flight with the specified ID is not found.</exception>
    Task DeleteFlightAsync(int id);

    /// <summary>
    /// Search flight with search query
    /// </summary>
    /// <param name="flightSearchQuery">Flight search query</param>
    /// <returns>Flights that meet search conditions</returns>
    Task<IEnumerable<FlightModel>> SearchFlights(SearchQuery<FlightModel> flightSearchQuery);
}