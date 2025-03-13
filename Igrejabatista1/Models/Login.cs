using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IgrejaBatista1.Models
{
    [Table("Login")]
    public class Login
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("Usuario")]
        [Required]
        [StringLength(200)]
        public string LoginUsuario { get; set; }

        [Column("Senha")]
        [Required]
        [StringLength(200)]
        public string Senha { get; set; }
    }
}
