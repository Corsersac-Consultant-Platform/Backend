namespace Domain.Exceptions;

public class InvalidUsageDateException : Exception
{
    public InvalidUsageDateException(string message) : base(message)
    {
    }
}