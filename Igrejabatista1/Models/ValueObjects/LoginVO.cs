using Microsoft.AspNetCore.Mvc.Rendering;

namespace IgrejaBatista1.Models.ValueObjects
{
    public class LoginVO
    {
        public int Id { get; set; }
        public string LoginUsuario { get; set; }
        public string Senha { get; set; }
        public string Mensagem { get; set; }
        public string PerfilLogin { get; set; }
        public int PerfilTipoId { get; set; }
        public List<string> DepartamentoTipoId { get; set; }

        public string DepartamentoTipoDescricao { get; set; }
        public IEnumerable<SelectListItem> PerfilTipo { get; set; }

        public IEnumerable<SelectListItem> DepartamentoTipo { get; set; }
    }
}
