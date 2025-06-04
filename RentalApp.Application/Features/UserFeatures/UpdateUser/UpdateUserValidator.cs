using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Application.Features.UserFeatures.UpdateUser
{
    public sealed class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserValidator()
        {
            RuleFor(user => user.Id).NotEmpty();
            RuleFor(user => user.Email).NotEmpty().MinimumLength(10);
            RuleFor(user => user.Name).NotEmpty().MinimumLength(5);
            RuleFor(user => user.Role).NotEmpty().MinimumLength(3);
        }
    }
}
