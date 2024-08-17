namespace Order.Domain.Entities;

public class Product : Entity
{
    public string Name { get; private set; } = default!;
    public decimal Price { get; private set; } = default!;

    public static Product Create(Guid id, string name, decimal price)
    {
        return new Product
        {
            Id = id,
            Name = name,
            Price = price,
        };
    }
}