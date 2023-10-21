using AuctionApp.Domain.Enteties;
using System.Collections.Generic;

namespace AuctionApp.Infrastructure.Repositories.AuctionRepositories
{
    public interface IAuctionRepository : IGenericRepository<Auction>
    {
        List<Auction> GetTimeAscAuctions();

    }
}