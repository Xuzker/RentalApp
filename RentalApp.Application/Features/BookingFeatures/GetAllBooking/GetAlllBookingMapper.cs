using AutoMapper;
using RentalApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Application.Features.BookingFeatures.GetAllBooking
{
    public sealed class GetAlllBookingMapper : Profile
    {
        public GetAlllBookingMapper()
        {
            CreateMap<Booking, GetAllBookingResponse>();
        }
    }
}
