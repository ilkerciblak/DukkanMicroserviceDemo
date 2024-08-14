using System.Text.Json;
using Basket.API.Exception;
using Basket.API.Models;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.API.Data;

public class CachedBasketRepository(BasketRepository repo, IDistributedCache cache) : IBasketRepository
{
    public async Task<ShoppingCart> GetBasket(string username, CancellationToken cancellationToken)
    {
        var cachedCart = await cache.GetStringAsync(key: username, token: cancellationToken);
        
        if (cachedCart is not null)
        {
            return JsonSerializer.Deserialize<ShoppingCart>(cachedCart);
        }

        var cart = await repo.GetBasket(username, cancellationToken);

        if (cart is null)
        {
            throw new BasketNotFoundException(username);
        }
        
        await cache.SetStringAsync(username, JsonSerializer.Serialize(cart), cancellationToken);
        return cart;
    }

    public async Task<ShoppingCart> StoreBasket(ShoppingCart shoppingCart, CancellationToken cancellationToken)
    {
        await cache.SetStringAsync(shoppingCart.UserName, JsonSerializer.Serialize(shoppingCart), cancellationToken);
        var cart = await repo.StoreBasket(shoppingCart, cancellationToken);
        return cart;
    }

    public async Task<bool> DeleteBasket(string username, CancellationToken cancellationToken)
    {
        await cache.RemoveAsync(username, cancellationToken);
        await repo.DeleteBasket(username, cancellationToken);
        return true;
    }
}