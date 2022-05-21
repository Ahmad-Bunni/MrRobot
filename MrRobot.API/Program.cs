using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using MrRobot.Core;
using MrRobot.Domain;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDomain(); // includes fluent validations

builder.Services.AddCore(); // includes mediator injection

builder.Services
    .AddControllers()
    .AddFluentValidation()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mr Robot APIs", Version = "v1" });
});

var app = builder.Build();

await app.InitializeDatabase();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "Running!");
app.UseHttpsRedirection();

app.MapControllers();

app.Run();
