using StudyService.Api;
using StudyService.Application;
using StudyServices.Infrastructure;

var builder = WebApplication.CreateSlimBuilder(args);
builder.Services.AddApiServices();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();

var app = builder.Build();
app.MapApiServices();

await app.RunAsync();
