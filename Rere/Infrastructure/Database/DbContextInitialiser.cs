using Rere.Tests.Fixtures;

namespace Rere.Infrastructure.Database;

public static class DbInitialiser
{
    public static void Seed(RereDbContext context)
    {
        if (context.Flights.Any()) return;
        context.Flights.AddRange(TestFlightFixture.GetTestFlights());
        context.SaveChanges();
    }
}