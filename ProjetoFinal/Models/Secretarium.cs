using System;
using System.Collections.Generic;

namespace ProjetoFinal.Models
{
    public partial class Secretarium
    {
        public Secretarium()
        {
            SaudePostos = new HashSet<SaudePosto>();
        }

        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Senha { get; set; } = null!;

        public virtual ICollection<SaudePosto> SaudePostos { get; set; }
    }
}
