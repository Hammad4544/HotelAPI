using DataAcess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Repositories.Implementations
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        private readonly HotelDbContext _dbcontext;

        public BookingRepository(HotelDbContext dbContext) : base(dbContext)
        {
            _dbcontext = dbContext;
        }

        public async Task<IEnumerable<Booking>> GetActiveBookingsAsync()
        {
            return await _dbcontext.Bookings
                .Where(b => b.CheckOutDate >= DateTime.Now)
                .ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetBookingsByGuestIdAsync(int guestId)
        {
            return await _dbcontext.Bookings
                .Where(b => b.GuestId == guestId)
                .ToListAsync();
        }
    }
}
