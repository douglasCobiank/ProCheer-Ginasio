using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Ginasio.Infrastructure.Data
{
    public class GinasioDbContextFactory: IDesignTimeDbContextFactory<GinasioDbContext>
    {
        public GinasioDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<GinasioDbContext>();

            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=procampdb;Username=postgres;Password=postgres;KeepAlive=30");

            return new GinasioDbContext(optionsBuilder.Options);
        }
    }
}