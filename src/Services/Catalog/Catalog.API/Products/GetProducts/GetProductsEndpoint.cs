namespace Catalog.API.Products.GetProducts;


// Wird nicht verwendet!
public record GetProductsRequest();

public record GetPoductsResponse(IEnumerable<Product> Products);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender) =>
        {
            var products = await sender.Send(new GetProductsQuery());

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

