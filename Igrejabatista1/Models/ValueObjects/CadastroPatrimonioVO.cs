using Microsoft.AspNetCore.Mvc;

namespace IgrejaBatista1.Models.ValueObjects
{
    public class CadastroPatrimonioVO
    {
        public int Id { get; set; }
        public int DepartamentoTipoId { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Setor { get; set; }
        public DateTime? DataBaixa { get; set; }
        public string Foto { get; set; }
        public IFormFile Imagem { get; set; }
        public string DepartamentoDescricao { get; set; }

        public DateTime DataCriacao { get; set; }

        public long TamanhoFoto { get; set; }

        public FileContentResult FotoCarregada { get; set; }

        public IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> DepartamentoTipo { get; set; }

    }
}
