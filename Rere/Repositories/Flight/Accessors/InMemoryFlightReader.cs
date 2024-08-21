using Rere.Core.Repositories;
using Rere.Core.Repositories.Flight.Accessors;
using FlightModel = Rere.Core.Models.Flight.Flight;

namespace Rere.Repositories.Flight.Accessors;

public class InMemoryFlightReader : IFlightReader
{
    public Task<FlightModel> GetFlightByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<FlightModel>> ListFlightsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<FlightModel>> SearchAsync(SearchQuery<FlightModel> query)
    {
        throw new NotImplementedException();
    }
}