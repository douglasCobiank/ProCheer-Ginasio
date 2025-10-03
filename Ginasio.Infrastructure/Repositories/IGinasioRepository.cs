using Ginasio.Infrastructure.Data.Models;

namespace Ginasio.Infrastructure.Repositories
{
    public interface IGinasioRepository
    {
        Task AddAsync(GinasioData ginasioData);
        Task EditAsync(GinasioData ginasioData);
        Task DeleteAsync(GinasioData ginasioData);
        Task<List<GinasioData>> GetAllWithIncludeAsync();
        Task<GinasioData?> GetByIdAsync(int id);
        Task<GinasioData?> GetGinasioPorNomeAsync(string nome);
    }
}