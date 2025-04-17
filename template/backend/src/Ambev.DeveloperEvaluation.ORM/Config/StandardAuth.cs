using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Microsoft.Extensions.DependencyInjection;
using System.Data.Entity;

namespace Ambev.DeveloperEvaluation.ORM.Config
{
    class StandardAuth
    {
        public static async Task CreateDefaultUser(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DefaultContext>();

            if (!await context.Users.AnyAsync(u => u.Email == "admin@gmail.com"))
            {
                var defaultUser = new User
                {
                    Id = Guid.NewGuid(),
                    Username = "admin",
                    Email = "admin@gmail.com",
                    Phone = "+12345678901",
                    Password = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                    Role = UserRole.Admin,
                    Status = UserStatus.Active,
                    CreatedAt = DateTime.UtcNow
                };

                context.Users.Add(defaultUser);
                await context.SaveChangesAsync();
            }
        }
    }
}
