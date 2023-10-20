﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AuctionApp.Business.AccountServices
{
    public interface IAccountService
    {
        Task<IdentityResult> RegisterAsync(string firstName, string lastName, string username, string password);
    }
}