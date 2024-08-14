using Basket.API.Models;

namespace Basket.API.Data;

internal interface IBasketRepository
{
    public Task<ShoppingCart> GetBasket(string username, CancellationToken cancellationToken);

    public Task<ShoppingCart> StoreBasket(ShoppingCart shoppingCart, CancellationToken cancellationToken);

    public Task<bool> DeleteBasket(string username, CancellationToken cancellationToken);
}