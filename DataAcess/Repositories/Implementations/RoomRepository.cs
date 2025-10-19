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
    public class RoomRepository : GenericRepository<Room>, IRoomRepository
    {
        private readonly HotelDbContext _dbcontext;

        public RoomRepository(HotelDbContext dbContext) : base(dbContext)
        {
            _dbcontext = dbContext;
        }

        public async Task<IEnumerable<Room>> GetAvailableRoomsAsync()
        {
           return await _dbcontext.Rooms.Where(r => r.IsAvailable).ToListAsync();
        }
    }
}
