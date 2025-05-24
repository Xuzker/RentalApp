using AutoMapper;
using MediatR;
using RentalApp.Application.Common.Exceptions;
using RentalApp.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Application.Features.ApartmentFeatures.UpdateApartment
{
    public sealed class UpdateApartmentHandler 
        : IRequestHandler<UpdateApartmentRequest, UpdateApartmentResponse>
    {
        private readonly IApartmentRepository _apartmentRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateApartmentHandler(IApartmentRepository apartmentRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _apartmentRepository = apartmentRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<UpdateApartmentResponse> Handle(UpdateApartmentRequest request, CancellationToken cancellationToken)
        {
            var apartment = await _apartmentRepository.Get(request.Id, cancellationToken);

            if (apartment == null)
            {
                throw new NotFoundException($"Apartment with Id: {request.Id} not found!");
            }

            _mapper.Map(request, apartment);
            apartment.DateUpdated = DateTime.UtcNow;
            await _unitOfWork.SaveAsync(cancellationToken);

            return _mapper.Map<UpdateApartmentResponse>(apartment);
        }
    }
}
