namespace Catalog.API.Products.GetProducts;

public record GetProductsQuery() : IQuery<GetProductsResult>;

// !Wichtig: Der Name "Products" ist so gewählt, dass er mit dem Property-Namen in GetPoductsResponse übereinstimmt.
public record GetProductsResult(IEnumerable<Product> Products);

internal class GetProductsQueryHandler(IDocumentSession documentSession, ILogger<GetProductsQueryHandler> logger)
    : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        // query wird nicht verwendet! => Separation
        logger.LogInformation("GetProductQueryHandler.Handle clled with {@Query}", query);

        var products = await documentSession.Query<Product>().ToListAsync(cancellationToken);

        return new GetProductsResult(products);
    }
}
