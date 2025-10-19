using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        IRoomRepository Rooms { get; }
        IGuestRepository Guests { get; }
        IBookingRepository Bookings { get; }
        Task<int> CompleteAsync();
    }
}
