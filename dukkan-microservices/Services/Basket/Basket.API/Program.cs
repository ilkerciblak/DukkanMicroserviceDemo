using Basket.API.Data;
using Basket.API.Models;
using Discount.Grpc;
using JasperFx.Core;
using Microsoft.Extensions.Caching.Distributed;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddCarter();
services.AddMarten(
    opt =>
    {
        opt.Connection(builder.Configuration.GetConnectionString("Database"));
        opt.Schema.For<ShoppingCart>().Identity(x => x.UserName);
    }
).UseLightweightSessions();
services.AddMediatR(
    opt =>
    {
        opt.RegisterServicesFromAssemblyContaining(typeof(Program));
        opt.AddOpenBehavior(typeof(ValidationBehaviour<,>));
    }
);

services.AddStackExchangeRedisCache(
    opt => { opt.Configuration = builder.Configuration.GetConnectionString("Redis"); }
);

services.AddScoped<BasketRepository>();
services.AddScoped<IBasketRepository>(
    provider =>
    {
        var basketRepository = provider.GetRequiredService<BasketRepository>();
        return new CachedBasketRepository(basketRepository, provider.GetRequiredService<IDistributedCache>());
    }
);

services.AddGrpcClient<DiscountGrpcService.DiscountGrpcServiceClient>(options =>
        options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!
        ))
    .ConfigurePrimaryHttpMessageHandler(() =>
    {
        var handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback =
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

        return handler;
    });
;

services.AddValidatorsFromAssembly(typeof(Program).Assembly);
services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

app.MapCarter();
app.UseExceptionHandler(opt => { });

app.Run();