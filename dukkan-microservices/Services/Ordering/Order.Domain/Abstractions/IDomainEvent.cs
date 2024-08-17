using MediatR;

namespace Order.Domain.Abstractions;

public interface IDomainEvent : INotification
{
    Guid EventId => Guid.NewGuid();
    public DateTime OccuredAt => DateTime.UtcNow;
    public string EventType => GetType().AssemblyQualifiedName;
}