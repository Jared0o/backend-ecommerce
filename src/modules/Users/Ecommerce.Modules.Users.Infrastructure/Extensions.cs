using Ecommerce.Modules.Users.Core.Models;
using Ecommerce.Modules.Users.Core.Services;
using Ecommerce.Modules.Users.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Modules.Users.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddUsersInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ITokenService, TokenService>();
        
        services.AddDbContext<UsersContext>(opt =>
            opt.UseNpgsql(configuration.GetConnectionString("UsersContext")));

        services.AddIdentityCore<User>(opt =>
            {
                opt.Password.RequireDigit = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequiredLength = 8;
            })
            .AddRoles<Role>()
            .AddEntityFrameworkStores<UsersContext>()
            .AddSignInManager<SignInManager<User>>()
            .AddRoleManager<RoleManager<Role>>()
            .AddDefaultTokenProviders();
        
        return services;
    }
}