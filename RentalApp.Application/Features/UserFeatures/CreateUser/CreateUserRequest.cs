using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Application.Features.UserFeatures.CreateUser
{
    public sealed record CreateUserRequest(
        string Email, string Name, string PasswordHash) : IRequest<CreateUserResponse>;
}
