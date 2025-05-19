using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Application.Features.ApartmentFeatures.DeleteApartment
{
    public sealed record DeleteApartmentRequest(Guid Id) : IRequest<DeleteApartmentResponse>;
}
