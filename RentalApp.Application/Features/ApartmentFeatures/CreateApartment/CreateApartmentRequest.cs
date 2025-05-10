using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Application.Features.Apartments.CreateApartment
{
    public sealed record CreateApartmentRequest (
        string Title,
        string Description,
        string Address,
        int Rooms,
        decimal PricePerDay
    ): IRequest<CreateApartmentResponse>;
}
