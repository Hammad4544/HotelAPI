using AutoMapper;
using Models.Entities;
using Models.DTOS.Room;
using Models.DTOS.Booking;

namespace HotelServices.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // 🟩 Room Mappings
            CreateMap<Room, RoomResponseDto>().ReverseMap();
            CreateMap<CreateRoomDto, Room>();
            CreateMap<UpdateRoomDto, Room>();
            CreateMap<Room, CreateRoomDto>();
            CreateMap<Room, UpdateRoomDto>();

            // 🟦 Booking Mappings
            CreateMap<Booking, BookingResponseDto>()
                .ForMember(dest => dest.Room, opt => opt.MapFrom(src => src.Room)) // ربط الغرفة داخل الحجز
                .ReverseMap();

            CreateMap<CreateBookingDTO, Booking>();
            CreateMap<UpdateBookingDto, Booking>();
        }
    }
}
