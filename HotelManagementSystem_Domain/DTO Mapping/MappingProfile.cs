using AutoMapper;
using HotelManagementSystem_Domain.Data.DTO;
using HotelManagementSystem_Domain.Data;

namespace HotelManagementSystem_Domain.DTO_Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserDTO>().ReverseMap();
            CreateMap<Country, CountryDTO>().ReverseMap();
            CreateMap<State, StateDTO>().ReverseMap();
            CreateMap<City, CityDTO>().ReverseMap();
            CreateMap<BookingModel, BookingModelDTO>().ReverseMap();
            CreateMap<HotelModel, HotelModelDTO>().ReverseMap();
            CreateMap<RatingModel, RatingModelDTO>().ReverseMap();
            CreateMap<ReviewModel, ReviewModelDTO>().ReverseMap();
            CreateMap<RoomTypeModel, RoomTypeModelDTO>().ReverseMap();
            CreateMap<Picturemenul, pictureDto>().ReverseMap(); 

        }




    }
}
