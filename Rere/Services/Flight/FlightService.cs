using Rere.Core.Repositories.Flight;
using Rere.Core.Services.Flight;
using FlightModel = Rere.Core.Models.Flight.Flight;

namespace Rere.Services.Flight;

public class FlightService(IFlightRepository flightRepository) : IFlightService
{
    public async Task<IEnumerable<FlightModel>> GetAllFlightAsync()
    {
        return await flightRepository.ListAsync();
    }

    public Task<FlightModel?> GetFlightByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task CreateFlightAsync(FlightModel flight)
    {
        throw new NotImplementedException();
    }

    public Task UpdateFlightAsync(FlightModel flight)
    {
        throw new NotImplementedException();
    }

    public Task DeleteFlightAsync(int id)
    {
        throw new NotImplementedException();
    }
}