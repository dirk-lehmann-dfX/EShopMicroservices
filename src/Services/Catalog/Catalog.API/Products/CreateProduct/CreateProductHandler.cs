namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
        : ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required.");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required.");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be grater than 0.");
        }
    }

    // IValidator<CreateProductCommand> ersetzt durch ValidationBehavior<,>
    internal class CreateProductCommandHandler(IDocumentSession documentSession/*, IValidator<CreateProductCommand> validator*/,
        ILogger<CreateProductCommandHandler> logger)
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            // ehem. Validierung über IValidator<CreateProductCommand>
            //var result = await validator.ValidateAsync(command, cancellationToken);
            //var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
            //if (errors.Any())
            //{
            //    throw new ValidationException(errors.FirstOrDefault());
            //}

            logger.LogInformation("CreateProductCommandHandler.Handle called with {@Command}", command);

            // Business logic to create a product
            var productToCreate = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };

            documentSession.Store(productToCreate);
            await documentSession.SaveChangesAsync(cancellationToken);

            return new CreateProductResult(productToCreate.Id);
        }
    }
}
