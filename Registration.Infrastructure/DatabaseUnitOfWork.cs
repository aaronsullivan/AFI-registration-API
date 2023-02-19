using Registration.Helpers.Repository;

namespace Registration.Infrastructure;

public class DatabaseUnitOfWork: IUnitOfWork
{
    private readonly DatabaseContext _dataContext;

    public DatabaseUnitOfWork(DatabaseContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task SaveChangesAsync()
    {
        await _dataContext.SaveChangesAsync();
    }
}
