using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Application.Features.UserFeatures.CreateUser
{
    public sealed class CreateUserValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.Name).MaximumLength(50).NotEmpty();
            RuleFor(x => x.Email).MinimumLength(10).MaximumLength(50).NotEmpty().EmailAddress();
            RuleFor(x => x.PasswordHash).MinimumLength(6).NotEmpty();
        }
    }
}
