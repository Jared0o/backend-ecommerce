using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Commons.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services;
    }
    
    public static async Task ApplyMigrations(this IServiceCollection services)
    {
        using var scope = services.BuildServiceProvider().CreateScope();
        var serviceProvider = scope.ServiceProvider;
        var allDbContexts = serviceProvider.GetServices<DbContext>();

        foreach (var dbContext in allDbContexts)
        {
            await dbContext.Database.MigrateAsync();
        }
    }
}