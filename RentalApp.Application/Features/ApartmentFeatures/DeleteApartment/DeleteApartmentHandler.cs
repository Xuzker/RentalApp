using AutoMapper;
using MediatR;
using RentalApp.Application.Repositories;
using RentalApp.Application.Common.Exceptions;

namespace RentalApp.Application.Features.ApartmentFeatures.DeleteApartment
{
    public sealed class DeleteApartmentHandler :
        IRequestHandler<DeleteApartmentRequest, DeleteApartmentResponse>
    {
        private readonly IApartmentRepository _repository;
        private readonly IMapper _mapper;

        public DeleteApartmentHandler(IApartmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<DeleteApartmentResponse> Handle(DeleteApartmentRequest request, CancellationToken cancellationToken)
        {
            var apartment = await _repository.Get(request.Id, cancellationToken);

            if (apartment == null)
            {
                throw new NotFoundException($"Apartment with Id: {request.Id} not found!");
            }
            _repository.Delete(apartment);

            return _mapper.Map<DeleteApartmentResponse>(apartment);
        }
    }
}
