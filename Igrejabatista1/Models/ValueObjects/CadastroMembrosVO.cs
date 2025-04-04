using Microsoft.AspNetCore.Mvc.Rendering;

namespace IgrejaBatista1.Models.ValueObjects
{
    public class CadastroMembrosVO
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; }
        public DateTime DataNascimento { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public string NomeMae { get; set; }
        public string NomePai { get; set; }
        public DateTime DataBatismo { get; set; }
        public DateTime DataEmissao { get; set; }
        public int? CargoId { get; set; }
        public IEnumerable<SelectListItem> Cargo { get; set; }

        public string CargoNome { get; set; }
        

    }
}
