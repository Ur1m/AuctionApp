using AuctionApp.Domain.DTO.UserDTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AuctionApp.Business.AccountServices
{
    public interface IAccountService
    {
        Task<SignInResult> LoginAsync(LoginDTO loginDTO);
        Task<IdentityResult> RegisterAsync(CreateUserDTO userDTO);
    }
}
