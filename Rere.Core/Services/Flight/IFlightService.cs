namespace Rere.Core.Services.Flight;

public interface IFlightService
{
    Task<IEnumerable<Models.Flight.Flight>> GetAllFlightAsync();
}