using AutoMapper;
using FlightModel = Rere.Core.Models.Flight.Flight;

namespace Rere.DTOs.Flight;

public class FlightMappingProfile : Profile
{
    public FlightMappingProfile()
    {
        CreateMap<CreateFlightDto, FlightModel>();
        CreateMap<UpdateFlightDto, FlightModel>();
        CreateMap<FlightModel, GetFlightDto>()
            .ForMember(flightDto => flightDto.Status,
                opt =>
                    opt.MapFrom(flight => flight.Status.ToString()));
    }
}