﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Application.Features.UserFeatures.GetUserByEmail
{
    public sealed record GetUserByEmailRequest(string Email) : IRequest<GetUserByEmailResponse>;
}
