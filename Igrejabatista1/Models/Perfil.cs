using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IgrejaBatista1.Models
{
    [Table("Perfil")]
    public class Perfil
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("TipoPerfilId")]
        public int TipoPerfilId { get; set; }

        [Column("DepartamentoTipoId")]
        public int DepartamentoTipoId { get; set; }

        [Column("DataCriacao")]
        public DateTime DataCriacao { get; set; }
    }
}
