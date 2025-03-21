namespace IgrejaBatista1.Models.ValueObjects
{
    public class DepartamentoIgrejaVO
    {
        public int Id { get; set; }
        public int DepartamentoTipoId { get; set; }
        public decimal ValorTotal { get; set; }
        public string DepartamentoTipoDescricao { get; set; }
        public decimal ValorReceita { get; set; }
        public decimal ValorContas { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
