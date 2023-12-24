using System.ComponentModel.DataAnnotations;

namespace ProjetoFinal.Models
{
    public class TipoComputador
    {

        public int Id { get; set; }
        [MaxLength(50)]
        [Required(ErrorMessage = "Selecione o tipo de computador")]
        public string Nome { get; set; }

        public virtual ICollection<Computador>? Computadores { get; set; }
    }
}
