using Rere.Core.Repositories.Flight.Accessors;
using Rere.Infra.Database;
using FlightModel = Rere.Core.Models.Flight.Flight;

namespace Rere.Repository.Flight.Accessors;

public class InMemoryFlightWriter(RereDbContext context) : IFlightWriter
{
    public async Task<int> AddFlight(FlightModel flight)
    {
        context.Flights.Add(flight);
        await context.SaveChangesAsync();
        return flight.Id;
    }

    public async Task UpdateFlight(FlightModel flight)
    {
        var existingFlight = await context.Flights.FindAsync(flight.Id);
        context.Entry(existingFlight!).CurrentValues.SetValues(flight);
        await context.SaveChangesAsync();
    }

    public async Task DeleteFlight(int id)
    {
        var existingFlight = await context.Flights.FindAsync(id);
        context.Flights.Remove(existingFlight!);
        await context.SaveChangesAsync();
    }
}