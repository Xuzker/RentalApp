using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Application.Features.BookingFeatures.GetAllBooking
{
    public sealed record GetAllBookingRequest() : IRequest<List<GetAllBookingResponse>>;
}
