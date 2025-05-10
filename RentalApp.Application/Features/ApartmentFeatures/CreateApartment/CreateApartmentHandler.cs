using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using RentalApp.Application.Repositories;
using RentalApp.Domain.Entities;

namespace RentalApp.Application.Features.Apartments.CreateApartment
{
    public sealed class CreateApartmentHandler : IRequestHandler<CreateApartmentRequest, CreateApartmentResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApartmentRepository _repository;
        private readonly IMapper _mappper;

        public CreateApartmentHandler(
            IUnitOfWork unitOfWork, 
            IApartmentRepository repository, 
            IMapper mappper)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _mappper = mappper;
        }

        public async Task<CreateApartmentResponse> Handle(
            CreateApartmentRequest request,
            CancellationToken cancellationToken)
        {
            var apartment = _mappper.Map<Apartment>(request);
            apartment.IsAvailable = true;

            _repository.Create(apartment);
            await _unitOfWork.Save(cancellationToken);

            return _mappper.Map<CreateApartmentResponse>(apartment);
        }
    }
}
