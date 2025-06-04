using RentalApp.Domain.Entities;

namespace RentalApp.Application.Features.BookingFeatures.UpdateBooking
{
    public sealed record UpdateBookingResponse
    {
        public Guid Id { get; set; }

        public Guid ApartmentId { get; set; }
        public Apartment Apartment { get; set; } = null!;

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal TotalPrice { get; set; }
    }
}