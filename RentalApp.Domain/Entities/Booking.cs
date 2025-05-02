using RentalApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Domain.Entities
{
    public sealed class Booking : BaseEntity
    {
        public Guid ApartmentId { get; set; }
        public Guid UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = string.Empty;

        public Apartment Apartment { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
