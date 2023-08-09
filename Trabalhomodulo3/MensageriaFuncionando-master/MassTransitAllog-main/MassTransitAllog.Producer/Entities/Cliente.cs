using System.Text.Json.Serialization;

namespace MassTransitAllog.Producer.Entities 
{
    public class Cliente 
    {
        public int Id { get; set; }

        public int Id_cidade { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Endereco { get; set; } = string.Empty;

        public string Telefone { get; set; } = string.Empty;

        public Cidade? Cidade {get; set;}
    }
}
