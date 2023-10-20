using AuctionApp.Domain.DTO.UserDTO;
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
            var user = _mapper.Map<ApplicationUser>(userDTO);
           
            var result = await _userManager.CreateAsync(user, userDTO.Password);

            if (result.Succeeded)
            {
                // Optionally, sign the user in after registration
                await _signInManager.SignInAsync(user, isPersistent: false);
            }

            return result;
        }
    }
}
