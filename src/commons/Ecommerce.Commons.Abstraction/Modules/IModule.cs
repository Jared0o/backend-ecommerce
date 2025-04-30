using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Commons.Abstraction.Modules;

public interface IModule
{
    string Name { get; }
    string RoutePrefix { get; }
    bool IsEnabled { get; } 
    void RegisterEndpoints(RouteGroupBuilder app);
    void RegisterServices(IServiceCollection services);
    void RegisterMiddlewares(IApplicationBuilder app);
}