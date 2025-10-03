using System.Reflection.Metadata;

namespace Ginasio.Api.Models
{
    public class Gym
    {
        public string Nome { get; set; } = "";
        public Address Endereco { get; set; } = new Address();
        public string Responsavel { get; set; } = "";
        public string Telefone { get; set; } = "";
        public string Email { get; set; } = "";
        public int Tipo { get; set; }
    }
}