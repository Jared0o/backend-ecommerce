using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Modules.Users.Infrastructure.Helpers;

public static class ApplyUsersMigration
{
    public static async Task ApplyMigrations(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<UsersContext>();
        await dbContext.Database.MigrateAsync();
    }
}