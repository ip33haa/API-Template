using API.Application.Interface;
using API.Infrastructure.Data;
using API.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Text;

namespace API.Infrastructure.Extensions
{
    public static class InfrastrutureDIRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<APIDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("APIDatabase"),
            o => o.EnableRetryOnFailure()));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = configuration["AppSettings:Issuer"],
                            ValidateAudience = true,
                            ValidAudience = configuration["AppSettings:Audience"],
                            ValidateLifetime = true,
                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(configuration["AppSettings:Token"]!)),
                            ValidateIssuerSigningKey = true
                        };
                    });

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
