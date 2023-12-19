using System;
using System.Collections.Generic;

namespace ProjetoFinal.Models
{
    public partial class Vacinador
    {
        public Vacinador()
        {
            VacinacaoSalas = new HashSet<VacinacaoSala>();
        }

        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Senha { get; set; } = null!;
        public int? FkPostoSaude { get; set; }

        public virtual SaudePosto? FkPostoSaudeNavigation { get; set; }
        public virtual ICollection<VacinacaoSala> VacinacaoSalas { get; set; }
    }
}
