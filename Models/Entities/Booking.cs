using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Booking
    {
        public int BookingId { get; set; }
        public string UserId { get; set; }
        public ApplicationUser? User { get; set; } 
        public int RoomId { get; set; }
        public Room? Room { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
      
    }
}
