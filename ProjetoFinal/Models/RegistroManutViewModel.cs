namespace ProjetoFinal.Models
{
    public class RegistroManutViewModel
    {
        public RegistroManutencao RegistroManutencao { get; set; }
        public DateTime DataManutencao { get; set; }
        public DateTime DataPrevisao { get; set; }
        public int ProcedimentoId { get; set; }
        public virtual Procedimento Procedimento { get; set; }
        public int ComputadorId { get; set; }
        public virtual Computador Computador { get; set; }
        public List<Computador> ComputadorList { get; set; }
        public List<Marca> Marcas { get; set; }
        public List<TipoComputador> TiposComputador { get; set; }
        public List<Secao> Secoes { get; set; }
    }
}
