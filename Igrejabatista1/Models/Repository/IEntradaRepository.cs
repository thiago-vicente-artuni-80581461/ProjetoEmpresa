using IgrejaBatista1.Models.ValueObjects;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IgrejaBatista1.Models.Repository
{
    public interface IEntradaRepository
    {
        void ExcluirEntrada(EntradaVO registro);
        IEnumerable<SelectListItem> RecuperarDadosCadastroMembro();
        IEnumerable<SelectListItem> RecuperarDadosContribuicaoTipo();
        IEnumerable<SelectListItem> RecuperarDadosDepartamentoTipo(int perfilId, int departamentoId);
        IEnumerable<SelectListItem> RecuperarDadosEvento();
        Entrada RecuperarInformacoesEntrada(int id);
        IEnumerable<EntradaVO> RecuperarListaEntrada(int perfilId, int departamentoTipoId);
        void SalvarEntrada(EntradaVO entrada);
    }
}
