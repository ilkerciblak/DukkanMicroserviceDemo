namespace BuildingBlocks.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
        
    }

    public NotFoundException(string name, object detail) : base(message: $"Entity {name} with id {detail} not found")
    {
        
    }
}