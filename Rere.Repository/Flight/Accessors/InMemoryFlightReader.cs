using Microsoft.EntityFrameworkCore;
using Rere.Core.Repositories;
using Rere.Core.Repositories.Flight.Accessors;
using Rere.Infra.Database;
using FlightModel = Rere.Core.Models.Flight.Flight;

namespace Rere.Repository.Flight.Accessors;

public class InMemoryFlightReader(RereDbContext context) : IFlightReader
{
    public async Task<FlightModel> GetFlightByIdAsync(int id)
    {
        return (await context.Flights.FindAsync(id))!;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await context.Flights.AnyAsync(flight => flight.Id == id);
    }

    public async Task<IEnumerable<FlightModel>> ListFlightsAsync()
    {
        return await context.Flights.ToListAsync();
    }

    public async Task<IEnumerable<FlightModel>> SearchFlightsAsync(SearchQuery<FlightModel> query)
    {
        var querySearchCriteria = query.SearchCriteria;
        return await context.Flights.Where(querySearchCriteria).ToListAsync();
    }
}