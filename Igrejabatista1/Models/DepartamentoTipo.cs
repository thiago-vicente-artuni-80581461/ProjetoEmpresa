
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IgrejaBatista1.Models
{
    [Table("DepartamentoTipo")]
    public class DepartamentoTipo
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("Nome")]
        public string Nome { get; set; }

        [Column("DataCriacao")]
        public DateTime DataCriacao { get; set; }
    }
}
