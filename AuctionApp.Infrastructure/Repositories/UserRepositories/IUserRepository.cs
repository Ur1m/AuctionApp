using AuctionApp.Domain.DTO.UserDTOs;
using AuctionApp.Domain.Enteties;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionApp.Infrastructure.Repositories.UserRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User GetUserByLogin(LoginDTO loginDTO);

    }
}
