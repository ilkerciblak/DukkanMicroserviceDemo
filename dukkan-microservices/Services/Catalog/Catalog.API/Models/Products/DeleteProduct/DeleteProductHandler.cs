using BuildingBlocks.CQRS;
using Marten;

namespace Catalog.API.Models.Products.DeleteProduct;

public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;

public record DeleteProductResult(bool Success);

internal class DeleteProductCommandHandler(IDocumentSession session, ILogger<DeleteProductCommandHandler> logger)
    : ICommandHandler<DeleteProductCommand, DeleteProductResult>  
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteProductHandler triggered");
        
        session.Delete<Product>(id: request.Id);
        await session.SaveChangesAsync(cancellationToken);

        return new DeleteProductResult(true);

    }
}