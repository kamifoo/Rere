namespace Rere.Core.Repositories.Flight.Accessors;

using FlightModel = Rere.Core.Models.Flight.Flight;

public interface IFlightReader
{
    /// <summary>
    /// Get a flight by its ID
    /// </summary>
    /// <param name="id">ID of that requesting flight</param>
    /// <returns>Flight entity</returns>
    Task<FlightModel> GetFlightByIdAsync(int id);

    /// <summary>
    /// Check if a flight is existed or not
    /// </summary>
    /// <param name="id">ID of that flight</param>
    /// <returns>true: existed; false: NOT existed</returns>
    Task<bool> ExistsAsync(int id);

    /// <summary>
    /// Get all flights from persistence
    /// </summary>
    /// <returns>Flights</returns>
    Task<IEnumerable<FlightModel>> ListFlightsAsync();

    /// <summary>
    /// Search flight by search query
    /// </summary>
    /// <param name="query">Flight search query</param>
    /// <returns>All matched flights</returns>
    Task<IEnumerable<FlightModel>> SearchFlightsAsync(SearchQuery<FlightModel> query);
}