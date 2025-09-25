namespace Basket.API.Basket.GetBasket;

public record GetBasketRequest(string UserName);

public record GetBasketResponse(ShoppingCart Cart);


public class GetBasketEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{userName}", async (ISender sender, string userName) =>
        {
            var basket = await sender.Send(new GetBasketQuery(userName));

            var response = basket.Adapt<GetBasketResponse>();

            return Results.Ok(response);
        })
            .WithName("GetBasketByName")
            .Produces<GetBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Basket By UserName")
            .WithDescription("Get Basket By UserName");
    }
}
