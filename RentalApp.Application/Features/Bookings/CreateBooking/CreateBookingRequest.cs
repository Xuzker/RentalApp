using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Application.Features.Bookings.CreateBooking
{
    public sealed record CreateBookingRequest(
        Guid ApartmentId,
        Guid UserId,
        DateTime StartDate,
        DateTime EndDate) : IRequest<CreateBookingResponse>;
}
