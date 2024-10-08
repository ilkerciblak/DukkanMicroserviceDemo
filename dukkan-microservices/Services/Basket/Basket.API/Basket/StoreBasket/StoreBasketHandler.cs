using Basket.API.Data;
using Basket.API.Models;
using Discount.Grpc;

namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>; 

public record StoreBasketResult(string UserName);

public class StoreBasketValidation : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketValidation()
    {
        RuleFor(x => x.Cart.Items).NotEmpty().WithMessage("Shopping Cart should be filled with items");
        RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("Username is required to identify the shopping cart, MF.");
    }
}

internal class StoreBasketHandler(IBasketRepository repo, DiscountGrpcService.DiscountGrpcServiceClient discountClient) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        foreach (var item in command.Cart.Items)
        {
             var discount = discountClient.GetDiscount(new GetCouponRequest{ProductName = item.ProductName}, cancellationToken: cancellationToken);
             item.Price -= discount.Amount;
        }
        var cart = await repo.StoreBasket(command.Cart, cancellationToken);
        
        return new StoreBasketResult(cart.UserName);
    }
}