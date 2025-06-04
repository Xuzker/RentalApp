using AutoMapper;
using MediatR;
using RentalApp.Application.Common.Exceptions;
using RentalApp.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Application.Features.BookingFeatures.UpdateBooking
{
    public sealed class UpdateBookingHandler : IRequestHandler<UpdateBookingRequest, UpdateBookingResponse>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBookingHandler(IBookingRepository bookingRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<UpdateBookingResponse> Handle(UpdateBookingRequest request, CancellationToken cancellationToken)
        {
            var booking = await _bookingRepository.Get(request.Id, cancellationToken);

            if (booking == null)
                throw new NotFoundException($"Booking with {request.Id} not found");

            _mapper.Map(request, booking);
            booking.DateUpdated = DateTime.UtcNow;
            await _unitOfWork.SaveAsync(cancellationToken);

            return _mapper.Map<UpdateBookingResponse>(booking);
        }
    }
}
