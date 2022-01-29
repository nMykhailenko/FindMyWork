namespace FindMyWork.Shared.Infrastructure.Database.Repositories;

public abstract class Repository : IRepository
{
    public IUnitOfWork UnitOfWork { get; }

    protected Repository(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }
}