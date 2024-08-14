using Mapster;
using MediatR;

namespace Basket.API.Basket.DeleteBasket;

public class DeleteBasketResponse(bool Success);

public class DeleteBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete(
            pattern: "/basket/{username}",
            async (string username, ISender sender) =>
            {
                var command = new DeleteBasketCommand(username);
                var result = await sender.Send(command);
                var response = result.Adapt<DeleteBasketResponse>();
                Console.WriteLine("-------------------------");
                Console.WriteLine($"{response}-------------------------");
                return Results.Ok(response);
            });
    }
}