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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteApartmentHandler(IApartmentRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<DeleteApartmentResponse> Handle(DeleteApartmentRequest request, CancellationToken cancellationToken)
        {
            var apartment = await _repository.Get(request.Id, cancellationToken);

            if (apartment == null)
            {
                throw new NotFoundException($"Apartment with Id: {request.Id} not found!");
            }
            _repository.Delete(apartment);
            await _unitOfWork.SaveAsync(cancellationToken);

            return _mapper.Map<DeleteApartmentResponse>(apartment);
        }
    }
}
