using AutoMapper;
using RentalApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Application.Features.ApartmentFeatures.UpdateApartment
{
    public sealed class UpdateApartmentMapper : Profile
    {
        public UpdateApartmentMapper()
        {
            CreateMap<UpdateApartmentRequest, Apartment>()
                .ForAllMembers(opts => 
                opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Apartment, UpdateApartmentResponse>();
        }
    }
}
