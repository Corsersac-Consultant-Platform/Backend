namespace Domain.Exceptions;

public class InvalidEmitDateException : Exception
{
    public InvalidEmitDateException(string message) : base(message)
    {
    }
}