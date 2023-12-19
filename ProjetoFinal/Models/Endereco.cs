using System;
using System.Collections.Generic;

namespace ProjetoFinal.Models
{
    public partial class Endereco
    {
        public Endereco()
        {
            SaudePostos = new HashSet<SaudePosto>();
        }

        public int Id { get; set; }
        public string Cep { get; set; } = null!;
        public string Rua { get; set; } = null!;
        public string Numero { get; set; } = null!;
        public string Bairro { get; set; } = null!;
        public string Cidade { get; set; } = null!;
        public string Estado { get; set; } = null!;
        public string Complemento { get; set; } = null!;

        public virtual ICollection<SaudePosto> SaudePostos { get; set; }
    }
}
