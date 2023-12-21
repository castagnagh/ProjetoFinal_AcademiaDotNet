using System.ComponentModel.DataAnnotations;

namespace ProjetoFinal.Models
{
    public class Procedimento
    {
        public int Id { get; set; }
        [MaxLength(50)]
        [Required(ErrorMessage = "Descrição do procedimento é obrigatório")]
        public string Descricao { get; set; }

        public virtual ICollection<RegistroManutencao> registroManutencoes { get; set; }
    }
}
