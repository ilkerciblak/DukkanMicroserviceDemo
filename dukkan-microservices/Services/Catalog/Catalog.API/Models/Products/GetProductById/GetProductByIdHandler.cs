using BuildingBlocks.CQRS;
using Catalog.API.Models.Exceptions;
using Marten;

namespace Catalog.API.Models.Products.GetProductById;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

public record GetProductByIdResult(Product Product);

public class GetProductByIdHandler(IDocumentSession session) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var result =  await session.LoadAsync<Product>(request.Id, cancellationToken);

        if (result is null)
        {
            throw new ProductNotFoundException("Product", request.Id);
        }

        return new GetProductByIdResult(result);

    }
}