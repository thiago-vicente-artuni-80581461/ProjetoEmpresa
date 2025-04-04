using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IgrejaBatista1.Models
{

    [Table("CadastroPatrimonio")]
    public class CadastroPatrimonio
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("DepartamentoTipoId")]
        [Required]
        public int DepartamentoTipoId { get; set; }

        [Column("Codigo")]
        [Required]
        public string Codigo { get; set; }

        [Column("Nome")]
        [Required]
        public string Nome { get; set; }

        [Column("Descricao")]
        public string Descricao { get; set; }

        [Column("Setor")]
        [Required]
        public string Setor { get; set; }

        [Column("DataBaixa")]
        public DateTime DataBaixa { get; set; }

        [Column("Foto")]
        [Required]
        public string Foto { get; set; }

        [Column("DataCriacao")]
        public DateTime DataCriacao { get; set; }

        [Column("TamanhoFoto")]
        public long TamanhoFoto { get; set; }

    }
}
