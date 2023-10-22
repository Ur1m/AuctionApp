using AuctionApp.Domain.DTO.AuctionDTOs;
using AuctionApp.Domain.Enteties;
using AuctionApp.Domain.Enums;
using AuctionApp.Infrastructure.Repositories.AuctionRepositories;
using AuctionApp.Infrastructure.Repositories.UserRepositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AuctionApp.Business.AuctionServices
{
    public class AuctionService : IAuctionService
    {
        public readonly IAuctionRepository _auctionRepository;
        public readonly IUserRepository _userRepository;
        public readonly IMapper _mapper;
        public AuctionService(IAuctionRepository auctionRepository, IMapper mapper, IUserRepository userRepository)
        {
            _auctionRepository = auctionRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<Auction> CreateAuction(CreateAuctionDTO createAuctionDTO)
        {
            var mappedAuction = _mapper.Map<Auction>(createAuctionDTO);

            mappedAuction.Status = (int)AuctionStatusEnum.Created;

            var result = _auctionRepository.Create(mappedAuction);

            return result;
        }

        public List<AuctionDTO> GetTimeAscAuctions()
        {
            var auctions = _auctionRepository.GetTimeAscAuctions();

            return _mapper.Map<List<AuctionDTO>>(auctions);
        }
         public List<AuctionDTO> GetAllAuctionsByUserId(int id)
        {
            var auctions = _auctionRepository.GetAllAuctionsByUserId(id);

            return _mapper.Map<List<AuctionDTO>>(auctions);
        }

        public async Task<AuctionDTO> GetAuctionById(int id)
        {
            var auctions = await _auctionRepository.GetById(id);

            return _mapper.Map<AuctionDTO>(auctions);
        }

        public bool DeleteAuction(int id)
        {
            var auctions = _auctionRepository.Delete(id);

            if(auctions != null)
            {
                return true;
            }

            return false;
        }

        public async Task<AuctionDTO> BidAuction(int userId, int auctionId, decimal ammount)
        {
            var auction = await _auctionRepository.GetById(auctionId);

            var user = await _userRepository.GetById(userId);

            if (auction.StartingBid < ammount && user.Budged >= ammount && userId != auction.UserId)
            {
                auction.BidderUserId = userId;
                auction.StartingBid = ammount;
                var updatedRes = _auctionRepository.Update(auction);
                return _mapper.Map<AuctionDTO>(updatedRes);
            }

            return null;
        }

        public async Task CompleteAuctions()
        {
            var auction = _auctionRepository.GetAllNotSoldOut();

            foreach(var item in auction)
            {
                if (item.EndDate <= DateTime.Now)
                {
                    if(item.BidderUserId != null)
                    {
                        var buyer = await _userRepository.GetById(item.BidderUserId.Value);
                        buyer.Budged -= item.StartingBid;
                        _userRepository.Update(buyer);

                        var seller = await _userRepository.GetById(item.UserId);
                        seller.Budged += item.StartingBid;
                        _userRepository.Update(seller);

                        item.Status = (int)AuctionStatusEnum.Sold;
                        _auctionRepository.Update(item);
                    }
                    else
                    {
                        item.Status = (int)AuctionStatusEnum.NotSold;
                        _auctionRepository.Update(item);
                    }
                }
            }
        }

    }
}
