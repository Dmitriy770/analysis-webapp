using Uis.Api.Extensions;
using Uis.Infrastructure;
using Uis.Infrastructure.Repositories.Users;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

await app.RegisterUserRepository();
app.RegisterApi();

await app.RunAsync();
