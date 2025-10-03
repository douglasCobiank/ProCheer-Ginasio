using AutoMapper;
using Ginasio.Core.DTOs;
using Ginasio.Core.Interfaces;
using Ginasio.Infrastructure.Data.Models;
using Ginasio.Infrastructure.Repositories;

namespace Ginasio.Core.Services
{
    public class GinasioService(IGinasioRepository ginasioRepository, IMapper mapper) : IGinasioService
    {
        private readonly IGinasioRepository _ginasioRepository = ginasioRepository;
        private readonly IMapper _mapper = mapper;

        public async Task AddGinasioAsync(GinasioDto ginasio)
        {
            var ginasioData = _mapper.Map<GinasioData>(ginasio);
            ginasioData.ImagemLogo = "";
            await _ginasioRepository.AddAsync(ginasioData);
        }

        public async Task DeletaGinasioAsync(int id)
        {
            var ginasioResult = await _ginasioRepository.GetByIdAsync(id);

            if (ginasioResult is not null)
                await _ginasioRepository.DeleteAsync(ginasioResult);
        }

        public async Task EditarGinasioAsync(GinasioDto ginasio, int id)
        {
            var ginasioResult = _ginasioRepository.GetByIdAsync(id).Result;

            if (ginasioResult is not null)
            {
                ginasioResult = _mapper.Map<GinasioData>(ginasio);
                ginasioResult.Endereco = _mapper.Map<EnderecoData>(ginasio.Endereco);

                await _ginasioRepository.EditAsync(ginasioResult);
            }
        }

        public async Task<List<GinasioDto>> GetGinasioAsync()
        {
            var ginasioData = await _ginasioRepository.GetAllWithIncludeAsync();
            return _mapper.Map<List<GinasioDto>>(ginasioData);
        }

        public async Task<GinasioDto> GetGinasioPorNomeAsync(string nome)
        {
            var ginasioData = await _ginasioRepository.GetGinasioPorNomeAsync(nome);
            return _mapper.Map<GinasioDto>(ginasioData);
        }

        public async Task AdicionaLogoGinasioAsync(int id, string imagem)
        {
            var ginasioResult = _ginasioRepository.GetByIdAsync(id).Result;

            if (ginasioResult is not null)
            {
                ginasioResult.ImagemLogo = imagem;

                await _ginasioRepository.EditAsync(ginasioResult);
            }
        }
    }
}