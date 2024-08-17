namespace Order.Domain.ValueObjects;

public class Address
{
    public string? AddressTitle { get;  } = default!;
    public string? Country { get;  } = default!;
    public string? State { get;  } = default!;
    public string? ZipCode { get;  } = default!;
    
    public Address(string addressTitle, string country, string state, string zipCode)
    {
        AddressTitle = addressTitle;
        Country = country;
        State = state;
        ZipCode = zipCode;
    }
}