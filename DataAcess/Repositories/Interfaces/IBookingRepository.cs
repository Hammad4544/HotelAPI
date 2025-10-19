using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Repositories.Interfaces
{
    public interface IBookingRepository: IGenericRepository<Booking>
    {
        Task<IEnumerable<Booking>> GetBookingsByGuestIdAsync(int guestId);
        Task<IEnumerable<Booking>> GetActiveBookingsAsync();

    }
}
