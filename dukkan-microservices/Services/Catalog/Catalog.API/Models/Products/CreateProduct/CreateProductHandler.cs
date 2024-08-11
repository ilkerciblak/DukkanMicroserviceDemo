
using BuildingBlocks.CQRS;
using FluentValidation;
using Marten;



namespace Catalog.API.Models.Products.CreateProduct;

public record CreateProductCommand(
    string Name,
    string Description,
    List<string> Category,
    decimal Price,
    string ImageFile
) : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

public class CreateProductCommandValidation : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidation()
    {
        RuleFor(p => p.Name).NotEmpty().WithMessage("Product Name Should be Filled");
        RuleFor(p => p.Description).NotEmpty().WithMessage("Product Description Should be Filled");
        RuleFor(p => p.Price).GreaterThanOrEqualTo(Decimal.Zero)
            .WithMessage("Product Price should be greater than or equal to 0(zero)");
    }
}


internal class CreateProductHandler(IDocumentSession session, ILogger<CreateProductHandler> logger) : ICommandHandler<CreateProductCommand, CreateProductResult>  
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        
        logger.LogInformation("CreateProductHandler triggered with {@Command}", command);
        var product = Product.FromCreateProductCommand(command: command);
        
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);
        
        return new CreateProductResult(product.Id);
    }
}