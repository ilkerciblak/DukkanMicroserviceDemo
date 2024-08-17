using System.Runtime.CompilerServices;

namespace Order.Domain.Entities;

public class Ordering : Aggregate
{
    public Guid CustomerId { get; private set; } = default!;
    public Address ShippingAddress { get; private set; } = default!;
    public Address BillingAddress { get; private set; } = default!;
    public Payment Payment { get; private set; } = default!;
    public OrderStatus OrderStatus { get; private set; } = OrderStatus.Pending;

    private readonly List<OrderItem> _orderItems = new();
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();

    public decimal TotalPrice
    {
        get => OrderItems.Sum(x => x.Price * x.Quantity);
        private set { }
    }


    public static Ordering Create(
        Guid customerId, 
        Address shippingAddress, 
        Address billingAddress,
        Payment payment
        //OrderStatus orderStatus
    )
    {
        var order = new Ordering
        {
            CustomerId = customerId,
            ShippingAddress = shippingAddress,
            BillingAddress = billingAddress,
            Payment = payment,
            OrderStatus = (OrderStatus)2,
        };

        order.AddDomainEvent(new OrderCreatedDomainEvent(order));
        
        return order;
    }

    public void Update(
        Guid? customerId = null,
        Address? shippingAddress = null,
        Address? billingAddress = null,
        Payment? payment = null,
        OrderStatus? orderStatus = null
    )
    {
        OrderStatus = orderStatus ?? this.OrderStatus;
        ShippingAddress = shippingAddress ?? this.ShippingAddress;
        BillingAddress = billingAddress ?? this.BillingAddress;
        Payment = payment ?? this.Payment;
        CustomerId = customerId ?? this.CustomerId;
        
        this.AddDomainEvent(new OrderUpdatedDomainEvent(this));
    }

    public void AddItem(Guid productId, int quantity, decimal price)
    {
        var orderItem = new OrderItem(productId: productId, quantity: quantity, price: price, orderId: Id);
        this.AddDomainEvent(new OrderUpdatedDomainEvent(this));
        _orderItems.Add(orderItem);
    }

    public void RemoveItem(Guid productId)
    {
        var p = OrderItems.FirstOrDefault(x => x.ProductId == productId);

        if (p is not null)
        {
            _orderItems.Remove(p);
            this.AddDomainEvent(new OrderUpdatedDomainEvent(this));
        }
    }
    
}