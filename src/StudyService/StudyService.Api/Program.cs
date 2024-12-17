using StudyService.Api;

var builder = WebApplication.CreateSlimBuilder(args);
builder.Services.AddApiServices();
builder.Services.AddApiServices();

var app = builder.Build();
app.MapApiServices();

await app.RunAsync();
