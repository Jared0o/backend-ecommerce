using Ecommerce.Commons.Abstraction.Modules;
using Ecommerce.Modules.Users.Api.Routes;
using Ecommerce.Modules.Users.Core;
using Ecommerce.Modules.Users.Infrastructure;
using Ecommerce.Modules.Users.Infrastructure.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Modules.Users.Api;

public class UserModule : IModule
{
    public string Name => "User";
    public string RoutePrefix => "Users";
    public bool IsEnabled => true;

    public void RegisterEndpoints(RouteGroupBuilder app)
        => app.MapUserEndpoints();

    public void RegisterServices(IServiceCollection services, IConfiguration configuration)
        => services.AddCore()
            .AddUsersInfrastructure(configuration);

    public void RegisterMiddlewares(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        ApplyUsersMigration.ApplyMigrations(serviceProvider).GetAwaiter().GetResult();
        DataSeeder.SeedRoleAsync(serviceProvider).GetAwaiter().GetResult();
        
    }
}