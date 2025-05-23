using MediatR;
using RentalApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Application.Features.ApartmentFeatures.UpdateApartment
{
    public sealed record UpdateApartmentRequest(
        Guid Id, string Title, string Description,
        string Address, int Rooms,
        decimal PricePerDay, bool IsAvailable, List<Booking>? Bookings) : IRequest<UpdateApartmentResponse>;
}
