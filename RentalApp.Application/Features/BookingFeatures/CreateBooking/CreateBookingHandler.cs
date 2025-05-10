using AutoMapper;
using MediatR;
using RentalApp.Application.Common.Exceptions;
using RentalApp.Application.Repositories;
using RentalApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Application.Features.Bookings.CreateBooking
{
    public sealed class CreateBookingHandler : IRequestHandler<CreateBookingRequest, CreateBookingResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApartmentRepository _apartmentRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;

        public CreateBookingHandler(
            IUnitOfWork unitOfWork,
            IApartmentRepository apartmentRepository,
            IBookingRepository bookingRepository,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _apartmentRepository = apartmentRepository;
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        public async Task<CreateBookingResponse> Handle(
            CreateBookingRequest request,
            CancellationToken cancellationToken)
        {
            var apartment = await _apartmentRepository.Get(request.ApartmentId, cancellationToken);
            if (apartment == null || !apartment.IsAvailable)
                throw new NotFoundException("Apartment not found or not available");

            var booking = new Booking
            {
                ApartmentId = request.ApartmentId,
                UserId = request.UserId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                TotalPrice = apartment.PricePerDay * (decimal)(request.EndDate - request.StartDate).TotalDays,
                Status = "Pending"
            };

            _bookingRepository.Create(booking);
            await _unitOfWork.Save(cancellationToken);

            return _mapper.Map<CreateBookingResponse>(booking);
        }
    }
}
