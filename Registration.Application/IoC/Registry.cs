using Lamar;
using Lamar.Scanning.Conventions;
using Registration.Application.Customers;

namespace Registration.Application.IoC;

public class Registry : ServiceRegistry
{
    public Registry()
    {
        Scan(s =>
        {
            s.AssemblyContainingType<ICustomerService>();
            s.LookForRegistries();
            s.WithDefaultConventions(OverwriteBehavior.Never);
        });
    }
}
