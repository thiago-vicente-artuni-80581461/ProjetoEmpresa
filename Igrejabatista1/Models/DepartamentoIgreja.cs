using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IgrejaBatista1.Models
{
    [Table("DepartamentoIgreja")]
    public class DepartamentoIgreja
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("DepartamentoTipoId")]
        public int DepartamentoTipoId { get; set; }

        [Column("ValorTotal")]
        public decimal ValorTotal { get; set; }

        [Column("DataCriacao")]
        public DateTime DataCriacao { get; set; }
    }
}
