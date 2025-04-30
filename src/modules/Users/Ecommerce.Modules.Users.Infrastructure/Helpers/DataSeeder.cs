using Ecommerce.Modules.Users.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Modules.Users.Infrastructure.Helpers;

public static class DataSeeder
{
    public static async Task SeedRoleAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
        string[] roles = Enum.GetNames<Roles>();

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new Role{Name = role});
            }
        }
    }
}