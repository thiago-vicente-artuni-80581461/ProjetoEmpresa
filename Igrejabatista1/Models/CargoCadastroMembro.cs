using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IgrejaBatista1.Models
{
    [Table("CargoCadastroMembro")]
    public class CargoCadastroMembro
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("CargoId")]
        public int CargoId { get; set; }

        [Column("MembroId")]
        public int? MembroId { get; set; }
    }
}
