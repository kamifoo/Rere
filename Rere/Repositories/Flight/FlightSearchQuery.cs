using Rere.Core.Models.Flight;
using Rere.Core.Repositories;
using FlightModel = Rere.Core.Models.Flight.Flight;

namespace Rere.Repositories.Flight;

public class FlightSearchQuery : SearchQuery<FlightModel>
{
    public int[] SearchIds { get; set; }
    public string[] SearchFlightNumbers { get; set; }
    public string[] SearchAirlines { get; set; }
    public string[] SearchDepartureAirports { get; set; }
    public string[] SearchArrivalAirports { get; set; }
    public DateTime[] SearchDepartureTimes { get; set; }
    public DateTime[] SearchArrivalTimes { get; set; }
    public FlightStatus[] SearchStatuses { get; set; }
}