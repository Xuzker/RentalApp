using AutoMapper;
using RentalApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Application.Features.UserFeatures.GetUserByEmail
{
    public sealed class GetUserByEmailMapper : Profile
    {
        public GetUserByEmailMapper()
        {
            CreateMap<User, GetUserByEmailResponse>();
        }
    }
}
