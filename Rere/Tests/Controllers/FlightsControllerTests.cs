using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Rere.Controller;
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
}