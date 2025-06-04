using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Application.Features.UserFeatures.GetUserById
{
    public sealed record GetUserByIdRequest(Guid Id) : IRequest<GetUserByIdResponse>;
}
