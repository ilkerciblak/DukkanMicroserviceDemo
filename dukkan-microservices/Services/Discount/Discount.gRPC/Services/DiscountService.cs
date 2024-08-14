using Discount.Grpc;
using Discount.gRPC.Data;
using Discount.gRPC.Models;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Mapster;

namespace Discount.gRPC.Services;

public class DiscountService(DiscountContext dbContext) : DiscountGrpcService.DiscountGrpcServiceBase 
{
    public override async  Task<CouponModel> GetDiscount(GetCouponRequest request, ServerCallContext context)
    {
        var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
        return coupon is null
            ? new CouponModel
            {
                Id = 0,
                ProductName = "No Discount Found",
                Description = "No Discount Found",
                Amount = 0
            }
            : new CouponModel
            {
                Id = coupon.Id,
                ProductName = coupon.ProductName,
                Description = coupon.Description,
                Amount = coupon.Amount
            };
    }

    public override async Task<CouponProtoList> GetAllDiscount(Empty request, ServerCallContext context)
    {
        var list = await dbContext.Coupons.ToListAsync<Coupon>();
        var listA = list.Select(x => x.Adapt<CouponModel>()).ToList();
        var res = new CouponProtoList();
        res.List.Add(listA);
        return res;
    }

    public override async Task<CouponModel> CreateDiscount(CreateCouponRequest request, ServerCallContext context)
    {
        var coupon =  dbContext.Coupons.Add(new Coupon
        {

            ProductName = request.Coupon.ProductName,
            Description = request.Coupon.Description,
            Amount = request.Coupon.Amount
        });

        await dbContext.SaveChangesAsync(context.CancellationToken);

        return new CouponModel
        {
            Id = coupon.Entity.Id,
            ProductName = coupon.Entity.ProductName,
            Description = coupon.Entity.Description,
            Amount = coupon.Entity.Amount
        };
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateCouponRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();
        if (coupon is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));

        dbContext.Coupons.Update(coupon);
        await dbContext.SaveChangesAsync();


        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }

    public override async Task<DeleteCouponResponse> DeleteDiscount(DeleteCouponRequest request, ServerCallContext context)
    {
        var coupon = await dbContext
            .Coupons
            .FirstOrDefaultAsync(x => x.Id == request.CouponId);

        if (coupon is null)
            throw new RpcException(new Status(StatusCode.NotFound, $"Discount with CouponId={request.CouponId} is not found."));

        dbContext.Coupons.Remove(coupon);
        await dbContext.SaveChangesAsync();


        return new DeleteCouponResponse { Success = true };
    }
}