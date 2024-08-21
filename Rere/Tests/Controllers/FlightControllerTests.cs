using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Rere.Controller;
using Rere.Core.Models.Flight;
using Rere.Core.Services.Flight;

namespace Rere.Tests.Controllers;

[TestFixture]
public class FlightControllerTests
{
    private Mock<IFlightService> _flightServiceMock;
    private FlightController _controllerUnderTest;

    [SetUp]
    public void Setup()
    {
        _flightServiceMock = new Mock<IFlightService>();
        _controllerUnderTest = new FlightController();
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
        Assert.That(result, Is.InstanceOf<OkObjectResult>());
        Assert.That(result.Value, Is.SameAs(flights));
    }
}