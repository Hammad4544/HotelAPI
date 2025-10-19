using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Repositories.Interfaces
{
    public interface IGuestRepository: IGenericRepository<Guest>
    {
        Task<Guest?> GetGuestWithBookingsAsync(int guestId);

    }
}
