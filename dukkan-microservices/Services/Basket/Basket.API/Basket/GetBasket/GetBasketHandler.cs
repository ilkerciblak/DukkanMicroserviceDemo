using Basket.API.Data;
using Basket.API.Models;

namespace Basket.API.Basket.GetBasket;

public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;

public record GetBasketResult(ShoppingCart Cart);

internal class GetBasketHandler(IBasketRepository repo) : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {
        var cart = await repo.GetBasket(request.UserName, cancellationToken);
        return new GetBasketResult(cart);
    }
}