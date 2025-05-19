using AutoMapper;
using MediatR;
using RentalApp.Application.Repositories;
using RentalApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Application.Features.ApartmentFeatures.GetAllApartment
{
    public sealed class GetAllApartmentHandler : IRequestHandler<GetAllApartmentRequest, List<GetAllApartmentResponse>>
    {
        private readonly IApartmentRepository _repository;
        private readonly IMapper _mapper;

        public GetAllApartmentHandler(IApartmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetAllApartmentResponse>> Handle(
            GetAllApartmentRequest request, 
            CancellationToken cancellationToken)
        {
            var apartments = await _repository.GetAll(cancellationToken);

            return _mapper.Map<List<GetAllApartmentResponse>>(apartments);
        }
    }
}