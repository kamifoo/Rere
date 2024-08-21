namespace Rere.Core.Repositories.Flight.Accessors;

using FlightModel = Rere.Core.Models.Flight.Flight;

public interface IFlightWriter
{
    Task<int> AddFlight(FlightModel flight);
    Task UpdateFlight(FlightModel flight);
    Task DeleteFlight(int id);
}
