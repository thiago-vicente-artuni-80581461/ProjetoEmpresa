using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IgrejaBatista1.Models
{
    [Table("Evento")]
    public class Evento
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("Nome")]
        public string Nome { get; set; }
    }
}
