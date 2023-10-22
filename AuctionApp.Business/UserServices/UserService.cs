using AuctionApp.Domain.Enteties;
using AuctionApp.Infrastructure.Repositories.UserRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AuctionApp.Business.UserServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetUserById(int id)
        {
            
            var result = await  _userRepository.GetById(id);
            return result;

        } 
    }
}
