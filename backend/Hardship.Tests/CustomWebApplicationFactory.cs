using Hardship.Infrastructure;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Hardship.Tests
{
    // Custom factory used for integration testing.
    // Replaces the real database with an in-memory provider to ensure tests run in isolation.
    public class CustomWebApplicationFactory
    : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(
            Microsoft.AspNetCore.Hosting.IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove the existing DbContext registration.
                var descriptor = services
                    .SingleOrDefault(d =>
                        d.ServiceType == typeof(DbContextOptions<HardshipDbContext>));

                if (descriptor != null)
                    services.Remove(descriptor);
                // Register in-memory database for testing.
                services.AddDbContext<HardshipDbContext>(options =>
                {
                    options.UseInMemoryDatabase("TestDb");
                });
            });
        }
    }
}
