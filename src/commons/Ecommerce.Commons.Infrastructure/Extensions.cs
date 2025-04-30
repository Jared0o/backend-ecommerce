using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Commons.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {

        services.AddMediator(x => x.ServiceLifetime = ServiceLifetime.Scoped);
        
        return services;
    }
}