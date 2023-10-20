using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace AuctionApp.Domain.Enteties
{
    public class ApplicationUser : IdentityUser
    {
        public int? UserId { get; set; }
        public virtual User User { get; set; }
    }
}
