using Models.DTOS.Booking;
using Models.DTOS.DashboardStats;
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
        Task<IEnumerable<BookingResponseDto>> GetAllBookingsAsync(string userId);
        Task<BookingResponseDto?> GetBookingByIdAsync(int id, string userId);
        Task<BookingResponseDto> CreateBookingAsync(CreateBookingDTO Dto, string userId);
        Task<bool> UpdateBookingAsync(int id, UpdateBookingDto updatedBooking, string userId);
        Task<bool> DeleteBookingAsync(int id , string userId);
        Task<IEnumerable<BookingResponseDto>> GetBookingsByGuestId(string guestId);
        Task<IEnumerable<BookingResponseDto>> GetActiveBookings();
        Task<DashboardStatsDto> GetStats();

        Task<IEnumerable<BookingDashboardDto>> GetLatestBookings();
    }
}
