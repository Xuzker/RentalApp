using AutoMapper;
using RentalApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Application.Features.ApartmentFeatures.GetAllApartment
{
    public sealed class GetAllApartmentMapper : Profile
    {
        public GetAllApartmentMapper()
        {
            CreateMap<Apartment, GetAllApartmentResponse>();
        }
    }
}
