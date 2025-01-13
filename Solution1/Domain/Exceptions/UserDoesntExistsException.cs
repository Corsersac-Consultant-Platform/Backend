namespace Domain.Exceptions;

public class UserDoesntExistsException : Exception
{
    public UserDoesntExistsException(string username) : base("User with username " + username + " doesn't exist.")
    {
    }
}