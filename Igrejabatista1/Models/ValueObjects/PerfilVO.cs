using System.ComponentModel.DataAnnotations.Schema;

namespace IgrejaBatista1.Models.ValueObjects
{
    public class PerfilVO
    {
        public int Id { get; set; }
        public int TipoPerfilId { get; set; }
        public int DepartamentoTipoId { get; set; }
        public DateTime DataCriacao { get; set; }
        public int LoginId { get; set; }
        public string PerfilTipo { get; set; }
    }
}
