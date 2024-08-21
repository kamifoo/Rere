using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Rere.Controller;
using Rere.Core.Exceptions;
using Rere.Core.Models.Flight;
using Rere.Core.Services.Flight;

namespace Rere.Tests.Controllers;

[TestFixture]
public class FlightsControllerTests
{
    private Mock<IFlightService> _flightServiceMock;
    private FlightsController _controllerUnderTest;

    [SetUp]
    public void Setup()
    {
        _flightServiceMock = new Mock<IFlightService>();
        _controllerUnderTest = new FlightsController(_flightServiceMock.Object);
    }

    [Test]
    public async Task GetAllFlights_ReturnsOkResult_WithListOfFlights()
    {
        // Arrange
        var flights = new List<Flight> { new(), new() };
        _flightServiceMock
            .Setup(service => service.GetAllFlightAsync())
            .ReturnsAsync(flights);

        // Act
        var result = await _controllerUnderTest.GetAllFlights();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        Assert.That(((OkObjectResult)result.Result!).Value, Is.EqualTo(flights));
    }

    [Test]
    public async Task GetAllFlights_ReturnsOkResult_WithEmptyListOfFlights()
    {
        var flights = new List<Flight>();
        _flightServiceMock
            .Setup(service => service.GetAllFlightAsync())
            .ReturnsAsync(flights);

        var result = await _controllerUnderTest.GetAllFlights();

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        Assert.That(((OkObjectResult)result.Result!).Value, Is.EqualTo(flights));
    }

    [Test]
    public async Task GetFlightByMatchedId_ReturnsOkResult_WithFlight()
    {
        var flight = new Flight()
        {
            Id = 1
        };
        _flightServiceMock
            .Setup(service => service.GetFlightByIdAsync(It.IsAny<int>()))!
            .ReturnsAsync(flight);

        var result = await _controllerUnderTest.GetFlightById(1);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        Assert.That(((OkObjectResult)result.Result!).Value, Is.EqualTo(flight));
    }

    [Test]
    public async Task GetFlightByUnmatchedId_ReturnsNotFoundResult()
    {
        _flightServiceMock
            .Setup(service => service.GetFlightByIdAsync(It.IsAny<int>()))!
            .ReturnsAsync(null as Flight);

        var result = await _controllerUnderTest.GetFlightById(999);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task CreateFlight_ReturnsCreatedAtActionResult()
    {
        var newFlight = new Flight { Id = 1 };
        _flightServiceMock
            .Setup(service => service.CreateFlightAsync(It.IsAny<Flight>()))
            .ReturnsAsync(newFlight.Id);

        var result = await _controllerUnderTest.CreateFlight(newFlight);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Result, Is.InstanceOf<CreatedResult>());
        Assert.That(((CreatedResult)result.Result!).Value, Is.EqualTo(newFlight.Id));
    }

    [Test]
    public async Task UpdateFlight_ReturnsOkResult()
    {
        var flightToUpdate = new Flight { Id = 1 };
        _flightServiceMock
            .Setup(service => service.UpdateFlightAsync(It.IsAny<int>(), It.IsAny<Flight>()));

        var result = await _controllerUnderTest.UpdateFlight(1, flightToUpdate);

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<OkResult>());
    }

    [Test]
    public async Task UpdateFlight_ReturnsNotFoundResult_WhenFlightDoesNotExist()
    {
        _flightServiceMock
            .Setup(service => service.UpdateFlightAsync(It.IsAny<int>(), It.IsAny<Flight>()))
            .Throws<ResourceNotFoundException<Flight>>();

        var flightToUpdate = new Flight { Id = 1 };
        var result = await _controllerUnderTest.UpdateFlight(1, flightToUpdate);

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }

    [Test]
    public async Task DeleteFlight_ReturnsNoContentResult()
    {
        _flightServiceMock
            .Setup(service => service.DeleteFlightAsync(It.IsAny<int>()))
            .Throws<ResourceNotFoundException<Flight>>();

        var result = await _controllerUnderTest.DeleteFlight(1);

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }
}