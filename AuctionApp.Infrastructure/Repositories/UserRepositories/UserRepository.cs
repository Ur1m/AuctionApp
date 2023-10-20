using AuctionApp.Domain.Enteties;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionApp.Infrastructure.Repositories.UserRepositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AuctionAppDbContext context) : base(context)
        {
        }
    }
}