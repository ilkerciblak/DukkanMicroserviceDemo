using Basket.API.Models;
using Mapster;
using MediatR;

namespace Basket.API.Basket.GetBasket;

public record GetBasketResponse(ShoppingCart Cart);

public class GetBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "/basket/{username}",
            async (string username, ISender sender) =>
            {
                var query = new GetBasketQuery(username);
                var result = await sender.Send(query);
                var response = result.Adapt<GetBasketResponse>();
                return Results.Ok(response);
            }
        );
    }
}