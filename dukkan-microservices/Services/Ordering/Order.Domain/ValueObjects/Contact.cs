namespace Order.Domain.ValueObjects;

public class Contact
{
    public string? Email { get; private set; } = default!;
    public string? PhoneNumber { get; private set; } = default!;

    public Contact(string email, string phoneNumber)
    {
        Email = email;
        PhoneNumber = phoneNumber;
    }
}