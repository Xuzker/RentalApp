using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Application.Features.Apartments.CreateApartment
{
    public sealed class CreateApartmentValidator : AbstractValidator<CreateApartmentRequest>
    {
        public CreateApartmentValidator()
        {
            RuleFor(apartment => apartment.Title).NotEmpty().MaximumLength(100);
            RuleFor(apartment => apartment.Description).MaximumLength(500);
            RuleFor(apartment => apartment.Address).NotEmpty().MaximumLength(200);
            RuleFor(apartment => apartment.Rooms).GreaterThan(0);
            RuleFor(apartment => apartment.PricePerDay).GreaterThan(0);
        }
    }
}
