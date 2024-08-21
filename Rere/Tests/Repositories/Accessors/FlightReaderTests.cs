using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Rere.Core.Repositories.Flight.Accessors;
using Rere.Infrastructure.Database;
using Rere.Repositories.Flight;
using Rere.Repositories.Flight.Accessors;
using FlightModel = Rere.Core.Models.Flight.Flight;

namespace Rere.Tests.Repositories.Accessors;

[TestFixture]
public class FlightReaderTests
{
    private RereDbContext _context;
    private IFlightReader _flightReader;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<RereDbContext>()
            .UseInMemoryDatabase("TestDatabase")
            .Options;

        _context = new RereDbContext(options);
        _flightReader = new InMemoryFlightReader();

        _context.Flights.AddRange(new List<FlightModel>
        {
            new() { Id = 1, FlightNumber = "FL001" },
            new() { Id = 2, FlightNumber = "FL002" }
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
    public async Task GetFlightByIdAsync_ReturnsFlight_WhenFlightExists()
    {
        var result = await _flightReader.GetFlightByIdAsync(1);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(1));
    }

    [Test]
    public async Task GetFlightByIdAsync_ReturnsNull_WhenFlightDoesNotExist()
    {
        var result = await _flightReader.GetFlightByIdAsync(999);

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task ListFlightsAsync_ReturnsAllFlights()
    {
        var result = await _flightReader.ListFlightsAsync();

        Assert.That(result, Has.Exactly(2).Items);
    }

    [Test]
    public async Task SearchAsync_FindsFlightsByCriteria()
    {
        var query = new FlightSearchQuery();

        var result = await _flightReader.SearchAsync(query);

        Assert.That(result, Has.Exactly(1).Items);
        Assert.That(result.First().FlightNumber, Is.EqualTo("FL001"));
    }
}