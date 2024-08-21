namespace Rere.Core.Repositories.Flight.Accessors;

using FlightModel = Rere.Core.Models.Flight.Flight;

public interface IFlightReader
{
    Task<FlightModel> GetFlightByIdAsync(int id);
    Task<IEnumerable<FlightModel>> ListFlightsAsync();
}