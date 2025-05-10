using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Application.Features.Bookings.CreateBooking
{
    public sealed class CreateBookingValidator : AbstractValidator<CreateBookingRequest>
    {
        public CreateBookingValidator()
        {
            RuleFor(x => x.ApartmentId).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.StartDate).GreaterThan(DateTime.Now);
            RuleFor(x => x.EndDate).GreaterThan(x => x.StartDate);
        }
    }
}
