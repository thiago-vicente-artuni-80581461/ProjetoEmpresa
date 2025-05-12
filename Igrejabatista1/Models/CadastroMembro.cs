using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace IgrejaBatista1.Models
{
    [Table("CadastroMembro")]
    public class CadastroMembro
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("NomeCompleto")]
        [Required]
        [StringLength(200)]
        public string NomeCompleto { get; set; }

        [Column("DataNascimento")]
        [Required]
        public DateTime DataNascimento { get; set; }

        [Column("CPF")]
        [Required]
        [StringLength(200)]
        public string CPF { get; set; }

        [Column("RG")]
        [Required]
        [StringLength(20)]
        public string RG { get; set; }

        [Column("NomeMae")]
        [Required]
        [StringLength(200)]
        public string NomeMae { get; set; }

        [Column("NomePai")]
        [Required]
        [StringLength(200)]
        public string NomePai { get; set; }

        [Column("DataBatismo")]
        [Required]
        public DateTime DataBatismo { get; set; }

        [Column("DataEmissao")]
        public DateTime DataEmissao { get; set; }

        [Column("CargoId")]
        public int? CargoId { get; set; }

        [Column("Ativo")]
        public bool Ativo { get; set; }
    }
}
