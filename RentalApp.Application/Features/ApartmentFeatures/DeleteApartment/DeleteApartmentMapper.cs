using AutoMapper;
using RentalApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Application.Features.ApartmentFeatures.DeleteApartment
{
    public sealed class DeleteApartmentMapper : Profile
    {
        public DeleteApartmentMapper()
        {
            CreateMap<Apartment, DeleteApartmentResponse>();
        }
    }
}
