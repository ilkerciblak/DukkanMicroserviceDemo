using Order.Domain.Abstractions;
using Order.Domain.ValueObjects;

namespace Order.Domain.Entities;

public class Customer : Entity
{
    public string Name { get; private set; } = default!;
    public Contact Contact { get; private set; } = default!;

    public static Customer Create(Guid id, string name, Contact contact)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(contact);

        return new Customer
        {
            Id = id,
            Name = name,
            Contact = contact
        };
    }
}