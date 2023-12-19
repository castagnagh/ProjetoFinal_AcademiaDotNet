using System;
using System.Collections.Generic;

namespace ProjetoFinal.Models
{
    public partial class VacinacaoSala
    {
        public int Id { get; set; }
        public int QtdDisponivel { get; set; }
        public TimeSpan? HorarioAbertura { get; set; }
        public TimeSpan? HorarioFechamento { get; set; }
        public int? FkVacinador { get; set; }
        public int? FkVacina { get; set; }
        public int? FkPosto { get; set; }

        public virtual SaudePosto? FkPostoNavigation { get; set; }
        public virtual Vacina? FkVacinaNavigation { get; set; }
        public virtual Vacinador? FkVacinadorNavigation { get; set; }
    }
}
