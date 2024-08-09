namespace Catalog.API.Models.Products.GetProductById;

// public record GetProductByIdQueryRequest(Guid Id);

public record GetProductByIdQueryResponse(Product Product);

public class GetProductByIdQueryEnpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
            pattern: "/products/{id}",
            async (Guid id,ISender sender) =>
            {
                var result = await sender.Send(new GetProductByIdQuery(id));
                var response = result.Adapt<GetProductByIdQueryResponse>();
                
                return Results.Ok(response);
            }
        );
    }
}