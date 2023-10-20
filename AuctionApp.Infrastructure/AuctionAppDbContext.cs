using AuctionApp.Domain.Enteties;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionApp.Infrastructure
{
    public class AuctionAppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AuctionAppDbContext(DbContextOptions<AuctionAppDbContext> options) : base(options) { }
        
        public DbSet<User> User { get; set; }
        public DbSet<Auction> Auctions { get; set; }


    }
}
