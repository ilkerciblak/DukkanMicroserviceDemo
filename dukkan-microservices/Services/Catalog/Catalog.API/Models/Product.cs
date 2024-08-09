using Catalog.API.Models.Products.CreateProduct;

namespace Catalog.API.Models;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<string> Category { get; set; } = new List<string>();
    public decimal Price { get; set; }
    public string ImageFile { get; set; } = string.Empty;


    public static Product FromCreateProductCommand(CreateProductCommand command)
    {
        return new Product
        {
            Name = command.Name,
            Description = command.Description,
            Category = command.Category,
            Price = command.Price,
            ImageFile = command.ImageFile
        };
    }
}