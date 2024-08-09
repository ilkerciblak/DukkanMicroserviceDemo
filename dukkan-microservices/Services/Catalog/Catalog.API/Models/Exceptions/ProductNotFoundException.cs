using BuildingBlocks.Exceptions;

namespace Catalog.API.Models.Exceptions;

public class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException(string message) : base(message)
    {
    }

    public ProductNotFoundException(string name, object detail) : base(name, detail)
    {
    }
}