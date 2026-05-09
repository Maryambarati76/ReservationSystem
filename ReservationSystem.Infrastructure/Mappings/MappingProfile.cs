using AutoMapper;
using ReservationSystem.Core.Entities;
using ReservationSystem.Core.DTOs;

namespace ReservationSystem.Infrastructure.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Resource, ResourceDto>().ReverseMap();
        CreateMap<Reservation, ReservationResponseDto>().ReverseMap();
        CreateMap<ReservationCreateDto, Reservation>().ReverseMap();
    }
}