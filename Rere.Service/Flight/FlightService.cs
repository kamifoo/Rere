using Rere.Core.Exceptions;
using Rere.Core.Repositories;
using Rere.Core.Repositories.Flight;
using Rere.Core.Services.Flight;
using FlightModel = Rere.Core.Models.Flight.Flight;

namespace Rere.Service.Flight;

public class FlightService(IFlightRepository flightRepository) : IFlightService
{
    public async Task<IEnumerable<FlightModel>> GetAllFlightAsync()
    {
        return await flightRepository.ListAsync();
    }

    public async Task<FlightModel?> GetFlightByIdOrNullAsync(int id)
    {
        return await flightRepository.GetByIdOrNullAsync(id);
    }

    public async Task<int> CreateFlightAsync(FlightModel flight)
    {
        return await flightRepository.AddAsync(flight);
    }

    public async Task UpdateFlightAsync(int id, FlightModel flight)
    {
        if (await flightRepository.ExistsAsync(id) is false)
            throw new ResourceNotFoundException<FlightModel>(id);

        await flightRepository.UpdateAsync(flight);
    }

    public async Task DeleteFlightAsync(int id)
    {
        if (await flightRepository.ExistsAsync(id) is false)
            throw new ResourceNotFoundException<FlightModel>(id);

        await flightRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<FlightModel>> SearchFlights(SearchQuery<FlightModel> flightSearchQuery)
    {
        return await flightRepository.SearchAsync(flightSearchQuery);
    }
}