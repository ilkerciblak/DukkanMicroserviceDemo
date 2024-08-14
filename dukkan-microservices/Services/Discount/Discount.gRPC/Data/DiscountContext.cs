using Discount.gRPC.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.gRPC.Data;

public class DiscountContext : DbContext
{
    public DbSet<Coupon> Coupons { get; set; } = default!;

    public DiscountContext(DbContextOptions<DiscountContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Coupon>().HasData(
            new Coupon
            {
                Id = 1,
                Description = "Description",
                ProductName = "Iphone X",
                Amount = 10
            },
            new Coupon
            {
                Id = 2,
                Description = "Description 2",
                ProductName = "Iphone 2X",
                Amount = 20
            }
        );
    }
}