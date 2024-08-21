using Rere.Core.Repositories.Flight.Accessors;

namespace Rere.Repositories.Flight.Accessors;

public class InMemoryFlightReader : IFlightReader
{
    public Task<Core.Models.Flight.Flight> GetFlightByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Core.Models.Flight.Flight>> ListFlightsAsync()
    {
        throw new NotImplementedException();
    }
}