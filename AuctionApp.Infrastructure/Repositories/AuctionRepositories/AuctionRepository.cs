using AuctionApp.Domain.Enteties;
using AuctionApp.Infrastructure.Repositories.UserRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AuctionApp.Infrastructure.Repositories.AuctionRepositories
{
    public class AuctionRepository : GenericRepository<Auction>, IAuctionRepository
    {
        public AuctionRepository(AuctionAppDbContext context) : base(context)
        {
        }

        public List<Auction> GetTimeAscAuctions()
        {
            var result = DbSet.Where(x => x.EndDate > DateTime.Now)  
                              .OrderBy(x => x.EndDate)      
                              .ToList();
            return result;
        }
    }
}
