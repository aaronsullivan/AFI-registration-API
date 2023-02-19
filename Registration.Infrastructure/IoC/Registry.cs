using Lamar;
using Lamar.Scanning.Conventions;
using Registration.Helpers.Repository;

namespace Registration.Infrastructure.IoC;

public class Registry : ServiceRegistry
{
    public Registry()
    {
        Scan(s =>
        {
            s.TheCallingAssembly();
            s.LookForRegistries();
            s.WithDefaultConventions(OverwriteBehavior.Never);
        });

        For(typeof(IRepository<>)).Use(typeof(DatabaseRepository<>));
        For<IUnitOfWork>().Use<DatabaseUnitOfWork>();
    }
}
