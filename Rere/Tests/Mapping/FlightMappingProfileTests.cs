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
            DepartureTime = DateTime.UtcNow,
            ArrivalTime = DateTime.UtcNow.AddHours(5),
            Status = FlightStatus.Scheduled
        };

        var model = _mapper.Map<Flight>(dto);

        Assert.That(model.FlightNumber, Is.EqualTo(dto.FlightNumber));
        Assert.That(model.Airline, Is.EqualTo(dto.Airline));
        Assert.That(model.DepartureAirport, Is.EqualTo(dto.DepartureAirport));
        Assert.That(model.ArrivalAirport, Is.EqualTo(dto.ArrivalAirport));
        Assert.That(model.DepartureTime, Is.EqualTo(dto.DepartureTime));
        Assert.That(model.ArrivalTime, Is.EqualTo(dto.ArrivalTime));
        Assert.That(model.Status, Is.EqualTo(dto.Status));
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
            DepartureTime = DateTime.UtcNow,
            ArrivalTime = DateTime.UtcNow.AddHours(5),
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
}