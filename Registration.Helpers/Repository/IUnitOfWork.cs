namespace Registration.Helpers.Repository;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}