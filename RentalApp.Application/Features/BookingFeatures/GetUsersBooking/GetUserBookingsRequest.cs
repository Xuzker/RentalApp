using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Application.Features.BookingFeatures.GetUsersBooking
{
    public sealed record GetUserBookingsRequest(Guid UserId) : IRequest<List<GetUserBookingsResponse>>;
}
