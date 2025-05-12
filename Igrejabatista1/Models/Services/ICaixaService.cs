using AspNetCoreGeneratedDocument;
using IgrejaBatista1.Models.ValueObjects;

namespace IgrejaBatista1.Models.Services
{
    public interface ICaixaService
    {
        Saida BuscarDadosSaida(int id);
        void ExcluirSaida(SaidaVO vo);
        IEnumerable<DepartamentoIgrejaVO> RecuperarListaCaixa(int departamentoTipoId, string usuarioLogin);
        IEnumerable<DepartamentoIgrejaVO> RecuperarListaCaixaRelatorio(int departamentoTipoId, int? mes, int? ano, string usuarioLogin);
        IEnumerable<DepartamentoIgrejaVO> RecuperarListaEntradaRelatorio(int departamentoTipoId, int? mes, int? ano, string usuarioLogin);
        IEnumerable<SaidaDadosVO> RecuperarListaSaida(int departamentoTipoId, string tipoConta, string dataSaida, string usuarioLogin);
        IEnumerable<SaidaDadosVO> RecuperarListaSaidaRelatorio(int departamentoTipoId, int? mes, int? ano, string usuarioLogin);
        void SalvarSaida(SaidaVO saida);
    }
}
