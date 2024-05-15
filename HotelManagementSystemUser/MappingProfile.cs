﻿using AutoMapper;
using HotelManagementSystem_Domain.Data.DTO;
using HotelManagementSystem_Domain.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        }




    }
}
