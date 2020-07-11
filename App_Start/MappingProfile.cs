using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VidlyStore.Dtos;
using VidlyStore.Models;

namespace VidlyStore.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Domaint to DTO
            Mapper.CreateMap<Customer, CustomerDto>();
            Mapper.CreateMap<Movie, MovieDto>();
            Mapper.CreateMap<MemberShipType, MemberShipTypeDto>();
            Mapper.CreateMap<User, UserDto>();
            Mapper.CreateMap<Appointment, AppointmentDto>();
            //Mapper.CreateMap<Rental, NewRentalDto>();

            //HTTPPUT throw error 
            //Property ‘Id’ is part of object’s key information and cannot be modified
            //to redolve which .ForMember clause
            Mapper.CreateMap<CustomerDto, Customer>().ForMember(c => c.Id, opt => opt.Ignore());
            Mapper.CreateMap<MovieDto, Movie>().ForMember(m => m.Id, opt => opt.Ignore());
            Mapper.CreateMap<UserDto, User>().ForMember(m => m.Id, opt => opt.Ignore());
            Mapper.CreateMap<AppointmentDto, Appointment>().ForMember(m => m.Id, opt => opt.Ignore());
           // Mapper.CreateMap<MemberShipTypeDto, MemberShipType>();
        }
    }
}