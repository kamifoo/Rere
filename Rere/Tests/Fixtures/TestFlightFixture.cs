using Rere.Core.Models.Flight;

namespace Rere.Tests.Fixtures;

public static class TestFlightFixture
{
    public static Flight GetSingleTestFlightWithoutId()
    {
        return new Flight()
        {
            FlightNumber = "NZ402",
            Airline = "NZ",
            DepartureAirport = "WLG",
            ArrivalAirport = "AKL",
            DepartureTime = TimeProvider.System.GetUtcNow().DateTime,
            ArrivalTime = TimeProvider.System.GetUtcNow().DateTime.AddHours(1),
            Status = FlightStatus.Canceled
        };
    }

    public static IList<Flight> GetTestFlights()
    {
        return new List<Flight>
        {
            new()
            {
                Id = 1,
                FlightNumber = "NZ421",
                Airline = "NZ",
                DepartureAirport = "WLG",
                ArrivalAirport = "AKL",
                DepartureTime = TimeProvider.System.GetUtcNow().DateTime,
                ArrivalTime = TimeProvider.System.GetUtcNow().DateTime.AddHours(1),
                Status = FlightStatus.Scheduled
            },
            new()
            {
                Id = 2,
                FlightNumber = "CA783",
                Airline = "CA",
                DepartureAirport = "PEK",
                ArrivalAirport = "AKL",
                DepartureTime = TimeProvider.System.GetUtcNow().DateTime,
                ArrivalTime = TimeProvider.System.GetUtcNow().DateTime.AddHours(1),
                Status = FlightStatus.InAir
            },
            new()
            {
                Id = 3,
                FlightNumber = "CA784",
                Airline = "CA",
                DepartureAirport = "AKL",
                ArrivalAirport = "PEK",
                DepartureTime = TimeProvider.System.GetUtcNow().DateTime,
                ArrivalTime = TimeProvider.System.GetUtcNow().DateTime.AddHours(1),
                Status = FlightStatus.Delayed
            },
            new()
            {
                Id = 4,
                FlightNumber = "3C8890",
                Airline = "3C",
                DepartureAirport = "PEK",
                ArrivalAirport = "CTU",
                DepartureTime = TimeProvider.System.GetUtcNow().DateTime,
                ArrivalTime = TimeProvider.System.GetUtcNow().DateTime.AddHours(1),
                Status = FlightStatus.Landed
            },
            new()
            {
                Id = 5,
                FlightNumber = "NZ421",
                Airline = "NZ",
                DepartureAirport = "AKL",
                ArrivalAirport = "WLG",
                DepartureTime = TimeProvider.System.GetUtcNow().DateTime,
                ArrivalTime = TimeProvider.System.GetUtcNow().DateTime.AddHours(1),
                Status = FlightStatus.Landed
            }
        };
    }
}