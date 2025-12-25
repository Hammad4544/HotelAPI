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
            CreateMap<Room, RoomResponseDto>()
                .ForMember(dest=>dest.Images,opt=> opt.MapFrom(ser=>ser.Images.Select(i=>i.ImageUrl)))
                .ReverseMap();
            CreateMap<CreateRoomDto, Room>().ForMember(dest => dest.Images, opt => opt.Ignore()); ;
            CreateMap<UpdateRoomDto, Room>();
            CreateMap<Room, CreateRoomDto>();
            CreateMap<Room, UpdateRoomDto>();

            // 🟦 Booking Mappings
            CreateMap<Booking, BookingResponseDto>()
                .ForMember(dest => dest.Room, opt => opt.MapFrom(src => src.Room)) // ربط الغرفة داخل الحجز
                .ReverseMap();

            CreateMap<CreateBookingDTO, Booking>();
            CreateMap<UpdateBookingDto, Booking>();
            CreateMap<Booking, CreateBookingDTO>();
            CreateMap<Booking, UpdateBookingDto>();
        }
    }
}
