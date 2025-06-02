using MediatR;
using RentalApp.Application.Features.ApartmentFeatures.GetApartmentById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Application.Features.BookingFeatures.GetBookingById
{
    public sealed record GetBookingByIdRequest(Guid Id) : IRequest<GetBookingByIdResponse>;
}
