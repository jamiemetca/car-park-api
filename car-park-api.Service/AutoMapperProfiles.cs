using AutoMapper;
using car_park_api.Persistance.Models;
using car_park_api.Service.DTOs;
using car_park_api.Service.DTOs.CarParks;
using car_park_api.Service.DTOs.Reservations;

namespace car_park_api.Service
{
    public sealed class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Reservation, ReservationDTO>();

            CreateMap<UpdateReservationDTO, Reservation>()
                .ForMember(dest => dest.DateFrom,
                opt => opt.MapFrom(src => DateOnly.Parse(src.DateFrom)))
            .ForMember(dest => dest.DateTo,
                opt => opt.MapFrom(src => DateOnly.Parse(src.DateTo)))
            .ReverseMap();

            CreateMap<CarPark, CarParkDTO>();

            CreateMap<CreateReservationDTO, CarParkAvailabilityRequestDTO>();

            CreateMap<UpdateReservationDTO, CarParkAvailabilityRequestDTO>()
                .ReverseMap();
        }
    }
}
