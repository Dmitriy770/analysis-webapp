using Common.Web.Authorization;
using Common.Web.Authorization.Extensions;
using StorageService.Application;

var builder = WebApplication.CreateSlimBuilder(args);
// Add common
builder.Services.AddAuthorizationFilter();

builder.Services.AddApplicationServices();
builder.Services.AddControllers(options =>
{
    options.AddAuthorizationFilter();
});
builder.Services.AddOpenApi();

var app = builder.Build();
app.MapOpenApi();
app.MapControllers();

await app.RunAsync();
