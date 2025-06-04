using MediatR;
using RentalApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Application.Features.BookingFeatures.UpdateBooking
{
    public sealed record UpdateBookingRequest(Guid Id, 
        Guid? ApartmentId, Guid? UserId, DateTime? StartDate, 
        DateTime? EndDate, decimal? TotalPrice) : IRequest<UpdateBookingResponse>;
}
