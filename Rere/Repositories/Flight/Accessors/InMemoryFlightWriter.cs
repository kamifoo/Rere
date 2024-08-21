using Rere.Core.Repositories.Flight.Accessors;

namespace Rere.Repositories.Flight.Accessors;

public class InMemoryFlightWriter : IFlightWriter
{
    public Task<int> AddFlight(Core.Models.Flight.Flight flight)
    {
        throw new NotImplementedException();
    }

    public Task UpdateFlight(Core.Models.Flight.Flight flight)
    {
        throw new NotImplementedException();
    }

    public Task DeleteFlight(int id)
    {
        throw new NotImplementedException();
    }
}