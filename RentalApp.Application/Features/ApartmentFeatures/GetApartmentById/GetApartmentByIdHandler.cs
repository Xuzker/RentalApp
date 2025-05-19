using AutoMapper;
using MediatR;
using RentalApp.Application.Common.Exceptions;
using RentalApp.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Application.Features.ApartmentFeatures.GetApartmentById
{
    public class GetApartmentByIdHandler : IRequestHandler<GetApartmentByIdRequest, GetApartmentByIdResponse>
    {
        private readonly IApartmentRepository _repository;
        private readonly IMapper _mapper;

        public GetApartmentByIdHandler(IApartmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetApartmentByIdResponse> Handle(
            GetApartmentByIdRequest request,
            CancellationToken cancellationToken)
        {
            var apartment = await _repository.Get(request.Id, cancellationToken);

            if (apartment == null)
            {
                throw new NotFoundException($"Apartment with Id: {request.Id} not found!");
            }

            return _mapper.Map<GetApartmentByIdResponse>(apartment);
        }
    }
}
