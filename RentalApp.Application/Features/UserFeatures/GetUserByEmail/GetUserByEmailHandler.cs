using AutoMapper;
using MediatR;
using RentalApp.Application.Common.Exceptions;
using RentalApp.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Application.Features.UserFeatures.GetUserByEmail
{
    public sealed class GetUserByEmailHandler : IRequestHandler<GetUserByEmailRequest, GetUserByEmailResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserByEmailHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<GetUserByEmailResponse> Handle(GetUserByEmailRequest request, CancellationToken cancellationToken)
        {
            if(request.Email == null)
                throw new ArgumentNullException($"The email is null");

            if (request.Email.Length == 0)
                throw new ArgumentException($"The email is empty");

            var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

            if(user == null) 
                throw new NotFoundException($"Email {request.Email} not found...");

            return _mapper.Map<GetUserByEmailResponse>(user);
        }
    }
}
