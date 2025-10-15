using AutoMapper;
using Ginasio.Api.Models;
using Ginasio.Core.DTOs;
using Ginasio.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ginasio.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GinasioController(IGinasioHandler ginasioHandler, IMapper mapper) : ControllerBase
    {
        private readonly IGinasioHandler _ginasioHandler = ginasioHandler;
        private readonly IMapper _mapper = mapper;

        [HttpPost("cadastrar")]
        public async Task<IActionResult> CriarGinasioAsync([FromBody] Gym gym)
        {
            var ginasio = _mapper.Map<GinasioDto>(gym);
            await _ginasioHandler.CadastrarGinasioHandler(ginasio);

            return Ok(new[] { $"Ginasio criado" });
        }

        [HttpPost("editar")]
        public async Task<IActionResult> EditarGinasio([FromBody] Gym gym, int id)
        {
            var ginasio = _mapper.Map<GinasioDto>(gym);
            await _ginasioHandler.EditarGinasioAsync(ginasio, id);

            return Ok(new[] { $"Ginasio editado" });
        }

        [HttpPost("deletar/{id}")]
        public async Task<IActionResult> DeletarGinasio(int id)
        {
            await _ginasioHandler.DeletaGinasioAsync(id);

            return Ok(new[] { $"Ginasio deletado" });
        }

        [HttpPost("buscar-ginasio")]
        public async Task<IActionResult> BuscarGinasios()
        {
            var response = await _ginasioHandler.BuscaGinasioAsync();

            return Ok(new[] { response });
        }

        [HttpPost("buscar-ginasio-por-nome/{nome}")]
        public async Task<IActionResult> BuscarGinasios(string nome)
        {
            var response = await _ginasioHandler.BuscaGinasioPorNomeAsync(nome);

            return Ok(new[] { response });
        }

        [HttpPost("buscar-ginasio-por-id/{id}")]
        public async Task<IActionResult> BuscarGinasioPorId(int id)
        {
            var response = await _ginasioHandler.GetGinasioPorIdAsync(id);

            return Ok(new[] { response });
        }
        
        [HttpPost("adiciona-logo")]
        public async Task<IActionResult> AdicionaLogoGinasio([FromForm] UploadLogoDto arquivo)
        {
            if (arquivo == null || arquivo?.Arquivo?.Length == 0)
                return BadRequest("Nenhum arquivo enviado.");

            byte[] imagemBytes;

            using (var ms = new MemoryStream())
            {
                await arquivo.Arquivo.CopyToAsync(ms);
                imagemBytes = ms.ToArray();
            }

            string base64String = Convert.ToBase64String(imagemBytes);
            await _ginasioHandler.AdicionaLogoGinasioAsync(arquivo.Id, base64String);


            return Ok(new { Mensagem = "Upload realizado com sucesso!", Tamanho = imagemBytes.Length });
        }
    }
}