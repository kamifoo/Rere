using System.Linq.Expressions;
using Rere.Core.Models.Flight;
using Rere.Core.Repositories;
using Rere.Infrastructure.Extension;
using FlightModel = Rere.Core.Models.Flight.Flight;

namespace Rere.Repositories.Flight;

public class FlightSearchQuery : SearchQuery<FlightModel>
{
    public int[]? SearchIds { get; set; }
    public string[]? SearchFlightNumbers { get; set; }
    public string[]? SearchAirlines { get; set; }
    public string[]? SearchDepartureAirports { get; set; }
    public string[]? SearchArrivalAirports { get; set; }
    public DateTime? SearchDepartureTime { get; set; }
    public DateTime? SearchArrivalTime { get; set; }
    public FlightStatus[]? SearchStatuses { get; set; }

    public override Expression<Func<FlightModel, bool>> SearchCriteria => BuildCriteria();

    private Expression<Func<FlightModel, bool>> BuildCriteria()
    {
        Expression<Func<FlightModel, bool>> criteria = flight => true;

        if (SearchIds != null && SearchIds.Any())
            criteria = criteria.AndAlso(flight => SearchIds.Contains(flight.Id));

        if (SearchFlightNumbers != null && SearchFlightNumbers.Any())
            criteria = criteria.AndAlso(flight => SearchFlightNumbers.Contains(flight.FlightNumber));

        if (SearchAirlines != null && SearchAirlines.Any())
            criteria = criteria.AndAlso(flight => SearchAirlines.Contains(flight.Airline));

        if (SearchDepartureAirports != null && SearchDepartureAirports.Any())
            criteria = criteria.AndAlso(flight => SearchDepartureAirports.Contains(flight.DepartureAirport));

        if (SearchArrivalAirports != null && SearchArrivalAirports.Any())
            criteria = criteria.AndAlso(flight => SearchArrivalAirports.Contains(flight.ArrivalAirport));

        if (SearchDepartureTime.HasValue)
            criteria = criteria.AndAlso(flight => flight.DepartureTime >= SearchDepartureTime.Value);

        if (SearchArrivalTime.HasValue)
            criteria = criteria.AndAlso(flight => flight.DepartureTime <= SearchArrivalTime.Value);

        if (SearchStatuses != null && SearchStatuses.Any())
            criteria = criteria.AndAlso(flight => SearchStatuses.Contains(flight.Status));

        return criteria;
    }
}