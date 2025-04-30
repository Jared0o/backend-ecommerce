using Ecommerce.Commons.Abstraction.Modules;
using Ecommerce.Modules.Users.Api.Routes;
using Ecommerce.Modules.Users.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Modules.Users.Api;

public class UserModule : IModule
{
    public string Name => "User";
    public string RoutePrefix => "Users";
    public bool IsEnabled => true;

    public void RegisterEndpoints(RouteGroupBuilder app)
        => app.MapUserEndpoints();

    public void RegisterServices(IServiceCollection services)
        => services.AddCore();

    public void RegisterMiddlewares(IApplicationBuilder app)
    {
        
    }
}