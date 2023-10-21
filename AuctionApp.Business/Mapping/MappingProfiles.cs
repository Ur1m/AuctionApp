using AuctionApp.Domain.DTO.AuctionDTOs;
using AuctionApp.Domain.DTO.UserDTOs;
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
            CreateMap<UserDTO,ApplicationUser>().ReverseMap();
            CreateMap<CreateUserDTO, ApplicationUser>().ReverseMap();
            CreateMap<CreateAuctionDTO, Auction>().ReverseMap();
            CreateMap<CreateAuctionDTO, AuctionDTO>().ReverseMap();
            CreateMap<Auction, AuctionDTO>().ReverseMap();
        }
        
    }
}
