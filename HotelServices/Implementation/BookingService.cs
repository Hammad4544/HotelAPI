using AutoMapper;
using DataAcess.Repositories.Interfaces;
using HotelServices.Interfaces;
using Models.DTOS.Booking;
using Models.Entities;

namespace HotelServices.Implementation
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BookingResponseDto> CreateBookingAsync(CreateBookingDTO dto, string userId)
        {
            var booking = _mapper.Map<Booking>(dto);
            booking.UserId = userId;

            await _unitOfWork.Bookings.AddAsync(booking);
            await _unitOfWork.CompleteAsync();

            // تحميل الغرفة بعد الحفظ
            var room = await _unitOfWork.Rooms.GetByIdAsync(booking.RoomId);
            booking.Room = room;

            return _mapper.Map<BookingResponseDto>(booking);
        }

        public async Task<bool> DeleteBookingAsync(int id, string userId)
        {
            var booking = await _unitOfWork.Bookings.GetByIdAsync(id);
            if (booking == null || booking.UserId != userId)
                return false;

            _unitOfWork.Bookings.Delete(booking);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<IEnumerable<BookingResponseDto>> GetAllBookingsAsync(string userId)
        {
            var bookings = await _unitOfWork.Bookings.GetAllAsync();
            var userBookings = bookings.Where(b => b.UserId == userId);

            return _mapper.Map<IEnumerable<BookingResponseDto>>(userBookings);
        }

        public async Task<BookingResponseDto?> GetBookingByIdAsync(int id, string userId)
        {
            var booking = await _unitOfWork.Bookings.GetByIdAsync(id);
            if (booking == null || booking.UserId != userId)
                return null;

            return _mapper.Map<BookingResponseDto>(booking);
        }

        public async Task<bool> UpdateBookingAsync(int id, UpdateBookingDto updatedBooking, string userId)
        {
            var existingBooking = await _unitOfWork.Bookings.GetByIdAsync(id);
            if (existingBooking == null || existingBooking.UserId != userId)
                return false;

            _mapper.Map(updatedBooking, existingBooking);

            _unitOfWork.Bookings.Update(existingBooking);
            await _unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<IEnumerable<BookingResponseDto>> GetActiveBookings()
        {
            var bookings = await _unitOfWork.Bookings.GetActiveBookingsAsync();
            return _mapper.Map<IEnumerable<BookingResponseDto>>(bookings);
        }

        public async Task<IEnumerable<BookingResponseDto>> GetBookingsByGuestId(string guestId)
        {
            var bookings = await _unitOfWork.Bookings.GetBookingsByGuestIdAsync(guestId);
            return _mapper.Map<IEnumerable<BookingResponseDto>>(bookings);
        }
    }
}
