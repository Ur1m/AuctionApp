using AuctionApp.Domain.DTO.UserDTOs;
using AuctionApp.Domain.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionApp.Infrastructure.Repositories.UserRepositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AuctionAppDbContext context) : base(context)
        {
        }

        public User GetUserByLogin(LoginDTO loginDTO)
        {
           var result =  DbSet.Where(x=>x.Username.Equals(loginDTO.Username) && x.Password.Equals(loginDTO.Password))
                              .FirstOrDefault();

            return result;
        }
    }
}