using Marten;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCarter();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());



builder.Services.AddMarten(opts =>
    {
        opts.Connection(builder.Configuration.GetConnectionString("Database"));
    })
    .UseLightweightSessions();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.MapCarter();

app.Run();