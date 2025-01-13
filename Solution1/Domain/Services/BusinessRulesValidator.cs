using System.Text.RegularExpressions;
using Domain.Exceptions;
using Domain.Interfaces;
using Support.Models;

namespace Domain.Services;

public class BusinessRulesValidator : IBusinessRulesValidator
{
    readonly Regex _passwordReg = new ("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{8,15}$");

    public void Validate(User user)
    {
        if (!Regex.IsMatch(user.Password, _passwordReg.ToString()))
            throw new InvalidUserException("Password must contain at least one uppercase letter, one lowercase letter, one number, and be between 8 and 15 characters long");

        if (user.Password.Length < 8)
        {
            throw new InvalidUserException("Password must be at least 8 characters long");
        }
    }
}
    