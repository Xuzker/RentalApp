using AutoMapper;
using MediatR;
using RentalApp.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Application.Features.ApartmentFeatures.GetAvailableApartment
{
    public sealed class GetAvailableApartmentHandler : 
        IRequestHandler<GetAvailableApartmentRequest, List<GetAvailableApartmentResponse>>
    {
        private readonly IApartmentRepository _repository;
        private readonly IMapper _mapper;

        public GetAvailableApartmentHandler(IApartmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetAvailableApartmentResponse>> Handle(GetAvailableApartmentRequest request, CancellationToken cancellationToken)
        {
            var apartments = await _repository.GetAvailableApartmentsAsync(
                request.From, request.To, cancellationToken);

            return _mapper.Map<List<GetAvailableApartmentResponse>>(apartments);
        }
    }
}
