using AutoMapper;
using MediatR;
using RentalApp.Application.Repositories;
using RentalApp.Application.Common.Exceptions;

namespace RentalApp.Application.Features.BookingFeatures.DeleteBooking
{
    public sealed class DeleteBookingHandle : IRequestHandler<DeleteBookingRequest, DeleteBookingResponse>
    {
        private readonly IBookingRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteBookingHandle(IBookingRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<DeleteBookingResponse> Handle(DeleteBookingRequest request, CancellationToken cancellationToken)
        {
            var booking = await _repository.Get(request.Id, cancellationToken);

            if (booking == null)
            {
                throw new NotFoundException($"Booking with Id: {request.Id} not found");
            }

            _repository.Delete(booking);
            await _unitOfWork.SaveAsync(cancellationToken);

            return _mapper.Map<DeleteBookingResponse>(booking);
        }
    }
}
