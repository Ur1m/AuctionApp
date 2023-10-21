using AuctionApp.Domain.Enteties;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionApp.Domain.DTO.AuctionDTOs
{
    public class CreateAuctionDTO
    {
        public string ProductName { get; set; }
        public decimal StartingBid { get; set; }
        public string Description { get; set; }
        public DateTime EndDate { get; set; }
        public int? BidderUserId { get; set; }
        public int UserId { get; set; }
    }
}
