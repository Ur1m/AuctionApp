﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionApp.Domain.Enteties
{
    public class User 
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public decimal Budged { get; set; }
    }
}
