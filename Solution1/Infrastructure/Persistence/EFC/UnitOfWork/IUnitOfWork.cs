namespace Infrastructure.Persistence.EFC.UnitOfWork;

public interface IUnitOfWork
{
    Task CompleteAsync();
}