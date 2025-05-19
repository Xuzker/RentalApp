using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Application.Features.ApartmentFeatures.GetAvailableApartment
{
    public sealed record GetAvailableApartmentRequest(
        DateTime From, DateTime To): IRequest<List<GetAvailableApartmentResponse>>;
}
