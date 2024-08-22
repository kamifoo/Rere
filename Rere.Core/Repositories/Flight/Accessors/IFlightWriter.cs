namespace Rere.Core.Repositories.Flight.Accessors;

using FlightModel = Rere.Core.Models.Flight.Flight;

public interface IFlightWriter
{
    /// <summary>
    /// Add a flight entity
    /// </summary>
    /// <param name="flight">Flight entity</param>
    /// <returns>ID of new added flight</returns>
    Task<int> AddFlight(FlightModel flight);

    /// <summary>
    /// Update a flight entity
    /// </summary>
    /// <param name="flight">Flight entity</param>
    /// <returns></returns>
    Task UpdateFlight(FlightModel flight);

    /// <summary>
    /// Delete a flight entity by its ID
    /// </summary>
    /// <param name="id">ID of that entity</param>
    /// <returns></returns>
    Task DeleteFlight(int id);
}