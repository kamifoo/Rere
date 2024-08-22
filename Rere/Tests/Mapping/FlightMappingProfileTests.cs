using AutoMapper;
using NUnit.Framework;
using Rere.Core.Models.Flight;
using Rere.DTOs.Flight;

namespace Rere.Tests.Mapping;

[TestFixture]
public class FlightMappingProfileTests
{
    private IMapper _mapper;

    [SetUp]
    public void Setup()
    {
        var config = new MapperConfiguration(config => config.AddProfile<FlightMappingProfile>());
        _mapper = config.CreateMapper();
    }

    [Test]
    public void Should_Map_CreateFlightDto_To_Flight()
    {
        var dto = new CreateFlightDto()
        {
            FlightNumber = "NZ422",
            Airline = "NZ",
            DepartureAirport = "WLG",
            ArrivalAirport = "ALK",
            DepartureTime = TimeProvider.System.GetUtcNow().DateTime,
            ArrivalTime = TimeProvider.System.GetUtcNow().DateTime.AddHours(5),
            Status = "Scheduled"
        };

        var model = _mapper.Map<Flight>(dto);

        Assert.That(model.FlightNumber, Is.EqualTo(dto.FlightNumber));
        Assert.That(model.Airline, Is.EqualTo(dto.Airline));
        Assert.That(model.DepartureAirport, Is.EqualTo(dto.DepartureAirport));
        Assert.That(model.ArrivalAirport, Is.EqualTo(dto.ArrivalAirport));
        Assert.That(model.DepartureTime, Is.EqualTo(dto.DepartureTime));
        Assert.That(model.ArrivalTime, Is.EqualTo(dto.ArrivalTime));
        Assert.That(model.Status, Is.EqualTo(FlightStatus.Scheduled));
    }

    [Test]
    public void Should_Map_UpdateFlightDto_To_Flight()
    {
        var dto = new UpdateFlightDto()
        {
            Id = 1,
            FlightNumber = "NZ402",
            Airline = "NZ",
            DepartureAirport = "WLG",
            ArrivalAirport = "ALK",
            DepartureTime = TimeProvider.System.GetUtcNow().DateTime,
            ArrivalTime = TimeProvider.System.GetUtcNow().DateTime.AddHours(5),
            Status = FlightStatus.Scheduled
        };

        var model = _mapper.Map<Flight>(dto);

        Assert.That(model.Id, Is.EqualTo(dto.Id));
        Assert.That(model.FlightNumber, Is.EqualTo(dto.FlightNumber));
        Assert.That(model.Airline, Is.EqualTo(dto.Airline));
        Assert.That(model.DepartureAirport, Is.EqualTo(dto.DepartureAirport));
        Assert.That(model.ArrivalAirport, Is.EqualTo(dto.ArrivalAirport));
        Assert.That(model.DepartureTime, Is.EqualTo(dto.DepartureTime));
        Assert.That(model.ArrivalTime, Is.EqualTo(dto.ArrivalTime));
        Assert.That(model.Status, Is.EqualTo(dto.Status));
    }

    [Test]
    public void Should_Map_Flight_To_GetFlightDto()
    {
        var flight = new Flight()
        {
            Id = 1,
            FlightNumber = "NZ402",
            Airline = "NZ",
            DepartureAirport = "WLG",
            ArrivalAirport = "ALK",
            DepartureTime = TimeProvider.System.GetUtcNow().DateTime,
            ArrivalTime = TimeProvider.System.GetUtcNow().DateTime.AddHours(5),
            Status = FlightStatus.Scheduled
        };

        var dto = _mapper.Map<Flight, GetFlightDto>(flight);

        Assert.That(dto.Id, Is.EqualTo(flight.Id));
        Assert.That(dto.FlightNumber, Is.EqualTo(flight.FlightNumber));
        Assert.That(dto.Airline, Is.EqualTo(flight.Airline));
        Assert.That(dto.DepartureAirport, Is.EqualTo(flight.DepartureAirport));
        Assert.That(dto.ArrivalAirport, Is.EqualTo(flight.ArrivalAirport));
        Assert.That(dto.DepartureTime, Is.EqualTo(flight.DepartureTime));
        Assert.That(dto.ArrivalTime, Is.EqualTo(flight.ArrivalTime));
        Assert.That(dto.Status, Is.EqualTo("Scheduled"));
    }
}