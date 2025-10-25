using Models.DTOS.Booking;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelServices.Interfaces
{
    public interface IRoomService
    {
        public Task<IEnumerable<Room>> GetAvailableRooms();
        Task<Room?> GetRoomById(int id);
        Task<Room> CreateRoom(Room room);
        Task<bool> UpdateRoom(int id, Room updatedRoom);
        Task<bool> DeleteRoom(int id);
        Task<IEnumerable<Room>> GetAllRooms();
        //Task<IEnumerable<Booking>> GetAllBookingsAsync(string userId);
        //Task<Booking?> GetBookingByIdAsync(int id, string userId);
        //Task<Booking> CreateBookingAsync(CreateBookingDTO Dto, string userId);
        //Task<bool> UpdateBookingAsync(int id, Booking updatedBooking, string userId);
        //Task<bool> DeleteBookingAsync(int id, string userId);
    }
}
