// ReSharper disable NotAccessedPositionalProperty.Global

using BuildingBlocks.CQRS;
// ReSharper disable ClassNeverInstantiated.Global

namespace Catalog.API.Models.Products.CreateProduct;

public record CreateProductCommand(
    string Name,
    string Description,
    List<string> Category,
    decimal Price,
    string ImageFile
) : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Identifier);


internal class CreateProductHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        
        Console.WriteLine(command);
        var product = Product.FromCreateProductCommand(command: command);
        
        // DB Operation : Skipped for now

        return new CreateProductResult(Guid.NewGuid());
    }
}