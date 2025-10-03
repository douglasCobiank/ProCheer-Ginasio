using Ginasio.Infrastructure.Data;
using Ginasio.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Ginasio.Infrastructure.Repositories
{
    public class GinasioRepository(GinasioDbContext context) : IGinasioRepository
    {
        protected readonly GinasioDbContext _context = context;
        protected readonly DbSet<GinasioData> _dbSet = context.Set<GinasioData>();
        public async Task AddAsync(GinasioData ginasioData)
        {
            await _dbSet.AddAsync(ginasioData);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(GinasioData ginasioData)
        {
            if (_context.Entry(ginasioData).State == EntityState.Detached)
                _dbSet.Attach(ginasioData);

            _dbSet.Remove(ginasioData);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(GinasioData ginasioData)
        {
            _context.Entry(ginasioData).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<List<GinasioData>> GetAllWithIncludeAsync()
        {
            return await _dbSet.Include(d => d.Endereco).ToListAsync();
        }

        public async Task<GinasioData?> GetByIdAsync(int id)
        {
            return await _dbSet.Include(d => d.Endereco).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<GinasioData?> GetGinasioPorNomeAsync(string nome)
        {
            return await _dbSet.Include(d => d.Endereco).FirstOrDefaultAsync(a => a.Nome == nome);
        }
    }
}