using Rere.Core.Models.Flight;

namespace Rere.Tests.Fixtures;

public static class TestFlightFixture
{
    public static Flight GetSingleTestFlightWithoutId()
    {
        return new Flight()
        {
            FlightNumber = "AF567",
            Airline = "AF",
            DepartureAirport = "CDG",
            ArrivalAirport = "JFK",
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
                FlightNumber = "AA123",
                Airline = "AA",
                DepartureAirport = "LAX",
                ArrivalAirport = "JFK",
                DepartureTime = new DateTime(2024, 8, 19, 10, 0, 0),
                ArrivalTime = new DateTime(2024, 8, 19, 18, 0, 0),
                Status = FlightStatus.Scheduled
            },
            new()
            {
                Id = 2,
                FlightNumber = "BA456",
                Airline = "BA",
                DepartureAirport = "LHR",
                ArrivalAirport = "ORD",
                DepartureTime = new DateTime(2024, 8, 20, 14, 30, 0),
                ArrivalTime = new DateTime(2024, 8, 20, 17, 30, 0),
                Status = FlightStatus.InAir
            },
            new()
            {
                Id = 3,
                FlightNumber = "DL789",
                Airline = "DL",
                DepartureAirport = "ATL",
                ArrivalAirport = "MIA",
                DepartureTime = new DateTime(2024, 8, 21, 7, 15, 0),
                ArrivalTime = new DateTime(2024, 8, 21, 9, 15, 0),
                Status = FlightStatus.Delayed
            },
            new()
            {
                Id = 4,
                FlightNumber = "UA101",
                Airline = "UA",
                DepartureAirport = "SFO",
                ArrivalAirport = "SEA",
                DepartureTime = new DateTime(2024, 8, 22, 16, 45, 0),
                ArrivalTime = new DateTime(2024, 8, 22, 18, 45, 0),
                Status = FlightStatus.Canceled
            },
            new()
            {
                Id = 5,
                FlightNumber = "SW202",
                Airline = "SW",
                DepartureAirport = "LAS",
                ArrivalAirport = "PHX",
                DepartureTime = new DateTime(2024, 8, 23, 12, 0, 0),
                ArrivalTime = new DateTime(2024, 8, 23, 13, 15, 0),
                Status = FlightStatus.Landed
            }
        };
    }
}