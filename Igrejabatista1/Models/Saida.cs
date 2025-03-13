using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IgrejaBatista1.Models
{
    [Table("Saida")]
    public class Saida
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("DepartamentoTipoId")]
        public int DepartamentoTipoId { get; set; }

        [Column("TipoConta")]
        public string TipoConta { get; set; }

        [Column("Descricao")]
        public string Descricao { get; set; }

        [Column("ValorPago")]
        public decimal ValorPago { get; set; }

        [Column("DataSaida")]
        public DateTime DataSaida { get; set; }

        [Column("DataCriacao")]
        public DateTime DataCriacao { get; set; }
    }
}
