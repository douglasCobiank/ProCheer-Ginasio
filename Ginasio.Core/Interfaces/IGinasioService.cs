using Ginasio.Core.DTOs;

namespace Ginasio.Core.Interfaces
{
    public interface IGinasioService
    {
        Task AddGinasioAsync(GinasioDto user);
        Task<List<GinasioDto>> GetGinasioAsync();
        Task EditarGinasioAsync(GinasioDto user, int id);
        Task DeletaGinasioAsync(int id);
        Task<GinasioDto> GetGinasioPorNomeAsync(string nome);
        Task AdicionaLogoGinasioAsync(int id, string imagem);
    }
}