using DataAcess.Repositories.Interfaces;
using HotelServices.Interfaces;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelServices.Implementation
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitofwork;

        public BookingService(IUnitOfWork unitOfWork) 
        {
            _unitofwork= unitOfWork;

        }
        public async Task<Booking> CreateBookingAsync(Booking booking, string userId)
        {
            booking.UserId = userId;
            await _unitofwork.Bookings.AddAsync(booking);
            await _unitofwork.CompleteAsync();
            return booking;
        }

        public async Task<bool> DeleteBookingAsync(int id, string userId)
        {
            var booking = await _unitofwork.Bookings.GetByIdAsync(id);
            if (booking == null)
            {
                return false;
            }
            _unitofwork.Bookings.Delete(booking);
            await _unitofwork.CompleteAsync();
            return true;
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsAsync(string userId)
        {
           return await _unitofwork.Bookings.GetAllAsync();

        }

        public async Task<Booking?> GetBookingByIdAsync(int id, string userId)
        {

            
          var book = await _unitofwork.Bookings.GetByIdAsync(id);
            return (book.UserId == userId) ? book : null;

        }

        public async Task<bool> UpdateBookingAsync(int id, Booking updatedBooking, string userId)
        {
            var existingBooking = await _unitofwork.Bookings.GetByIdAsync(id);
            if (existingBooking == null || existingBooking.UserId != userId)
            {
                return false;
            }
            existingBooking.GuestId = updatedBooking.GuestId;
            existingBooking.RoomId = updatedBooking.RoomId;
            existingBooking.CheckInDate = updatedBooking.CheckInDate;
            existingBooking.CheckOutDate = updatedBooking.CheckOutDate;
            _unitofwork.Bookings.Update(existingBooking);
            await _unitofwork.CompleteAsync();
            return true;
        }
    }
}
