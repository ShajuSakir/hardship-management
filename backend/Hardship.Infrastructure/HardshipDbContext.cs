using Hardship.Application.Abstractions;
using Hardship.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hardship.Infrastructure
{
    public class HardshipDbContext
    : DbContext, IApplicationDbContext
    {
        public HardshipDbContext(DbContextOptions<HardshipDbContext> options)
            : base(options)
        {
        }

        public DbSet<HardshipApplication> Applications => Set<HardshipApplication>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<HardshipApplication>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.CustomerName)
                      .IsRequired()
                      .HasMaxLength(200);
                entity.Property(x => x.Income)
                      .HasPrecision(18, 2);
                entity.Property(x => x.Expenses)
                      .HasPrecision(18, 2);
            });
        }
    }
}
