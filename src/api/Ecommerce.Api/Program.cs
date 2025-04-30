using Ecommerce.Api.Modules;
using Ecommerce.Commons.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var modules = ModuleLoader.LoadModules();

foreach (var module in modules)
{
    if (module.IsEnabled)
    {
        module.RegisterServices(builder.Services);
    }
}

builder.Services.AddOpenApi();
builder.Services.AddInfrastructure();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/", () => "It's Ecommerce API!");

foreach (var module in modules)
{
     if(module.IsEnabled)
     {
         var group = app.MapGroup($"/{module.RoutePrefix}").WithTags(module.Name);
         module.RegisterEndpoints(group);
     }
}

await app.RunAsync();
