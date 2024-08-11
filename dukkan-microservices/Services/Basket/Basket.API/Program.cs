var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddCarter();
services.AddMarten(
    opt => opt.Connection(builder.Configuration.GetConnectionString("Database"))
).UseLightweightSessions();
services.AddMediatR(
    opt =>
    {
        opt.RegisterServicesFromAssemblyContaining(typeof(Program));
        opt.AddOpenBehavior(typeof(ValidationBehaviour<,>));
    }
);
services.AddValidatorsFromAssembly(typeof(Program).Assembly);
services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

app.MapCarter();
app.UseExceptionHandler(opt => { });

app.Run();