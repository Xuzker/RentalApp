using Microsoft.EntityFrameworkCore;
using RentalApp.Application.Repositories;
using RentalApp.Domain.Entities;
using RentalApp.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Infrastructure.Repositories
{
    public class BookingRepository : BaseRepository<Booking>, IBookingRepository
    {
        public BookingRepository(DataContext context) : base(context)
        {
        }

        public Task<List<Booking>> GetUserBookings(Guid userId, CancellationToken cancellationToken)
        {
            return _context.Bookings.Where(booking => booking.UserId == userId)
                .ToListAsync(cancellationToken);
        }
    }
}
