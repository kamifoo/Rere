using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Rere.Core.Repositories.Flight.Accessors;
using Rere.Infra.Database;
using Rere.Repository.Flight.Accessors;
using Rere.Repository.Tests.Fixtures;

namespace Rere.Repository.Tests.Flight.Accessors;

[TestFixture]
public class FlightWriterTests
{
    private RereDbContext _context;
    private IFlightWriter _flightWriter;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<RereDbContext>()
            .UseInMemoryDatabase("TestDatabase")
            .Options;

        _context = new RereDbContext(options);
        _flightWriter = new InMemoryFlightWriter(_context);

        _context.Flights.AddRange(TestFlightFixture.GetTestFlights());
        _context.SaveChanges();
    }

    [TearDown]
    public void Cleanup()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Test]
    public async Task AddFlight_AddsFlightToDatabase_AndReturnsFlightId()
    {
        var allFlights = _context.Flights.ToList();

        var newFlight = TestFlightFixture.GetSingleTestFlightWithoutId();

        var resultId = await _flightWriter.AddFlight(newFlight);

        Assert.That(resultId, Is.EqualTo(allFlights.Count + 1));

        var newFlightInDb = await _context.Flights.FindAsync(resultId);
        Assert.That(newFlightInDb, Is.Not.Null);
        Assert.That(newFlightInDb!.FlightNumber, Is.EqualTo(newFlight.FlightNumber));
    }

    [Test]
    public async Task UpdateFlight_UpdatesFlightInDatabase()
    {
        var flight = await _context.Flights.FindAsync(1);
        Assert.That(flight, Is.Not.Null);

        flight!.FlightNumber = "FL001-Updated";
        await _flightWriter.UpdateFlight(flight);

        var updatedFlight = await _context.Flights.FindAsync(1);
        Assert.That(updatedFlight!.FlightNumber, Is.EqualTo("FL001-Updated"));
    }

    [Test]
    public async Task DeleteFlight_RemovesFlightFromDatabase()
    {
        await _flightWriter.DeleteFlight(1);

        var result = await _context.Flights.FindAsync(1);

        Assert.That(result, Is.Null);
    }
}