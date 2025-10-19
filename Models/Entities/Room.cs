﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Room
    {
        public int RoomId { set; get; }
        public string Number { set; get; } = string.Empty;
        public string Type { set; get; } = string.Empty;
        public decimal PricePerNight { set; get; }
        public bool IsAvailable { set; get; }
        public ICollection<Booking>? Bookings { set; get; }

    }
}
