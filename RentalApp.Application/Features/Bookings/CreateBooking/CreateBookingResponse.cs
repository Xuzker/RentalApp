using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Application.Features.Bookings.CreateBooking
{
    public sealed record CreateBookingResponse(
        Guid Id,
        Guid ApartmentId,
        DateTime StartDate,
        DateTime EndDate,
        decimal TotalPrice,
        string Status);
}
