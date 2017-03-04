using System.Data.Entity;

namespace DataStore
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(string connectionString) : base(connectionString)
        {}

        public DbSet<Customer> Customers { set; get; }
        public DbSet<Order> Orders { set; get; }
    }
}