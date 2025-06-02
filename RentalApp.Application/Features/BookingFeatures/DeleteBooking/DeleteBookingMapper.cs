using AutoMapper;
using RentalApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Application.Features.BookingFeatures.DeleteBooking
{
    public sealed class DeleteBookingMapper : Profile
    {
        public DeleteBookingMapper()
        {
            CreateMap<Booking, DeleteBookingResponse>();
        }
    }
}
