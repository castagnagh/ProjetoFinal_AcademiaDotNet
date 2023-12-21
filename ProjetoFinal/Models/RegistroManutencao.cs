namespace ProjetoFinal.Models
{
    public class RegistroManutencao
    {
        public int Id { get; set; }
        public DateTime DataManutencao { get; set; }
        public DateTime DataPrevisao { get; set; }
        public int ComputadorId { get; set; }
        public int ProcedimentoId { get; set; }

        public virtual Computador Computador { get; set;}

        public virtual Procedimento Procedimento { get; set; }
    }
}
