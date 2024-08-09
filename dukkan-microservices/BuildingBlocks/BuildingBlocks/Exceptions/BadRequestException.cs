namespace BuildingBlocks.Exceptions;

public class BadRequestException : Exception
{
    public string Detail { get; private set; }

    public BadRequestException(string message) : base(message)
    {
    }

    public BadRequestException(string message, string detail) : base(message)
    {
        this.Detail = detail;
    }
}