using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Rere.Controllers;
using Rere.Core.Exceptions;
using Rere.Core.Models.Flight;
using Rere.Core.Repositories;
using Rere.Core.Services.Flight;
using Rere.DTOs.Flight;
using Rere.Repository.Flight;

namespace Rere.Tests.Controllers;

[TestFixture]
public class FlightsControllerTests
{
    private Mock<IFlightService> _flightServiceMock;
    private Mock<IMapper> _flightMapperMock;
    private Mock<ILogger<FlightsController>> _flightControllerLogger;
    private FlightsController _controllerUnderTest;

    [SetUp]
    public void Setup()
    {
        _flightServiceMock = new Mock<IFlightService>();
        _flightMapperMock = new Mock<IMapper>();
        _flightControllerLogger = new Mock<ILogger<FlightsController>>();
        _controllerUnderTest =
            new FlightsController(_flightControllerLogger.Object, _flightServiceMock.Object, _flightMapperMock.Object);
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
        Assert.That(((OkObjectResult)result.Result!).Value, Has.Exactly(2).Items);
    }

    [Test]
    public async Task GetAllFlights_ReturnsOkResult_WithEmptyListOfFlights()
    {
        var flights = new List<Flight>();
        _flightServiceMock
            .Setup(service => service.GetAllFlightAsync())
            .ReturnsAsync(flights);
        _flightMapperMock
            .Setup(mapper => mapper.Map<Flight, FlightDto>(It.IsAny<Flight>()))
            .Returns(new FlightDto());

        var result = await _controllerUnderTest.GetAllFlights();

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        Assert.That(((OkObjectResult)result.Result!).Value, Has.Exactly(0).Items);
    }

    [Test]
    public async Task GetFlightByMatchedId_ReturnsOkResult_WithFlight()
    {
        var flight = new Flight()
        {
            Id = 1
        };
        _flightServiceMock
            .Setup(service => service.GetFlightByIdOrNullAsync(It.IsAny<int>()))!
            .ReturnsAsync(flight);
        _flightMapperMock
            .Setup(mapper => mapper.Map<Flight, FlightDto>(It.IsAny<Flight>()))
            .Returns(new FlightDto());

        var result = await _controllerUnderTest.GetFlightById(1);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        Assert.That(((OkObjectResult)result.Result!).Value, Is.Not.Null);
    }

    [Test]
    public async Task GetFlightByUnmatchedId_ReturnsNotFoundResult()
    {
        _flightServiceMock
            .Setup(service => service.GetFlightByIdOrNullAsync(It.IsAny<int>()))!
            .ReturnsAsync(null as Flight);

        var result = await _controllerUnderTest.GetFlightById(999);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Result, Is.InstanceOf<NotFoundObjectResult>());
    }

    [Test]
    public async Task CreateFlight_ReturnsCreatedAtActionResult()
    {
        var newFlight = new FlightCreationDto() { };
        _flightServiceMock
            .Setup(service => service.CreateFlightAsync(It.IsAny<Flight>()))
            .ReturnsAsync(1);

        var result = await _controllerUnderTest.CreateFlight(newFlight);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Result, Is.InstanceOf<CreatedResult>());
        Assert.That(((CreatedResult)result.Result!).Value, Is.EqualTo(1));
    }

    [Test]
    public async Task UpdateFlight_ReturnsOkResult()
    {
        var flightToUpdate = new FlightUpdateDto() { Id = 1 };
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

        var flightToUpdate = new FlightUpdateDto() { Id = 1 };
        var result = await _controllerUnderTest.UpdateFlight(1, flightToUpdate);

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
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

    [Test]
    public async Task SearchFlights_ReturnsOkResult()
    {
        _flightMapperMock
            .Setup(mapper => mapper.Map<FlightSearchParams, FlightSearchQuery>(It.IsAny<FlightSearchParams>()))
            .Returns(new FlightSearchQuery());
        _flightServiceMock
            .Setup(service => service.SearchFlights(It.IsAny<SearchQuery<Flight>>()))
            .ReturnsAsync(new List<Flight>() { new() });

        var result = await _controllerUnderTest.SearchFlights(new FlightSearchParams());

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
    }
}