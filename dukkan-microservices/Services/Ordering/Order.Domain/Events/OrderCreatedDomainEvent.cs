namespace Order.Domain.Events;

public class OrderCreatedDomainEvent(Ordering order) : IDomainEvent
{
}