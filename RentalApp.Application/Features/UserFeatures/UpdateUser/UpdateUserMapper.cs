using AutoMapper;
using RentalApp.Application.Features.BookingFeatures.UpdateBooking;
using RentalApp.Domain.Entities;

namespace RentalApp.Application.Features.UserFeatures.UpdateUser
{
    public sealed class UpdateUserMapper : Profile
    {
        public UpdateUserMapper()
        {
            CreateMap<UpdateUserRequest, User>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<User, UpdateUserResponse>();
        }
    }
}
