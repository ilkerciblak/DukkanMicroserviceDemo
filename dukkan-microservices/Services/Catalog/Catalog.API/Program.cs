var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCarter();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.MapCarter();

app.Run();