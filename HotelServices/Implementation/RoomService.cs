using AutoMapper;
using DataAcess.Repositories.Interfaces;
using HotelServices.Interfaces;
using Models.DTOS.Room;
using Models.Entities;

namespace HotelServices.Implementation
{
    public class RoomService : IRoomService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoomService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<RoomResponseDto> CreateRoom(CreateRoomDto dto)
        {
            var room = _mapper.Map<Room>(dto);
            await _unitOfWork.Rooms.AddAsync(room);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<RoomResponseDto>(room);
        }

        public async Task<bool> DeleteRoom(int id)
        {
            var room = await _unitOfWork.Rooms.GetByIdAsync(id);
            if (room == null)
                return false;

            _unitOfWork.Rooms.Delete(room);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<IEnumerable<RoomResponseDto>> GetAllRooms()
        {
            var rooms = await _unitOfWork.Rooms.GetAllAsync();
            return  _mapper.Map<IEnumerable<RoomResponseDto>>(rooms);
        }

        public async Task<IEnumerable<RoomResponseDto>> GetAvailableRooms()
        {
            var availableRooms = await _unitOfWork.Rooms.GetAvailableRoomsAsync();
            return _mapper.Map<IEnumerable<RoomResponseDto>>(availableRooms);
        }

        public async Task<RoomResponseDto?> GetRoomById(int id)
        {
            var room = await _unitOfWork.Rooms.GetByIdAsync(id);
            return _mapper.Map<RoomResponseDto?>(room);
        }

        public async Task<bool> UpdateRoom(int id, UpdateRoomDto dto)
        {
            var existingRoom = await _unitOfWork.Rooms.GetByIdAsync(id);
            if (existingRoom == null)
                return false;

            _mapper.Map(dto, existingRoom);

            _unitOfWork.Rooms.Update(existingRoom);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
