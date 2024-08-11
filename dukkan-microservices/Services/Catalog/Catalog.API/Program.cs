using System.Text.Json;
using BuildingBlocks.Behaviours;
using BuildingBlocks.Behaviours.ValidationBehaviour;
using BuildingBlocks.Exceptions.Handler;
using Carter;
using FluentValidation;
using Marten;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add Services to the container
builder.Services.AddCarter();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<Program>();
    cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
});
builder.Services.AddMarten(
    configure: options =>
    {
        options.Connection(builder.Configuration.GetConnectionString("Database"));        
    }
).UseLightweightSessions();

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.MapCarter();

app.UseExceptionHandler(options => {});

// Configure the HTTP Request pipeline


app.Run();