using AuctionApp.Domain.DTO.AuctionDTOs;
using AuctionApp.Domain.Enteties;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionApp.Business.AuctionServices
{
    public interface IAuctionService
    {
        Task<Auction> CreateAuction(CreateAuctionDTO createAuctionDTO);
        Task<AuctionDTO> BidAuction(int userId, int auctionId, decimal ammount);
        Task CompleteAuctions();
        List<AuctionDTO> GetTimeAscAuctions();
        bool DeleteAuction(int id);
        Task<AuctionDTO> GetAuctionById(int id);

    }
}