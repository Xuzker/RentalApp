using AutoMapper;
using MediatR;
using RentalApp.Application.Repositories;
using RentalApp.Application.Common.Exceptions;

namespace RentalApp.Application.Features.BookingFeatures.GetUsersBooking
{
    public sealed class GetUsersBookingHandler : IRequestHandler<GetUsersBookingRequest, List<GetUsersBookingResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;

        public GetUsersBookingHandler(IUserRepository userRepository, IMapper mapper, IBookingRepository bookingRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _bookingRepository = bookingRepository;
        }

        public async Task<List<GetUsersBookingResponse>> Handle(GetUsersBookingRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Get(request.UserId, cancellationToken);

            if (user == null)
            {
                throw new NotFoundException($"User with Id: {request.UserId} not found!");
            }

            var bookings = await _bookingRepository.GetUserBookingsAsync(user.Id, cancellationToken);

            return _mapper.Map<List<GetUsersBookingResponse>>(bookings);
        }
    }
}
