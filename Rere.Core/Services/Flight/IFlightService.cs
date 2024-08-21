using Rere.Core.Exceptions;
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
    /// <exception cref="ResourceNotFoundException{T}">Thrown when the flight with the specified ID is not found.</exception>
    Task<FlightModel?> GetFlightByIdAsync(int id);

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
}