using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentalApp.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using RentalApp.Domain.Entities;
using RentalApp.Infrastructure.Context;

namespace RentalApp.Infrastructure.Repositories
{
    public class ApartmentRepository : BaseRepository<Apartment>, IApartmentRepository
    {
        public ApartmentRepository(DataContext context) : base(context)
        {
        }

        public Task<List<Apartment>> GetAvailableApartmentsAsync(DateTime from, DateTime to, CancellationToken cancellationToken)
        {
            var utcFrom = from.Kind == DateTimeKind.Unspecified
                ? DateTime.SpecifyKind(from, DateTimeKind.Utc)
                : from.ToUniversalTime();

            var utcTo = to.Kind == DateTimeKind.Unspecified
                ? DateTime.SpecifyKind(to, DateTimeKind.Utc)
                : to.ToUniversalTime();

            return _context.Apartments.Include(x => x.Bookings)
                .Where(apartment => apartment.IsAvailable &&
                    !apartment.Bookings.Any(booking => booking.Status == "Pending" &&
                            (booking.StartDate <= utcTo && booking.EndDate >= utcFrom)))
                .ToListAsync(cancellationToken);
        }
    }
}
