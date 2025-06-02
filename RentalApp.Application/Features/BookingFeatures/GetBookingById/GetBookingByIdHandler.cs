using AutoMapper;
using MediatR;
using RentalApp.Application.Common.Exceptions;
using RentalApp.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Application.Features.BookingFeatures.GetBookingById
{
    internal class GetBookingByIdHandler : IRequestHandler<GetBookingByIdRequest, GetBookingByIdResponse>
    {
        private readonly IBookingRepository _repository;
        private readonly IMapper _mapper;

        public GetBookingByIdHandler(IBookingRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetBookingByIdResponse> Handle(
            GetBookingByIdRequest request,
            CancellationToken cancellationToken)
        {
            var apartment = await _repository.Get(request.Id, cancellationToken);

            if (apartment == null)
            {
                throw new NotFoundException($"Apartment with Id: {request.Id} not found!");
            }

            return _mapper.Map<GetBookingByIdResponse>(apartment);
        }
    }
}