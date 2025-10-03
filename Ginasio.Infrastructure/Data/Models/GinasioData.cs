using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace Ginasio.Infrastructure.Data.Models
{
    public class GinasioData
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; } = "";
        public EnderecoData Endereco { get; set; } = new EnderecoData();
        public string Responsavel { get; set; } = "";
        public string Telefone { get; set; } = "";
        public string Email { get; set; } = "";
        public string? ImagemLogo { get; set; }
        public int Tipo { get; set; }
    }
}