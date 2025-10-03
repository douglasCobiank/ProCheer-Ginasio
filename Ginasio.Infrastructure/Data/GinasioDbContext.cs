using Ginasio.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Ginasio.Infrastructure.Data
{
    public class GinasioDbContext(DbContextOptions<GinasioDbContext> options) : DbContext(options)
    {
        public DbSet<GinasioData> Ginasio { get; set; }
        public DbSet<EnderecoData> Endereco { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GinasioData>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<EnderecoData>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}