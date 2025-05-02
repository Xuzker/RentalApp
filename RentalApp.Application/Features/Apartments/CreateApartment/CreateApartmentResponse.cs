using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Application.Features.Apartments.CreateApartment
{
    public sealed record CreateApartmentResponse(
        Guid Id,
        string Title,
        string Address,
        decimal PricePerDay
    );
}
