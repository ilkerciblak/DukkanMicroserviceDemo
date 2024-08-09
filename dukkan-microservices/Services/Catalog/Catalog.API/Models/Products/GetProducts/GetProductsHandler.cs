using BuildingBlocks.CQRS;
using Marten;

namespace Catalog.API.Models.Products.GetProducts;

public record GetProductsQuery() : IQuery<GetProductsResult>;

public record GetProductsResult(IEnumerable<Product> Products);

internal class GetProductsQueryHandler(IDocumentSession session, ILogger<GetProductsQueryHandler> logger)
    : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductsQuery Trigger with {@Query}", query);

        var result = await session.Query<Product>().ToListAsync();

        return new GetProductsResult(result);

    }
}