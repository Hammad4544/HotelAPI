using DataAcess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        public IRoomRepository Rooms => throw new NotImplementedException();

        public IGuestRepository Guests => throw new NotImplementedException();

        public IBookingRepository Bookings => throw new NotImplementedException();

        public Task<int> CompleteAsync()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
