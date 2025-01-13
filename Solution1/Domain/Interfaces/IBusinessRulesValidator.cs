using Support.Models;

namespace Domain.Interfaces;

public interface IBusinessRulesValidator
{
    void Validate(string password);
    void Validate(Invoice invoice);
}