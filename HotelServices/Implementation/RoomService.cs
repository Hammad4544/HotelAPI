using DataAcess.Repositories.Interfaces;
using HotelServices.Interfaces;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelServices.Implementation
{
    public class RoomService : IRoomService
    {
        private readonly IUnitOfWork _unitofwork;

        public RoomService(IUnitOfWork unitOfWork) {
        
            _unitofwork = unitOfWork;
        }
        public async Task<Room> CreateRoom(Room room)
        {
            var r = await _unitofwork.Rooms.GetByIdAsync(room.RoomId);
            await _unitofwork.Rooms.AddAsync(room);
             await _unitofwork.CompleteAsync();
            return r;
        }

        public async Task<bool> DeleteRoom(int id)
        {
           var room = await _unitofwork.Rooms.GetByIdAsync(id);
            if (room == null)
            {
                return false;
            }
             _unitofwork.Rooms.Delete(room);
            await _unitofwork.CompleteAsync();
            return true;

        }

        public async Task<IEnumerable<Room>> GetAllRooms()
        {
           var Rooms = await _unitofwork.Rooms.GetAllAsync();
            return Rooms;
        }

        public Task<IEnumerable<Room>> GetAvailableRooms()
        {
            return _unitofwork.Rooms.GetAvailableRoomsAsync();
        }

        public async Task<Room?> GetRoomById(int id)
        {
           var room = await _unitofwork.Rooms.GetByIdAsync(id);
            return room;
        }

        public async Task<bool> UpdateRoom(int id, Room updatedRoom)
        {
            var exctingRoom = await _unitofwork.Rooms.GetByIdAsync(id);
            if (exctingRoom == null)
            {
                return false;
            }

            exctingRoom.PricePerNight = updatedRoom.PricePerNight;
            exctingRoom.Number = updatedRoom.Number;
            exctingRoom.IsAvailable =updatedRoom.IsAvailable;
             _unitofwork.Rooms.Update(exctingRoom);
           await _unitofwork.CompleteAsync(); 
            return true;
        }
    }
}
