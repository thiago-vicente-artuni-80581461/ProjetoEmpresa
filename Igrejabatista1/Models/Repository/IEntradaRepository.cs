using IgrejaBatista1.Models.ValueObjects;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IgrejaBatista1.Models.Repository
{
    public interface IEntradaRepository
    {
        void ExcluirEntrada(EntradaVO registro);
        IEnumerable<SelectListItem> RecuperarDadosCadastroMembro();
        IEnumerable<SelectListItem> RecuperarDadosContribuicaoTipo();
        IEnumerable<SelectListItem> RecuperarDadosDepartamentoTipo(string usuarioLogin, int departamentoId);
        IEnumerable<SelectListItem> RecuperarDadosEvento();
        Entrada RecuperarInformacoesEntrada(int id);
        IEnumerable<EntradaVO> RecuperarListaEntrada(int perfilId, int departamentoTipoId, int? mes, int? ano, string membro, string usuarioLogin);
        void SalvarEntrada(EntradaVO entrada);
        Entrada VerificarEntradaMembro(int id);

        int RecuperarPerfilCorreto(string usuarioLogin, int departamentoId);
    }
}
