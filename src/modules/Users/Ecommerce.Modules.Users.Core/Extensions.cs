using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Modules.Users.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }
}