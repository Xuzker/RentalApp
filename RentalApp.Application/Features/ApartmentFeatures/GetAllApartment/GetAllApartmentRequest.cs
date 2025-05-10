using MediatR;
using RentalApp.Application.Features.Apartments.CreateApartment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Application.Features.ApartmentFeatures.GetAllApartment
{
    public sealed record GetAllApartmentRequest(
        DateTime From,
        DateTime To) : IRequest<List<GetAllApartmentResponse>>;
}
