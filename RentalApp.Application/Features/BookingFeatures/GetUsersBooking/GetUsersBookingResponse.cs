using RentalApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Application.Features.BookingFeatures.GetUsersBooking
{
    public sealed record GetUsersBookingResponse
    {
        public Guid Id { get; set; }

        public DateTime? DateCreated { get; set; }


        public Guid ApartmentId { get; set; }
        public Apartment Apartment { get; set; } = null!;

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal TotalPrice { get; set; }

        public string Status { get; set; } = string.Empty;
    }
}
