using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IgrejaBatista1.Models
{
    [Table("RegistroAcesso")]
    public class RegistroAcesso
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("LoginId")]
        public int LoginId { get; set; }

        [Column("DataRegistro")]
        public DateTime DataRegistro { get; set; }

        [Column("Usuario")]
        public string Usuario { get; set; }
    }
}
