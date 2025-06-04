using AutoMapper;
using MediatR;
using RentalApp.Application.Common.Exceptions;
using RentalApp.Application.Features.ApartmentFeatures.GetAllApartment;
using RentalApp.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Application.Features.UserFeatures.GetUserById
{
    public sealed class GetUserByIdHandler : IRequestHandler<GetUserByIdRequest, GetUserByIdResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserByIdHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<GetUserByIdResponse> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Get(request.Id, cancellationToken);

            if (user == null)
                throw new NotFoundException($"User with {request.Id} not found");

            return _mapper.Map<GetUserByIdResponse>(user);
        }
    }
}
