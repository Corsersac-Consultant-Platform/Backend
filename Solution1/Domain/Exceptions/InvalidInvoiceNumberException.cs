namespace Domain.Exceptions;

public class InvalidInvoiceNumberException : Exception
{
    public InvalidInvoiceNumberException(string message) : base(message)
    {
    }
}