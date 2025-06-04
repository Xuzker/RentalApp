using AutoMapper;
using RentalApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Application.Features.UserFeatures.GetUserById
{
    public sealed class GetUserByIdMapper : Profile
    {
        public GetUserByIdMapper()
        {
            CreateMap<User, GetUserByIdResponse>();
        }
    }
}
