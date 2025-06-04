using AutoMapper;
using RentalApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Application.Features.BookingFeatures.UpdateBooking
{
    public sealed class UpdateBookingMapper : Profile
    {
        public UpdateBookingMapper()
        {
            CreateMap<UpdateBookingRequest, User>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<User, UpdateBookingResponse>();
        }
    }
}
