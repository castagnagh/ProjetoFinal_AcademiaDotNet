using System;
using System.Collections.Generic;

namespace ProjetoFinal.Models
{
    public partial class Vacina
    {
        public Vacina()
        {
            VacinacaoSalas = new HashSet<VacinacaoSala>();
        }

        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Descricao { get; set; } = null!;
        public string Lote { get; set; } = null!;
        public int QtdEstoque { get; set; }
        public DateTime Validade { get; set; }

        public virtual ICollection<VacinacaoSala> VacinacaoSalas { get; set; }
    }
}
