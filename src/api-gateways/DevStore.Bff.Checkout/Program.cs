using DevStore.Bff.Checkout.Configuration;
using DevStore.WebAPI.Core.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

if (builder.Environment.IsDevelopment())
    builder.Configuration.AddUserSecrets<Program>();

#region Configure Services

builder.Services.AddApiConfiguration(builder.Configuration);

builder.Services.AddApiConfiguration(builder.Configuration);

builder.Services.AddJwtConfiguration(builder.Configuration);

builder.Services.AddSwaggerConfiguration();

builder.Services.RegisterServices();

builder.Services.AddMessageBusConfiguration(builder.Configuration);

builder.Services.ConfigureGrpcServices(builder.Configuration);


var app = builder.Build();
#endregion

#region Configure Pipeline


app.UseSwaggerConfiguration();

app.UseApiConfiguration(app.Environment);

app.Run();

#endregion