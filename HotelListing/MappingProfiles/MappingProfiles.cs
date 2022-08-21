using AutoMapper;
using HotelListing.DTOs;
using HotelListing.Models;

namespace HotelListing.MappingProfiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<Country, CreateCountryDto>().ReverseMap();
            CreateMap<Country, UpdateCountryDto>().ReverseMap();
            CreateMap<Hotel, HotelDto>().ReverseMap();
            CreateMap<Hotel, CreateHotelDto>().ReverseMap();
            CreateMap<Hotel, UpdateHotelDto>().ReverseMap();
            CreateMap<ApiUser, UserDto>().ReverseMap();
            CreateMap<ApiUser, LoginDto>().ReverseMap();
        }
    }
}
