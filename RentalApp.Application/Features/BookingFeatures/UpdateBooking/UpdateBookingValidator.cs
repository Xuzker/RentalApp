using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Application.Features.BookingFeatures.UpdateBooking
{
    public sealed class UpdateBookingValidator : AbstractValidator<UpdateBookingRequest>
    {
        public UpdateBookingValidator()
        {
            RuleFor(booking => booking.TotalPrice).GreaterThanOrEqualTo(500);
            RuleFor(booking => booking.StartDate).GreaterThan(new DateTime(2025, 6, 4));
            RuleFor(booking => booking.UserId).NotEmpty();
            RuleFor(booking => booking.ApartmentId).NotEmpty();
        }
    }
}
