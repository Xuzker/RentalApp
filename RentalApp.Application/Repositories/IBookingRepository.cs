using RentalApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Application.Repositories
{
    public interface IBookingRepository : IBaseRepository<Booking>
    {
        Task<List<Booking>> GetUserBookingsAsync(Guid userId, CancellationToken cancellationToken);
    }
}
