namespace BuildingBlocks.Exceptions;

public class InternalServerException : Exception
{
    public string Detail { get; private set; }

    public InternalServerException(string message) : base(message)
    {
        
    }

    public InternalServerException(string message, string detail) : base(message)
    {
        this.Detail = detail;
    }
}