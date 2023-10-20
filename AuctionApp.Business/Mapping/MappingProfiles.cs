using AuctionApp.Domain.DTO.UserDTO;
using AuctionApp.Domain.Enteties;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionApp.Business.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User,UserDTO>().ReverseMap();
            CreateMap<User,CreateUserDTO>().ReverseMap();
        }
        
    }
}
