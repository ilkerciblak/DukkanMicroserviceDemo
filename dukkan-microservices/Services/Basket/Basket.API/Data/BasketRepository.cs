using Basket.API.Exception;
using Basket.API.Models;

namespace Basket.API.Data;

public class BasketRepository(IDocumentSession session): IBasketRepository
{
    public async Task<ShoppingCart> GetBasket(string username, CancellationToken cancellationToken)
    {
        var cart = await session.LoadAsync<ShoppingCart>(username, cancellationToken);
        if (cart is null)
        {
            throw new BasketNotFoundException(username);
        }

        return cart;
    }

    public async Task<ShoppingCart> StoreBasket(ShoppingCart shoppingCart, CancellationToken cancellationToken)
    {
         session.Store<ShoppingCart>(shoppingCart);
         await session.SaveChangesAsync(cancellationToken);
         return shoppingCart;
    }

    public async Task<bool> DeleteBasket(string username, CancellationToken cancellationToken)
    {
        session.Delete<ShoppingCart>(username);
        await session.SaveChangesAsync(cancellationToken);

        return true;
    }
}