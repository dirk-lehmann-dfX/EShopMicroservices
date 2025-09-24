namespace Catalog.API.Products.GetProducts;


public record GetProductsRequest(int? PageNumber = 1, int? PageSize = 10);

public record GetPoductsResponse(IEnumerable<Product> Products);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender, [AsParameters] GetProductsRequest request) =>
        {
            var query = request.Adapt<GetProductsQuery>();

            var products = await sender.Send(query);

            var response = products.Adapt<GetPoductsResponse>();

            return Results.Ok(response);
        })
        .WithName("GetProducts")
        .Produces<GetPoductsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products")
        .WithDescription("Get Products");
    }
}

