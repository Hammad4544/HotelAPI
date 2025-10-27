using Models.DTOS.Booking;
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
        Task<Booking> CreateBookingAsync(CreateBookingDTO Dto, string userId);
        Task<bool> UpdateBookingAsync(int id, UpdateBookingDto updatedBooking, string userId);
        Task<bool> DeleteBookingAsync(int id , string userId);
        Task<IEnumerable<Booking>> GetBookingsByGuestId(string guestId);
        Task<IEnumerable<Booking>> GetActiveBookings();
    }
}
