using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace Ginasio.Core.DTOs
{
    public class GinasioDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = "";
        public EnderecoDto Endereco { get; set; } = new EnderecoDto();
        public string Responsavel { get; set; } = "";
        public string Telefone { get; set; } = "";
        public string Email { get; set; } = "";
        public string? ImagemLogo { get; set; }
        public int Tipo { get; set; }
    }
}