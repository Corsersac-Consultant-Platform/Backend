using Support.Models;

namespace Domain.Interfaces;

public interface IBusinessRulesValidator
{
    void Validate(User user);
}