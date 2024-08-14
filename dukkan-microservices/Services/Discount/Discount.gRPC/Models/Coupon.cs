namespace Discount.gRPC.Models;

public class Coupon
{
    public int Id { get; set; } = default!;
    public string ProductName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int Amount { get; set; } = default!;
}