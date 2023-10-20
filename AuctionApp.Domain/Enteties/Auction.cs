using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionApp.Domain.Enteties
{
    public class Auction
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal StartingBid { get; set; }
        public string Description { get; set; }
        public DateTime EndDate { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
