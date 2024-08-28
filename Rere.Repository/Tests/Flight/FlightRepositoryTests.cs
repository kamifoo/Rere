using Moq;
using NUnit.Framework;
using Rere.Core.Repositories.Flight;
using Rere.Core.Repositories.Flight.Accessors;
using Rere.Repository.Flight;
using FlightModel = Rere.Core.Models.Flight.Flight;


namespace Rere.Repository.Tests.Flight;

[TestFixture]
public class FlightRepositoryTests
{
    private Mock<IFlightReader> _flightReaderMock;
    private Mock<IFlightWriter> _flightWriterMock;
    private IFlightRepository _flightRepository;

    [SetUp]
    public void Setup()
    {
        _flightReaderMock = new Mock<IFlightReader>();
        _flightWriterMock = new Mock<IFlightWriter>();
        _flightRepository = new FlightRepository(_flightReaderMock.Object, _flightWriterMock.Object);
    }

    [Test]
    public async Task ListAsync_ReturnsAllFlights()
    {
        var flights = new List<FlightModel> { new() { Id = 1 }, new() { Id = 2 } };
        _flightReaderMock.Setup(reader => reader.ListFlightsAsync()).ReturnsAsync(flights);

        var result = await _flightRepository.ListAsync();

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Exactly(2).Items);
    }

    [Test]
    public async Task ExistsAsync_ReturnsTrue_WhenFlightExists()
    {
        _flightReaderMock.Setup(reader => reader.ExistsAsync(1)).ReturnsAsync(true);

        var exists = await _flightRepository.ExistsAsync(1);

        Assert.That(exists, Is.True);
    }

    [Test]
    public async Task ExistsAsync_ReturnsFalse_WhenFlightDoesNotExist()
    {
        _flightReaderMock.Setup(reader => reader.GetFlightByIdAsync(999)).ReturnsAsync((FlightModel)null);

        var exists = await _flightRepository.ExistsAsync(999);

        Assert.That(exists, Is.False);
    }

    [Test]
    public async Task GetByIdOrNullAsync_ReturnsFlight_WhenFlightExists()
    {
        var flight = new FlightModel { Id = 1 };
        _flightReaderMock.Setup(reader => reader.GetFlightByIdAsync(1)).ReturnsAsync(flight);
        _flightReaderMock.Setup(reader => reader.ExistsAsync(1)).ReturnsAsync(true);

        var result = await _flightRepository.GetByIdOrNullAsync(1);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(1));
    }

    [Test]
    public async Task GetByIdOrNullAsync_ReturnsNull_WhenFlightDoesNotExist()
    {
        _flightReaderMock.Setup(reader => reader.GetFlightByIdAsync(999)).ReturnsAsync((FlightModel)null);

        var result = await _flightRepository.GetByIdOrNullAsync(999);

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task AddAsync_CallsWriterToAddFlight_AndReturnsFlightId()
    {
        var flight = new FlightModel { Id = 3, FlightNumber = "FL003" };
        _flightWriterMock.Setup(writer => writer.AddFlight(flight)).ReturnsAsync(flight.Id);

        var resultId = await _flightRepository.AddAsync(flight);

        Assert.That(resultId, Is.EqualTo(3));
        _flightWriterMock.Verify(writer => writer.AddFlight(flight), Times.Once);
    }

    [Test]
    public async Task UpdateAsync_CallsWriterToUpdateFlight()
    {
        var flight = new FlightModel { Id = 1, FlightNumber = "FL001-Updated" };
        _flightWriterMock.Setup(writer => writer.UpdateFlight(flight)).Returns(Task.CompletedTask);

        await _flightRepository.UpdateAsync(flight);

        _flightWriterMock.Verify(writer => writer.UpdateFlight(flight), Times.Once);
    }

    [Test]
    public async Task DeleteAsync_CallsWriterToDeleteFlight()
    {
        var flight = new FlightModel { Id = 1 };
        _flightWriterMock.Setup(writer => writer.DeleteFlight(flight.Id)).Returns(Task.CompletedTask);

        await _flightRepository.DeleteAsync(flight.Id);

        _flightWriterMock.Verify(writer => writer.DeleteFlight(flight.Id), Times.Once);
    }

    [Test]
    public async Task SearchAsync_CallsReaderToSearchFlights()
    {
        var flights = new List<FlightModel> { new() { Id = 1, FlightNumber = "FL001" } };
        var query = new FlightSearchQuery();

        _flightReaderMock.Setup(reader => reader.SearchFlightsAsync(query)).ReturnsAsync(flights);

        var result = await _flightRepository.SearchAsync(query);

        Assert.That(result, Has.Exactly(1).Items);
        Assert.That(result.First().FlightNumber, Is.EqualTo("FL001"));
    }
}