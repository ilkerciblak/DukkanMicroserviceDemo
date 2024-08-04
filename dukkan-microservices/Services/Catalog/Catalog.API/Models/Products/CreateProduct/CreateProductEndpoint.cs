// ReSharper disable NotAccessedPositionalProperty.Global
namespace Catalog.API.Models.Products.CreateProduct;

public record CreateProductRequest(
    string Name,
    string Description,
    List<string> Category,
    decimal Price,
    string ImageFile
);

public record CreateProductResponse(Guid Identifier);


public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(
                "/products",
                async (CreateProductRequest request, ISender sender) =>
                {
                    var command = request.Adapt<CreateProductCommand>();
                    var result = await sender.Send(command);
                    var response = result.Adapt<CreateProductResponse>();

                    return Results.Created($"/products/{response.Identifier}", response);
                })
            .WithName("CreateProduct")
            .WithDescription("Create Product Description")
            .WithSummary("Create Product Summary")
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .Produces<CreateProductResponse>(StatusCodes.Status201Created);
    }
}