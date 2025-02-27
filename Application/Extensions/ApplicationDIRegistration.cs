using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Reflection;

namespace API.Application.Extensions
{
    public static class ApplicationDIRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // AutoMapper and MediatR setup
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(m => m.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            // Get connection string from configuration
            var connectionString = configuration.GetConnectionString("APIDatabase");

            // Register the SqlConnection with the connection string
            services.AddTransient<IDbConnection>(sp => new SqlConnection(connectionString));

            return services;
        }
    }
}
