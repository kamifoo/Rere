using System.ComponentModel.DataAnnotations;
using Rere.Core.Models.Flight;

namespace Rere.DTOs.Flight;

public class CreateFlightDto
{
    [Required(ErrorMessage = "Flight number is required")]
    [StringLength(6, MinimumLength = 3,
        ErrorMessage =
            "Flight number must be between 3 and 6 characters, with first two-character airline designator and 1 to 4 digit number.")]
    public string FlightNumber { get; set; }

    [Required(ErrorMessage = "Airline IATA code is required")]
    [StringLength(2, ErrorMessage = "Airline must be 2 characters")]
    public string Airline { get; set; }

    [Required(ErrorMessage = "Departure airport IATA code is required")]
    [StringLength(3, ErrorMessage = "Departure airport must be three-letter IATA airport location identifier")]
    public string DepartureAirport { get; set; }

    [Required(ErrorMessage = "Arrival airport IATA code is required")]
    [StringLength(3, ErrorMessage = "Arrival airport must be three-letter IATA airport location identifier")]
    public string ArrivalAirport { get; set; }

    [Required(ErrorMessage = "Flight departure time is required")]
    public DateTime DepartureTime { get; set; }

    [Required(ErrorMessage = "Flight departure time is required")]
    public DateTime ArrivalTime { get; set; }

    [Required(ErrorMessage = "Flight status is required")]
    [EnumDataType(typeof(FlightStatus),
        ErrorMessage = "Flight status must be one of the following: Scheduled, Delayed, Canceled, InAir, Landed")]
    public string Status { get; set; }
}