using System.Text.RegularExpressions;
using Domain.Exceptions;
using Domain.Interfaces;
using Support.Models;

namespace Domain.Validators;

public class BusinessRulesValidator : IBusinessRulesValidator
{
    readonly Regex _passwordReg = new(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$");
    private readonly DateTime _today = DateTime.Today;
    private readonly HashSet<string> _seriesAllowed = new() { "FDA1", "FCA1", "FRA1" };

    public void Validate(string password)
    {
        if (!_passwordReg.IsMatch(password))
        {
            throw new InvalidUserException("Password must contain at least one uppercase letter, one lowercase letter, one number, and be between 8 and 15 characters long");
        }
    }

    public void Validate(Invoice invoice)
    {
        if (invoice.EmitDate.Year > _today.Year && invoice.EmitDate.Month > _today.Month &&
            invoice.EmitDate.Day > _today.Day)
            throw new InvalidEmitDateException("Emit date must be in the past");

        if (!_seriesAllowed.Any(serie => serie == invoice.Serie))
        {
            throw new InvalidSerieException("This serie number is not allowed");
        }

        if (invoice.Number.Length != 8)
        {
            throw new InvalidInvoiceNumberException("Invoice number must be 8 characters long");
        }
        
        if (invoice.Amount <= 0)
        {
            throw new Exception("Amount must be greater than 0");
        }
    }

    public void Validate(Usage usage)
    {
        if (usage.Date.Year > _today.Year && usage.Date.Month > _today.Month &&
            usage.Date.Day > _today.Day)
            throw new InvalidUsageDateException("Usage cannot be in the future");

        if (usage.Quantity <= 0)
        {
            throw new Exception("Quantity must be greater than 0");
        }
    }
}
    