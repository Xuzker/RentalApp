using AutoMapper;
using RentalApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Application.Features.ApartmentFeatures.GetAvailableApartment
{
    public sealed class GetAvailableApartmentMapper : Profile
    {
        public GetAvailableApartmentMapper()
        {
            CreateMap<Apartment, GetAvailableApartmentResponse>();
        }
    }
}
