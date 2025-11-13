using Ordering.Application.Orders.Commands.DeleteOrder;

namespace Ordering.API.Endpoints;

// Accepts a order Id as a parameter
// Constructs a DeleteOrderCommand
// Use MediatR to send the command to the corresponding handler
// Returns a success or not found response

// Nicht benötigt, da nur Id und diese wird direkt im Rest-Endpunkt angegeben
//public record DeleteOrderRequest(Guid Id);

public record DelteOrderResponse(bool IsSuccess);

public class DeleteOrder
    : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/orders/{id}", async (Guid Id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteOrderCommand(Id));
            var response = result.Adapt<DelteOrderResponse>();

            return Results.Ok(response);
        })
            .WithName("DeleteOrder")
            .Produces<DelteOrderResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Delete Order")
            .WithDescription("Delete Order");
    }
}
