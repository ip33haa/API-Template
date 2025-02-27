using API.Infrastructure.Data;
using API.Infrastructure.SeedData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace API.Infrastructure.Extensions
{
    public static class StartupDbExtensions
    {
        public static async void CreateDbIfNotExists(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<APIDbContext>();

            context.Database.EnsureCreated();

            // Ensure the database is created
            await context.Database.EnsureCreatedAsync();

            // Apply pending migrations if any
            await context.Database.MigrateAsync();

            DBInitializerSeedData.InitializeDatabase(context);
        }
    }
}
