using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Rere.Core.Models.Flight;
using Rere.Core.Repositories.Flight.Accessors;
using Rere.Infrastructure.Database;
using Rere.Repositories.Flight;
using Rere.Repositories.Flight.Accessors;
using Rere.Tests.Fixtures;
using static Rere.Core.Models.Flight.FlightStatus;
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
        _flightReader = new InMemoryFlightReader(_context);

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
        var flights = TestFlightFixture.GetTestFlights();
        var result = await _flightReader.ListFlightsAsync();

        Assert.That(result, Has.Exactly(flights.Count).Items);
    }

    [Test]
    public async Task SearchAsync_FindsFlightsBySingleCriteriaWithNoMatching()
    {
        var query = new FlightSearchQuery()
        {
            SearchFlightNumbers = ["NZ359"]
        };
        var result = (await _flightReader.SearchAsync(query)).ToArray();

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Exactly(0).Items);
    }

    [Test]
    public async Task SearchAsync_FindsFlightsBySingleCriteriaWithOneMatching()
    {
        var query = new FlightSearchQuery()
        {
            SearchFlightNumbers = ["NZ421"]
        };

        var result = (await _flightReader.SearchAsync(query)).ToArray();

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Exactly(2).Items);
        Assert.That(result.First().FlightNumber, Is.EqualTo("NZ421"));
        Assert.That(result.Last().FlightNumber, Is.EqualTo("NZ421"));
    }

    [TestCase("", "", "", "", 3, new[] { Landed, InAir })]
    [TestCase("", "", "AKL", "", 2, null)]
    [TestCase("", "NZ", "WLG,AKL", "AKL,WLG", 2, null)]
    public async Task SearchAsync_FindsFlightsByMultipleCriteria(
        string flightNumbers,
        string airlines,
        string departures,
        string arrivals,
        int expectedResultCount,
        FlightStatus[]? statuses)
    {
        var query = new FlightSearchQuery();
        if (flightNumbers != string.Empty)
            foreach (var number in flightNumbers.Split(','))
                query.SearchFlightNumbers.Add(number);
        if (airlines != string.Empty)
            foreach (var airline in airlines.Split(','))
                query.SearchAirlines.Add(airline);
        if (departures != string.Empty)
            foreach (var departure in departures.Split(','))
                query.SearchDepartureAirports.Add(departure);
        if (arrivals != string.Empty)
            foreach (var arrival in arrivals.Split(','))
                query.SearchArrivalAirports.Add(arrival);
        if (statuses != null)
            foreach (var status in statuses)
                query.SearchStatuses.Add(status);


        var result = (await _flightReader.SearchAsync(query)).ToArray();

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Exactly(expectedResultCount).Items);
    }
}