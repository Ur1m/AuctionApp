using AuctionApp.Domain.DTO.UserDTOs;
using System;

namespace AuctionApp.Domain.DTO.AuctionDTOs
{
    public class AuctionDTO
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal StartingBid { get; set; }
        public string Description { get; set; }
        public DateTime EndDate { get; set; }
        public int? BidderUserId { get; set; }
        public int Status { get; set; }
        public int UserId { get; set; }
        public UserDTO User { get; set; }
    }
}
