using System;
using System.Collections.Generic;

namespace ProjetoFinal.Models
{
    public partial class SaudePosto
    {
        public SaudePosto()
        {
            VacinacaoSalas = new HashSet<VacinacaoSala>();
            Vacinadors = new HashSet<Vacinador>();
        }

        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Senha { get; set; } = null!;
        public int? FkEndereco { get; set; }
        public int? FkSecretaria { get; set; }

        public virtual Endereco? FkEnderecoNavigation { get; set; }
        public virtual Secretarium? FkSecretariaNavigation { get; set; }
        public virtual ICollection<VacinacaoSala> VacinacaoSalas { get; set; }
        public virtual ICollection<Vacinador> Vacinadors { get; set; }
    }
}
