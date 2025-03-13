using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IgrejaBatista1.Models.ValueObjects
{
    public class EntradaVO
    {
        public int Id { get; set; }
        public int? TipoId { get; set; }
        public int MembroId { get; set; }
        public int? EventoId { get; set; }
        public DateTime DataCriacao { get; set; }
        public decimal ValorTotal { get; set; }
        public int DepartamentoTipoId { get; set; }

        public int PerfilId { get; set; }
        public IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> ListaTipo { get; set; }

        public IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> Evento { get; set; }

        public IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> CadastroMembro { get; set; }

        public IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> DepartamentoTipo { get; set; }

        public string TipoDescricao { get; set; }

        public string MembroCadastroDescricao { get; set; }

        public string EventoDescricao { get; set; }

        public string DepartamentoTipoDescricao { get; set; }
    }
}
