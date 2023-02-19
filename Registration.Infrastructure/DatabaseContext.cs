using Microsoft.EntityFrameworkCore;
using Registration.Domain.Customers;

namespace Registration.Infrastructure;

public class DatabaseContext : DbContext
{
    public DatabaseContext() { }

    public DatabaseContext(DbContextOptions options) : base(options) { }

    public DbSet<Customer> Customers { get; set; } = null!;
}