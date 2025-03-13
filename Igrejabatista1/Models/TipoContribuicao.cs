using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IgrejaBatista1.Models
{
    [Table("TipoContribuicao")]
    public class TipoContribuicao
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("Tipo")]
        public string Tipo { get; set; }

        [Column("DataCriacao")]
        public DateTime DataCriacao { get; set; }
    }
}
