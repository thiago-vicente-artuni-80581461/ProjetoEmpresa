namespace IgrejaBatista1.Models.ValueObjects
{
    public class SaidaDadosVO
    {
        public int Id { get; set; }
        public int DepartamentoTipoId { get; set; }

        public string TipoConta { get; set; }

        public string Descricao { get; set; }

        public decimal ValorPago { get; set; }

        public DateTime DataSaida { get; set; }

        public DateTime DataCriacao { get; set; }

        public string DepartamentoTipoDescricao { get; set; }
    }
}
