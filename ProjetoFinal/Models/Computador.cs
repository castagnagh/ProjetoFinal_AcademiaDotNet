using System.ComponentModel.DataAnnotations;

namespace ProjetoFinal.Models
{
    public class Computador
    {
        public int Id { get; set; }
        [MaxLength(50)]
        [Required(ErrorMessage = "Número do Patrimônio é obrigatório")]
        public string NumPatrimonio { get; set; }
        [MaxLength(100)]
        [Required(ErrorMessage = "Descrição do Computador é obrigatório")]
        public string Descricao { get; set; }

        public int SecaoId { get; set; }

        public virtual Secao Secao { get; set; }

        public virtual ICollection<RegistroManutencao> Manutencoes { get; set;}
    }
}
