using BuildingBlocks.Behaviours.ValidationBehaviour;
using BuildingBlocks.Exceptions.Handler;
using FluentValidation;
using Marten;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCarter();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<Program>();
    cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
});

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
    
builder.Services.AddMarten(opts =>
    {
        opts.Connection(builder.Configuration.GetConnectionString("Database"));
    })
    .UseLightweightSessions();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseExceptionHandler(opt => { });

app.MapCarter();

app.Run();