using RentalApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Domain.Entities
{
    public sealed class User : BaseEntity
    {
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
