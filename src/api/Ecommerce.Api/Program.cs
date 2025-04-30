using Ecommerce.Api.Modules;
using Ecommerce.Commons.Infrastructure;
using Ecommerce.Modules.Users.Infrastructure;
using Microsoft.EntityFrameworkCore;
using NSwag;

var builder = WebApplication.CreateBuilder(args);
var modules = ModuleLoader.LoadModules();

ModuleLoader.RegisterModuleServices(modules, builder.Services, builder.Configuration);

builder.Services.AddAuthentication();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(x =>
{
    x.Title = "Ecommerce API";
    x.DocumentName = "Ecommerce API v1";
    x.Version = "v1";
    x.PostProcess = d => d.Info.Title = "Ecommerce API";
    x.AddSecurity("Bearer", [], new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Description = "JWT Authorization header using the Bearer scheme.",
        In = OpenApiSecurityApiKeyLocation.Header,
        Type = OpenApiSecuritySchemeType.ApiKey,
    });
});
builder.Services.AddInfrastructure();
await builder.Services.ApplyMigrations();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(x => x.DocExpansion = "list");
}

app.MapGet("/", () => "It's Ecommerce API!");

ModuleLoader.RegisterModules(modules, app);

await app.RunAsync();
