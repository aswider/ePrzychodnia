using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ePrzychodnia.Data
{
    public class DesignTimeDbContext : IDesignTimeDbContextFactory<ClinicDbContext>
    {
        public ClinicDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ClinicDbContext>();

            var connectionString =
                "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=clinic;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            builder.UseSqlServer(connectionString);

            return  new ClinicDbContext(builder.Options);
        }
    }
}