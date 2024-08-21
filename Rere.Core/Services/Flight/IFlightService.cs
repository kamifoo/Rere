using Rere.Core.Exceptions;
using FlightModel = Rere.Core.Models.Flight.Flight;

namespace Rere.Core.Services.Flight;

public interface IFlightService
{
    /// <summary>
    /// GET /api/ﬂights: Retrieve all ﬂights
    /// </summary>
    /// <returns>All flights in database</returns>
    Task<IEnumerable<FlightModel>> GetAllFlightAsync();

    /// <summary>
    /// GET /api/ﬂights/{id}: Retrieve a speciﬁc ﬂight by ID
    /// </summary>
    /// <param name="id">Id of the flight to be updated</param>
    /// <returns>The flight with the specified ID</returns>
    /// <exception cref="ResourceNotFoundException{T}">Thrown when the flight with the specified ID is not found.</exception>
    Task<FlightModel?> GetFlightByIdAsync(int id);

    /// <summary>
    /// POST /api/ﬂights: Create a new ﬂight
    /// </summary>
    /// <param name="flight">New flight model</param>
    /// <returns>Id of new created flight</returns>
    Task<int> CreateFlightAsync(FlightModel flight);

    /// <summary>
    /// PUT /api/ﬂights/{id}: Update a speciﬁc ﬂight
    /// </summary>
    /// <param name="id">Id of the flight to be updated</param>
    /// <param name="flight">Flight model</param>
    /// <exception cref="ResourceNotFoundException{T}">Thrown when the flight with the specified ID is not found.</exception>
    Task UpdateFlightAsync(int id, FlightModel flight);

    /// <summary>
    /// DELETE /api/ﬂights/{id}: Delete a speciﬁc ﬂight
    /// </summary>
    /// <param name="id">Id of the flight to be updated</param>
    /// <exception cref="ResourceNotFoundException{T}">Thrown when the flight with the specified ID is not found.</exception>
    Task DeleteFlightAsync(int id);
}