using Uis.Api.Extensions;
using Uis.Infrastructure;
using Uis.Infrastructure.Repositories.Users;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddApi();
// builder.Services.AddApplication();
// builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// app.RegisterUserRepository();
app.RegisterApi();

await app.RunAsync();
