using System.ComponentModel.DataAnnotations;

namespace Rere.Core.Models.Flight;

public class Flight
{
    [Key] public int Id { get; set; }

    [Required]
    [StringLength(6, MinimumLength = 3)]
    public string FlightNumber { get; set; }

    [Required]
    [StringLength(2, MinimumLength = 2)]
    public string Airline { get; set; }

    [Required]
    [StringLength(3, MinimumLength = 3)]
    public string DepartureAirport { get; set; }

    [Required]
    [StringLength(3, MinimumLength = 3)]
    public string ArrivalAirport { get; set; }

    [Required] public DateTime DepartureTime { get; set; }

    [Required] public DateTime ArrivalTime { get; set; }

    [Required]
    [EnumDataType(typeof(FlightStatus))]
    public FlightStatus Status { get; set; }
}