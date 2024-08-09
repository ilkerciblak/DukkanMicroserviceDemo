using BuildingBlocks.CQRS;
using JasperFx.Core;
using Marten;
using Marten.Pagination;

namespace Catalog.API.Models.Products.GetProducts;

public record GetProductsQuery(int? PageNumber = 1, int? PageSize = 10, string? Category = "")
    : IQuery<GetProductsResult>;

public record GetProductsResult(IEnumerable<Product> Products);

internal class GetProductsQueryHandler(IDocumentSession session, ILogger<GetProductsQueryHandler> logger)
    : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductsQuery Trigger with {@Query}", query);

        var result = await session.Query<Product>()
            .Where(p => p.Category.Contains(query.Category) || query.Category == "")
            .ToPagedListAsync(
                pageNumber: query.PageNumber ?? 1,
                pageSize: query.PageSize ?? 10,
                cancellationToken
            );

        return new GetProductsResult(result);
    }
}