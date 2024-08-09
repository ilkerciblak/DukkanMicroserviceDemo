using Marten;

namespace Catalog.API.Models.Products.DeleteProduct;

// public record DeleteProductRequest(Guid Id);

public record DeleteProductResponse(bool Success);

public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id}", async (Guid id, ISender sender) =>
        {

            var result = await sender.Send(new DeleteProductCommand(Id: id));
            var response = result.Adapt<DeleteProductResponse>();

            return Results.Ok(response);
        })
        .WithName("DeleteProduct")
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .Produces<DeleteProductResponse>(StatusCodes.Status202Accepted);
    }
}