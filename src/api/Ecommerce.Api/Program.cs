using Ecommerce.Api.Modules;
using Ecommerce.Commons.Infrastructure;
using Ecommerce.Modules.Users.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var modules = ModuleLoader.LoadModules();

ModuleLoader.RegisterModuleServices(modules, builder.Services, builder.Configuration);

builder.Services.AddAuthentication();
builder.Services.AddOpenApi();
builder.Services.AddInfrastructure();
await builder.Services.ApplyMigrations();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/", () => "It's Ecommerce API!");

ModuleLoader.RegisterModules(modules, app);

await app.RunAsync();
