using System.ComponentModel.DataAnnotations;
using Rere.Core.Models.Flight;

namespace Rere.DTOs.Flight;

public class UpdateFlightDto
{
    [Required(ErrorMessage = "Flight ID is required")]
    public int Id { get; set; }

    [StringLength(6, MinimumLength = 3,
        ErrorMessage =
            "Flight number must be between 3 and 6 characters, with first two-character airline designator and 1 to 4 digit number.")]
    public string FlightNumber { get; set; }

    [StringLength(2, ErrorMessage = "Airline must be 2 characters")]
    public string Airline { get; set; }

    [StringLength(3, ErrorMessage = "Departure airport must be three-letter IATA airport location identifier")]
    public string DepartureAirport { get; set; }

    [StringLength(3, ErrorMessage = "Arrival airport must be three-letter IATA airport location identifier")]
    public string ArrivalAirport { get; set; }

    public DateTime DepartureTime { get; set; }

    public DateTime ArrivalTime { get; set; }

    [EnumDataType(typeof(FlightStatus),
        ErrorMessage = "Flight status must be one of the following: Scheduled, Delayed, Canceled, InAir, Landed")]
    public string Status { get; set; }
}