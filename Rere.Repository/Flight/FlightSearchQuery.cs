using System.Linq.Expressions;
using Rere.Core.Models.Flight;
using Rere.Core.Repositories;
using Rere.Extension;
using FlightModel = Rere.Core.Models.Flight.Flight;

namespace Rere.Repository.Flight;

public class FlightSearchQuery : SearchQuery<FlightModel>
{
    public List<int> SearchIds { get; set; } = [];
    public List<string> SearchFlightNumbers { get; set; } = [];
    public List<string> SearchAirlines { get; set; } = [];
    public List<string> SearchDepartureAirports { get; set; } = [];
    public List<string> SearchArrivalAirports { get; set; } = [];
    public DateTime? SearchDepartureTime { get; set; }
    public DateTime? SearchArrivalTime { get; set; }
    public List<FlightStatus> SearchStatuses { get; set; } = [];

    public override Expression<Func<FlightModel, bool>> SearchCriteria => BuildCriteria();

    private Expression<Func<FlightModel, bool>> BuildCriteria()
    {
        Expression<Func<FlightModel, bool>> criteria = flight => true;

        if (SearchIds.Count > 0)
            criteria = criteria.AndAlso(flight => SearchIds.Contains(flight.Id));

        if (SearchFlightNumbers.Count > 0)
            criteria = criteria.AndAlso(flight => SearchFlightNumbers.Contains(flight.FlightNumber));

        if (SearchAirlines.Count > 0)
            criteria = criteria.AndAlso(flight => SearchAirlines.Contains(flight.Airline));

        if (SearchDepartureAirports.Count > 0)
            criteria = criteria.AndAlso(flight => SearchDepartureAirports.Contains(flight.DepartureAirport));

        if (SearchArrivalAirports.Count > 0)
            criteria = criteria.AndAlso(flight => SearchArrivalAirports.Contains(flight.ArrivalAirport));

        if (SearchDepartureTime.HasValue)
            criteria = criteria.AndAlso(flight => flight.DepartureTime >= SearchDepartureTime.Value);

        if (SearchArrivalTime.HasValue)
            criteria = criteria.AndAlso(flight => flight.DepartureTime <= SearchArrivalTime.Value);

        if (SearchStatuses.Count > 0)
            criteria = criteria.AndAlso(flight => SearchStatuses.Contains(flight.Status));

        return criteria;
    }
}