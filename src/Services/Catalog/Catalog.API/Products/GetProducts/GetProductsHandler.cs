namespace Catalog.API.Products.GetProducts;

public record GetProductsQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetProductsResult>;

// !Wichtig: Der Name "Products" ist so gewählt, dass er mit dem Property-Namen in GetPoductsResponse übereinstimmt.
public record GetProductsResult(IEnumerable<Product> Products);

internal class GetProductsQueryHandler(IDocumentSession documentSession/*, ILogger<GetProductsQueryHandler> logger*/)
    : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        // query wird nicht verwendet! => Separation

        // ehem. Logging, läuft nun über LoggingBehavior
        //logger.LogInformation("GetProductQueryHandler.Handle clled with {@Query}", query);

        var products = await documentSession.Query<Product>().ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 10, cancellationToken);

        return new GetProductsResult(products);
    }
}
