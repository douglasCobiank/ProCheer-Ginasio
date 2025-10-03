using Ginasio.Core.DTOs;
using Ginasio.Core.Interfaces;

namespace Ginasio.Core
{
    public class GinasioHandler(IGinasioService ginasioService) : IGinasioHandler
    {
        private readonly IGinasioService _ginasioService = ginasioService;
        public async Task<List<GinasioDto>> BuscaGinasioAsync()
        {
            return await _ginasioService.GetGinasioAsync();
        }

        public async Task<GinasioDto> BuscaGinasioPorNomeAsync(string nome)
        {
            return await _ginasioService.GetGinasioPorNomeAsync(nome);
        }

        public async Task CadastrarGinasioHandler(GinasioDto ginasioDto)
        {
            await _ginasioService.AddGinasioAsync(ginasioDto);
        }

        public async Task DeletaGinasioAsync(int id)
        {
            await _ginasioService.DeletaGinasioAsync(id);
        }

        public async Task EditarGinasioAsync(GinasioDto ginasioDto, int id)
        {
            await _ginasioService.EditarGinasioAsync(ginasioDto, id);
        }

        public async Task AdicionaLogoGinasioAsync(int id, string imagemBytes)
        {
            await _ginasioService.AdicionaLogoGinasioAsync(id, imagemBytes);
        }
    }
}