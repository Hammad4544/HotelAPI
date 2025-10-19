using DataAcess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Repositories.Implementations
{
    public class GuestRepository : GenericRepository<Guest>, IGuestRepository
    {
        private readonly HotelDbContext _dbcontext;

        public GuestRepository(HotelDbContext dbContext) : base(dbContext)
        {
            _dbcontext = dbContext;
        }

        public async Task<Guest?> GetGuestWithBookingsAsync(int guestId)
        {
           return await _dbcontext.Guests
                .Where(g => g.GuestId == guestId)
                .Include(g => g.Bookings)
                .FirstOrDefaultAsync();
        }
    }
}
