using AuctionApp.Domain.DTO.UserDTOs;
using AuctionApp.Domain.Enteties;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AuctionApp.Business.AccountServices
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private IMapper _mapper;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public async Task<IdentityResult> RegisterAsync(CreateUserDTO userDTO)
        {

            if (string.IsNullOrWhiteSpace(userDTO.FirstName) ||
                string.IsNullOrWhiteSpace(userDTO.LastName) ||
                string.IsNullOrWhiteSpace(userDTO.Username) ||
                string.IsNullOrWhiteSpace(userDTO.Password))
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "InvalidInput",
                    Description = "All fields are required."
                });
            }

            if (userDTO.Username.Length < 3 || userDTO.Username.Length > 20)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "InvalidUsernameLength",
                    Description = "Username must be between 3 and 20 characters."
                });
            }

            if (userDTO.Password.Length < 8)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "InvalidPasswordLength",
                    Description = "Password must be at least 8 characters long."
                });
            }

            var user = _mapper.Map<ApplicationUser>(userDTO);
            user.User = new User
            {

                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                Username = userDTO.Username,
                Password = userDTO.Password,
                Budged = 1000
            };


            var result = await _userManager.CreateAsync(user, userDTO.Password);

            if (result.Succeeded)
            {
                // Optionally, sign the user in after registration
            }

            return result;
        }
    }
}
