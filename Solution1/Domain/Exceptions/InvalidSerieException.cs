namespace Domain.Exceptions;

public class InvalidSerieException : Exception
{
    public InvalidSerieException(string message) : base(message)
    {
    }
}