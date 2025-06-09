using AutoMapper;
using MediatR;
using RentalApp.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Application.Features.BookingFeatures.GetAllBooking
{
    public sealed class GetAllUserHandler : IRequestHandler<GetAllUserRequest, List<GetAllUserResponse>>
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public GetAllUserHandler(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetAllUserResponse>> Handle(GetAllUserRequest request, CancellationToken cancellationToken)
        {
            var bookings = await _repository.GetAll(cancellationToken);

            if (bookings == null)
                throw new OperationCanceledException("No users found.");

            return _mapper.Map<List<GetAllUserResponse>>(bookings);
        }
    }
}
