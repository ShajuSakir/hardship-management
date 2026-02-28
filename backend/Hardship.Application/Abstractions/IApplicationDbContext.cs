using Hardship.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hardship.Application.Abstractions
{
    public interface IApplicationDbContext
    {
        DbSet<HardshipApplication> Applications { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
