using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication().AddJwtBearer("GatewayAuthenticationScheme", opt =>
{
    opt.Authority = builder.Configuration["IdentityServerURL"];
    opt.Audience = "ResourceGateway";
    opt.RequireHttpsMetadata = false;
});

builder.Services.AddOcelot();

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile($"configuration.{builder.Environment.EnvironmentName}.json")
    .AddEnvironmentVariables();


var app = builder.Build();

app.MapGet("/", () => "Hello World!");

await app.UseOcelot();

app.Run();
