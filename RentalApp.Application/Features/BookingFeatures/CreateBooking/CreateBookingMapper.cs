using AutoMapper;
using RentalApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Application.Features.Bookings.CreateBooking
{
    public sealed class CreateBookingMapper : Profile
    {
        public CreateBookingMapper()
        {
            CreateMap<Booking, CreateBookingResponse>();
        }
    }
}
