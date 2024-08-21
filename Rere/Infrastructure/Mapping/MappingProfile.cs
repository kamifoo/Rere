using AutoMapper;
using Rere.DTOs.Flight;
using FlightModel = Rere.Core.Models.Flight.Flight;

namespace Rere.Infrastructure.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateFlightDto, FlightModel>();
        CreateMap<UpdateFlightDto, FlightModel>();
    }
}