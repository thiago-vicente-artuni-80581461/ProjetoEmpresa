using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IgrejaBatista1.Models
{
    [Table("PerfilLogin")]
    public class PerfilLogin
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("PerfilId")]
        public int PerfilId { get; set; }

        [Column("LoginId")]
        public int LoginId { get; set; }
    }
}
