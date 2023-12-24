using System.ComponentModel.DataAnnotations;

namespace ProjetoFinal.Models
{
    public class Marca
    {

        public int Id { get; set; }
        [MaxLength(50)]
        [Required(ErrorMessage = "O nome da marca é obrigatório")]
        public string Nome { get; set; }

        public virtual ICollection<Computador>? Computadores { get; set; }
    }
}
