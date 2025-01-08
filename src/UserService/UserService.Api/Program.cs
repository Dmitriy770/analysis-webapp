using Uis.Api;
using Uis.Application;
using Uis.Infrastructure;
using Uis.Infrastructure.Repositories.Users;

var builder = WebApplication.CreateSlimBuilder(args);

builder.AddApi();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.Services.MigrateUserRepository();
app.UseApi();

await app.RunAsync();
