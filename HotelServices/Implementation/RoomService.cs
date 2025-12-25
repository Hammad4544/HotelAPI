using AutoMapper;
using DataAcess.Repositories.Implementations;
using DataAcess.Repositories.Interfaces;
using HotelServices.Interfaces;
using Models.DTOS.Room;
using Models.Entities;

namespace HotelServices.Implementation
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoomService(IUnitOfWork unitOfWork, IMapper mapper,IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<RoomResponseDto> CreateRoom(CreateRoomDto dto)
        {
            var room = _mapper.Map<Room>(dto);
            await _unitOfWork.Rooms.AddAsync(room);
            await _unitOfWork.CompleteAsync();
            // upload images
            if (dto.Images != null && dto.Images.Any()) { 
            
                var folderPath = Path.Combine("wwwroot", "images", "rooms", room.RoomId.ToString());
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);
                foreach (var image in dto.Images)
                {
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
                    var fullPath = Path.Combine(folderPath, fileName);

                    using var stream = new FileStream(fullPath, FileMode.Create);
                    await image.CopyToAsync(stream);

                    var roomImage = new RoomImage
                    {
                        RoomId = room.RoomId,
                        ImageUrl = $"/images/rooms/{room.RoomId}/{fileName}"
                    };

                    await _unitOfWork.RoomImages.AddAsync(roomImage);
                }
                await _unitOfWork.CompleteAsync();



            }

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
            var rooms = await _roomRepository.GetAllWithImages();
            return  _mapper.Map<IEnumerable<RoomResponseDto>>(rooms);
        }

        public async Task<IEnumerable<RoomResponseDto>> GetAvailableRooms()
        {
            var availableRooms = await _unitOfWork.Rooms.GetAvailableRoomsAsync();
            return _mapper.Map<IEnumerable<RoomResponseDto>>(availableRooms);
        }

        public async Task<RoomResponseDto?> GetRoomById(int id)
        {
            var room = await _roomRepository.GetByIdWhithImages(id);
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
