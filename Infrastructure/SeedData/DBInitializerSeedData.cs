using API.Domain.Entity;
using API.Infrastructure.Data;

namespace API.Infrastructure.SeedData
{
    public static class DBInitializerSeedData
    {
        public static void InitializeDatabase(APIDbContext dbContext)
        {
            if (dbContext.Roles.Any())
                return;

            var roles = new Role[]
            {
                new Role { RoleName = "Admin" },
                new Role { RoleName = "User" },
            };

            dbContext.Roles.AddRangeAsync(roles);
            dbContext.SaveChangesAsync();
        }
    }
}
