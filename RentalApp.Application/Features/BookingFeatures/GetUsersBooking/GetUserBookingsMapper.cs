using AutoMapper;
using RentalApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Application.Features.BookingFeatures.GetUsersBooking
{
    public sealed class GetUserBookingsMapper : Profile
    {
        public GetUserBookingsMapper()
        {
            CreateMap<Booking, GetUserBookingsResponse>();
        }
    }
}
