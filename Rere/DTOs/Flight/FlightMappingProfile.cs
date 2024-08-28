using AutoMapper;
using Rere.Core.Models.Flight;
using Rere.Repository.Flight;
using FlightModel = Rere.Core.Models.Flight.Flight;

namespace Rere.DTOs.Flight;

public class FlightMappingProfile : Profile
{
    public FlightMappingProfile()
    {
        // Mapping Flight Creation DTO to Flight Model
        CreateMap<FlightCreationDto, FlightModel>();
        // Mapping Flight Update DTO to Flight model
        CreateMap<FlightUpdateDto, FlightModel>();
        // Mapping Flight Model to Flight DTO
        CreateMap<FlightModel, FlightDto>()
            .ForMember(flightDto => flightDto.Status,
                opt =>
                    opt.MapFrom(flight => flight.Status.ToString()));
        // Mapping Flight Search Params to Flight Search Query
        CreateMap<FlightSearchParams, FlightSearchQuery>()
            .ForMember(query => query.SearchIds,
                opt => opt.MapFrom(param =>
                    param.Id!.Trim().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(int.Parse)
                        .Where(intId => intId > 0)
                        .ToList()
                ))
            .ForMember(query => query.SearchFlightNumbers,
                opt => opt.MapFrom(param =>
                    param.FlightNumber!.Trim().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .ToList()
                ))
            .ForMember(query => query.SearchAirlines,
                opt => opt.MapFrom(param =>
                    param.Airline!.Trim().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .ToList()
                ))
            .ForMember(query => query.SearchDepartureAirports,
                opt => opt.MapFrom(param =>
                    param.DepartureAirport!.Trim().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .ToList()
                ))
            .ForMember(query => query.SearchArrivalAirports,
                opt => opt.MapFrom(param =>
                    param.ArrivalAirport!.Trim().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .ToList()
                ))
            .ForMember(query => query.SearchStatuses,
                opt => opt.MapFrom(param =>
                    param.Status!.Trim().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(status => (FlightStatus)Enum.Parse(typeof(FlightStatus), status, true))
                        .ToList()
                ))
            .ForMember(query => query.SearchDepartureTime,
                opt => opt.MapFrom(param =>
                    param.DepartureTime
                ))
            .ForMember(query => query.SearchArrivalTime,
                opt => opt.MapFrom(param =>
                    param.ArrivalTime
                ));
    }
}