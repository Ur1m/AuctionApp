using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionApp.Domain.DTO.UserDTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public decimal Budged { get; set; }

    }
}
