using AutoMapper;
using RentalApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Application.Features.BookingFeatures.GetUsersBooking
{
    public sealed class GetUsersBookingMapper : Profile
    {
        public GetUsersBookingMapper()
        {
            CreateMap<Booking, GetUsersBookingResponse>();
        }
    }
}
