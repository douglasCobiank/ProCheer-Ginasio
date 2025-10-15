using Ginasio.Core.DTOs;

namespace Ginasio.Core.Interfaces
{
    public interface IGinasioHandler
    {
        Task CadastrarGinasioHandler(GinasioDto ginasioDto);
        Task<List<GinasioDto>> BuscaGinasioAsync();
        Task EditarGinasioAsync(GinasioDto ginasioDto, int id);
        Task DeletaGinasioAsync(int id);
        Task<GinasioDto> BuscaGinasioPorNomeAsync(string nome);
        Task AdicionaLogoGinasioAsync(int id, string imagemBytes);
        Task<GinasioDto> GetGinasioPorIdAsync(int id);
    }
}