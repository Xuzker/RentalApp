using RentalApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Application.Repositories
{
    public interface IApartmentRepository: IBaseRepository<Apartment>
    {
        Task<List<Apartment>> GetAvailableApartments(DateTime from, DateTime to, CancellationToken cancellationToken);
    }
}
