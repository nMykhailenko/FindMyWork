namespace FindMyWork.Shared.Infrastructure.Database.Repositories;

public interface IRepository
{
    IUnitOfWork UnitOfWork { get; }
}