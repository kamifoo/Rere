using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Rere.Core.Repositories.Flight.Accessors;
using Rere.Infrastructure.Database;
using Rere.Repositories.Flight.Accessors;
using FlightModel = Rere.Core.Models.Flight.Flight;

namespace Rere.Tests.Repositories.Accessors;

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
        _flightWriter = new InMemoryFlightWriter();

        _context.Flights.AddRange(new[]
        {
            new FlightModel { Id = 1, FlightNumber = "FL001" },
            new FlightModel { Id = 2, FlightNumber = "FL002" }
        });
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
        var newFlight = new FlightModel { Id = 3, FlightNumber = "FL003" };

        var resultId = await _flightWriter.AddFlight(newFlight);

        Assert.That(resultId, Is.EqualTo(3));

        var flightInDb = await _context.Flights.FindAsync(3);
        Assert.That(flightInDb, Is.Not.Null);
        Assert.That(flightInDb.FlightNumber, Is.EqualTo("FL003"));
    }

    [Test]
    public async Task UpdateFlight_UpdatesFlightInDatabase()
    {
        var flight = await _context.Flights.FindAsync(1);
        flight.FlightNumber = "FL001-Updated";

        await _flightWriter.UpdateFlight(flight);

        var updatedFlight = await _context.Flights.FindAsync(1);
        Assert.That(updatedFlight.FlightNumber, Is.EqualTo("FL001-Updated"));
    }

    [Test]
    public async Task DeleteFlight_RemovesFlightFromDatabase()
    {
        await _flightWriter.DeleteFlight(1);

        var result = await _context.Flights.FindAsync(1);
        Assert.That(result, Is.Null);
    }
}