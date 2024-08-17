namespace Order.Domain.Abstractions;

public abstract class Aggregate : Entity, IAggregate
{
    public readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public IDomainEvent[] ClearDomainEvents()
    {
        IDomainEvent[] list = _domainEvents.ToArray();
        _domainEvents.Clear();

        return list;
    }
}