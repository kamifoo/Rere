using Rere.Core.Repositories;
using Rere.Core.Repositories.Flight;
using Rere.Core.Repositories.Flight.Accessors;
using FlightModel = Rere.Core.Models.Flight.Flight;

namespace Rere.Repositories.Flight;

public class FlightRepository(IFlightReader reader, IFlightWriter writer) : IFlightRepository
{
    public async Task<IEnumerable<FlightModel>> ListAsync()
    {
        return await reader.ListFlightsAsync();
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await reader.ExistsAsync(id);
    }

    public async Task<FlightModel?> GetByIdOrNullAsync(int id)
    {
        if (await ExistsAsync(id) is false)
            return null;
        return await reader.GetFlightByIdAsync(id);
    }

    public async Task<IEnumerable<FlightModel>> SearchAsync(SearchQuery<FlightModel> query)
    {
        return await reader.SearchFlightsAsync(query);
    }

    public async Task<int> AddAsync(FlightModel flight)
    {
        return await writer.AddFlight(flight);
    }

    public async Task UpdateAsync(FlightModel flight)
    {
        await writer.UpdateFlight(flight);
    }

    public async Task DeleteAsync(int id)
    {
        await writer.DeleteFlight(id);
    }
}