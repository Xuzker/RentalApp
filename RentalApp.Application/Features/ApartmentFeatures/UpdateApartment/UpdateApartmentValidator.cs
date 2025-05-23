using FluentValidation;

namespace RentalApp.Application.Features.ApartmentFeatures.UpdateApartment
{
    public sealed class UpdateApartmentValidator : AbstractValidator<UpdateApartmentRequest>
    {
        public UpdateApartmentValidator()
        {
            RuleFor(apartment => apartment.Title).NotEmpty().MinimumLength(10);
            RuleFor(apartment => apartment.Description).NotEmpty().MinimumLength(10);
            RuleFor(apartment => apartment.PricePerDay).GreaterThan(1000);
            RuleFor(apartment => apartment.Address).MinimumLength(15);
        }
    }
}
