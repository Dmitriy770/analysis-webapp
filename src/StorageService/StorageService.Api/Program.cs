using StorageService.Api;
using StorageService.Application;
using StorageService.Infrastructure;

var builder = WebApplication.CreateSlimBuilder(args);
builder.Services.AddApiServices();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();
app.MapApiServices();

await app.RunAsync();
