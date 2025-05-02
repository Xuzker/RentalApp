using AutoMapper;
using RentalApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Application.Features.Apartments.CreateApartment
{
    public class CreateApartmentMapper : Profile
    {
        public CreateApartmentMapper()
        {
            CreateMap<CreateApartmentRequest, Apartment>();
            CreateMap<Apartment, CreateApartmentResponse>();
        }
    }
}
