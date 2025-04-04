
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IgrejaBatista1.Models
{
    [Table("Entrada")]
    public class Entrada
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("DepartamentoTipoId")]
        public int DepartamentoTipoId { get; set; }

        [Column("TipoId")]
        public int? TipoId { get; set; }

        [Column("MembroId")]
        public int? MembroId { get; set; }

        [Column("EventoId")]
        public int? EventoId { get; set; }

        [Column("DataCriacao")]
        public DateTime DataCriacao { get; set; }

        [Column("ValorTotal")]
        [Required]
        public decimal ValorTotal { get; set; }

        [Column("PerfilId")]
        [Required]
        public int PerfilId { get; set; }

        [Column("Mes")]
        [Required]
        public int? Mes { get; set; }

        [Column("Ano")]
        [Required]
        public int? Ano { get; set; }
    }
}
