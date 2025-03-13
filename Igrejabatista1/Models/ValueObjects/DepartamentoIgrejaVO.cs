namespace IgrejaBatista1.Models.ValueObjects
{
    public class DepartamentoIgrejaVO
    {
        public int Id { get; set; }
        public int DepartamentoTipoId { get; set; }
        public decimal ValorTotal { get; set; }
        public string DeparamentoTipoDescricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public IEnumerable<DepartamentoTipo> Tipo { get; set; }
    }
}
