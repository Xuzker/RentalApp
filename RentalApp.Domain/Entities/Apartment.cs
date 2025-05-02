using RentalApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Domain.Entities
{
    public sealed class Apartment : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int Rooms { get; set; }
        public decimal PricePerDay { get; set; }
        public bool IsAvailable { get; set; }

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
