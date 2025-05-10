using AutoMapper;
using RentalApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Application.Features.BookingFeatures.GetAllBooking
{
    public sealed class GetAlllUserMapper : Profile
    {
        public GetAlllUserMapper()
        {
            CreateMap<User, GetAllUserResponse>();
        }
    }
}
