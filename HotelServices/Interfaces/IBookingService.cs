using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelServices.Interfaces
{
    public interface IBookingService
    {
        Task<IEnumerable<Booking>> GetAllBookingsAsync(string userId);
        Task<Booking?> GetBookingByIdAsync(int id, string userId);
        Task<Booking> CreateBookingAsync(Booking booking, string userId);
        Task<bool> UpdateBookingAsync(int id, Booking updatedBooking, string userId);
        Task<bool> DeleteBookingAsync(int id , string userId);
    }
}
