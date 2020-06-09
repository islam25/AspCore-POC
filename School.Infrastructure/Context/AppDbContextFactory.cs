using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace School.Infrastructure.Context
{
    public class AppDbContextFactory: IDesignTimeDbContextFactory<SchoolContext>
    {
        public SchoolContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SchoolContext>();
            optionsBuilder.UseSqlServer("server=(localdb)\\MSSQLLocalDB;database=school;trusted_connection=true;");

            return new SchoolContext(optionsBuilder.Options);
        }
    }
}
