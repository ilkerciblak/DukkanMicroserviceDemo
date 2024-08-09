// ReSharper disable NotAccessedPositionalProperty.Global

using BuildingBlocks.CQRS;
using Marten;

// ReSharper disable ClassNeverInstantiated.Global

namespace Catalog.API.Models.Products.CreateProduct;

public record CreateProductCommand(
    string Name,
    string Description,
    List<string> Category,
    decimal Price,
    string ImageFile
) : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);


internal class CreateProductHandler(IDocumentSession session, ILogger<CreateProductHandler> logger) : ICommandHandler<CreateProductCommand, CreateProductResult>  
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        
        logger.LogInformation("CreateProductHandler triggered with {@Command}", command);
        var product = Product.FromCreateProductCommand(command: command);
        
        // DB Operation : Skipped for now
        session.Store<Product>(product);
        await session.SaveChangesAsync(cancellationToken);
        
        return new CreateProductResult(product.Id);
    }
}