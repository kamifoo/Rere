using Moq;
using NUnit.Framework;
using Rere.Core.Exceptions;
using Rere.Core.Models.Flight;
using Rere.Core.Services.Flight;
using Rere.Core.Repositories.Flight;
using Rere.Services.Flight;

namespace Rere.Tests.Services;

[TestFixture]
public class FlightServiceTests
{
    private Mock<IFlightRepository> _flightRepositoryMock;
    private IFlightService _flightService;

    [SetUp]
    public void Setup()
    {
        _flightRepositoryMock = new Mock<IFlightRepository>();
        _flightService = new FlightService(_flightRepositoryMock.Object);
    }

    [Test]
    public async Task GetAllFlights_ReturnsListOfFlights()
    {
        // Arrange
        var flights = new List<Flight> { new(), new() };
        _flightRepositoryMock
            .Setup(repo => repo.ListAsync())
            .ReturnsAsync(flights);

        // Act
        var result = await _flightService.GetAllFlightAsync();

        // Assert
        var flightResult = result as Flight[] ?? result.ToArray();
        Assert.That(flightResult, Is.Not.Null);
        Assert.That(flightResult.Count, Is.EqualTo(2));
    }

    [Test]
    public async Task GetFlightById_ReturnsFlight_WhenFlightExists()
    {
        var flight = new Flight { Id = 1 };
        _flightRepositoryMock
            .Setup(repo => repo.GetByIdOrNullAsync(It.IsAny<int>()))
            .ReturnsAsync(flight);

        var result = await _flightService.GetFlightByIdOrNullAsync(1);

        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Id, Is.EqualTo(1));
    }

    [Test]
    public async Task GetFlightById_ReturnsNull_WhenFlightDoesNotExist()
    {
        _flightRepositoryMock
            .Setup(repo => repo.GetByIdOrNullAsync(It.IsAny<int>()))
            .ReturnsAsync(null as Flight);

        var result = await _flightService.GetFlightByIdOrNullAsync(999);

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task CreateFlight_CallsAddAsync_AndReturnsFlight()
    {
        var flight = new Flight { Id = 1 };
        _flightRepositoryMock
            .Setup(repo => repo.AddAsync(It.IsAny<Flight>()))
            .ReturnsAsync(flight.Id);

        var result = await _flightService.CreateFlightAsync(flight);

        _flightRepositoryMock.Verify(repo => repo.AddAsync(flight), Times.Once);
        Assert.That(result, Is.EqualTo(flight.Id));
    }

    [Test]
    public Task UpdateFlight_CallsUpdateAsync_AndReturnsCompletedTask_WhenFlightExists()
    {
        var flight = new Flight { Id = 1 };

        _flightRepositoryMock
            .Setup(repo => repo.ExistsAsync(It.IsAny<int>()))
            .ReturnsAsync(true);
        _flightRepositoryMock
            .Setup(repo => repo.UpdateAsync(It.IsAny<Flight>()));

        Assert.DoesNotThrowAsync(async () => await _flightService.UpdateFlightAsync(1, flight));
        _flightRepositoryMock.Verify(repo => repo.UpdateAsync(flight), Times.Once);
        return Task.CompletedTask;
    }

    [Test]
    public Task UpdateFlight_ThrowsResourceNotFoundException_WhenFlightDoesNotExist()
    {
        var flight = new Flight { Id = 1 };

        _flightRepositoryMock
            .Setup(repo => repo.GetByIdOrNullAsync(It.IsAny<int>()))
            .ReturnsAsync(() => null);

        Assert.ThrowsAsync<ResourceNotFoundException<Flight>>(async () =>
            await _flightService.UpdateFlightAsync(1, flight));
        return Task.CompletedTask;
    }

    [Test]
    public Task DeleteFlight_CallsDeleteAsync_AndReturnsTrue_WhenFlightExists()
    {
        var flight = new Flight { Id = 1 };

        _flightRepositoryMock
            .Setup(repo => repo.ExistsAsync(It.IsAny<int>()))
            .ReturnsAsync(true);
        _flightRepositoryMock
            .Setup(repo => repo.GetByIdOrNullAsync(It.IsAny<int>()))
            .ReturnsAsync(flight);
        _flightRepositoryMock
            .Setup(repo => repo.DeleteAsync(It.IsAny<Flight>()));

        Assert.DoesNotThrowAsync(async () => await _flightService.DeleteFlightAsync(1));
        _flightRepositoryMock.Verify(repo => repo.DeleteAsync(flight), Times.Once);
        return Task.CompletedTask;
    }

    [Test]
    public Task DeleteFlight_ThrowsResourceNotFoundException_WhenFlightDoesNotExist()
    {
        _flightRepositoryMock
            .Setup(repo => repo.GetByIdOrNullAsync(It.IsAny<int>()))
            .ReturnsAsync(() => null);

        Assert.ThrowsAsync<ResourceNotFoundException<Flight>>(async () =>
            await _flightService.DeleteFlightAsync(999));
        return Task.CompletedTask;
    }
}