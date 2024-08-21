using Rere.Core.Repositories;
using Rere.Core.Repositories.Flight;
using Rere.Core.Repositories.Flight.Accessors;
using FlightModel = Rere.Core.Models.Flight.Flight;

namespace Rere.Repositories.Flight;

public class FlightRepository(IFlightReader reader, IFlightWriter writer) : IFlightRepository
{
    public Task<IEnumerable<FlightModel>> ListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<FlightModel> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<FlightModel>> SearchAsync(SearchQuery<FlightModel> query)
    {
        throw new NotImplementedException();
    }

    public Task<int> AddAsync(FlightModel entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(FlightModel entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(FlightModel entity)
    {
        throw new NotImplementedException();
    }
}