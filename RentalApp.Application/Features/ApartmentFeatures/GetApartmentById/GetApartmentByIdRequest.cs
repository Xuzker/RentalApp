using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Application.Features.ApartmentFeatures.GetApartmentById
{
    public sealed record GetApartmentByIdRequest(Guid Id) : IRequest<GetApartmentByIdResponse>;

}
