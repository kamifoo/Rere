using Rere.Infra.Database;
using Rere.Repository.Tests.Fixtures;

namespace Rere.Tests.Fixtures;

public static class DbInitialiser
{
    public static void Seed(RereDbContext context)
    {
        if (context.Flights.Any()) return;
        context.Flights.AddRange(TestFlightFixture.GetTestFlights());
        context.SaveChanges();
    }
}