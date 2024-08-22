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
            DepartureTime = new DateTime(2023, 12, 5, 10, 0, 0, DateTimeKind.Utc),
            ArrivalTime = new DateTime(2023, 12, 5, 14, 0, 0, DateTimeKind.Utc),
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
                DepartureTime = new DateTime(2024, 8, 19, 10, 0, 0),
                ArrivalTime = new DateTime(2024, 8, 19, 18, 0, 0),
                Status = FlightStatus.Scheduled
            },
            new()
            {
                Id = 2,
                FlightNumber = "CA783",
                Airline = "CA",
                DepartureAirport = "PEK",
                ArrivalAirport = "AKL",
                DepartureTime = new DateTime(2024, 8, 20, 14, 30, 0),
                ArrivalTime = new DateTime(2024, 8, 20, 17, 30, 0),
                Status = FlightStatus.InAir
            },
            new()
            {
                Id = 3,
                FlightNumber = "CA784",
                Airline = "CA",
                DepartureAirport = "AKL",
                ArrivalAirport = "PEK",
                DepartureTime = new DateTime(2024, 8, 21, 7, 15, 0),
                ArrivalTime = new DateTime(2024, 8, 21, 9, 15, 0),
                Status = FlightStatus.Delayed
            },
            new()
            {
                Id = 4,
                FlightNumber = "3C8890",
                Airline = "3C",
                DepartureAirport = "PEK",
                ArrivalAirport = "CTU",
                DepartureTime = new DateTime(2024, 8, 22, 16, 45, 0),
                ArrivalTime = new DateTime(2024, 8, 22, 18, 45, 0),
                Status = FlightStatus.Canceled
            },
            new()
            {
                Id = 5,
                FlightNumber = "NZ421",
                Airline = "NZ",
                DepartureAirport = "AKL",
                ArrivalAirport = "WLG",
                DepartureTime = new DateTime(2024, 8, 23, 12, 0, 0),
                ArrivalTime = new DateTime(2024, 8, 23, 13, 15, 0),
                Status = FlightStatus.Landed
            }
        };
    }
}