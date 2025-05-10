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
    public sealed class GetAllBookingHandler : IRequestHandler<GetAllBookingRequest, List<GetAllBookingResponse>>
    {
        private readonly IBookingRepository _repository;
        private readonly IMapper _mapper;

        public GetAllBookingHandler(IBookingRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetAllBookingResponse>> Handle(GetAllBookingRequest request, CancellationToken cancellationToken)
        {
            var bookings = await _repository.GetAll(cancellationToken);

            return _mapper.Map<List<GetAllBookingResponse>>(bookings);
        }
    }
}
