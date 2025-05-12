using IgrejaBatista1.Models.Repository;
using IgrejaBatista1.Models.ValueObjects;

namespace IgrejaBatista1.Models.Services
{
    public class CaixaService : ICaixaService
    {
        public readonly ICaixaRepository _caixaRepository;

        public CaixaService(ICaixaRepository caixaRepository)
        {
            _caixaRepository = caixaRepository;
        }

        public IEnumerable<DepartamentoIgrejaVO> RecuperarListaCaixa(int departamentoTipoId, string usuarioLogin)
        {
            return _caixaRepository.RecuperarListaCaixa(departamentoTipoId, usuarioLogin);
        }

        public IEnumerable<SaidaDadosVO> RecuperarListaSaida(int departamentoTipoId, string tipoConta, string dataSaida, string usuarioLogin)
        {
            return _caixaRepository.RecuperarListaSaida(departamentoTipoId, tipoConta, dataSaida, usuarioLogin);
        }

        public void SalvarSaida(SaidaVO saida)
        {
           _caixaRepository.SalvarSaida(saida);
        }

        public Saida BuscarDadosSaida(int id)
        {
            return _caixaRepository.BuscarDadosSaida(id);
        }

        public IEnumerable<DepartamentoIgrejaVO> RecuperarListaCaixaRelatorio(int departamentoTipoId, int? mes, int? ano, string usuarioLogin)
        {
            return _caixaRepository.RecuperarListaCaixaRelatorio(departamentoTipoId, mes, ano, usuarioLogin);
        }

        public IEnumerable<DepartamentoIgrejaVO> RecuperarListaEntradaRelatorio(int departamentoTipoId, int? mes, int? ano, string usuarioLogin)
        {
            return _caixaRepository.RecuperarListaEntradaRelatorio(departamentoTipoId, mes, ano, usuarioLogin);
        }

        public IEnumerable<SaidaDadosVO> RecuperarListaSaidaRelatorio(int departamentoTipoId, int? mes, int? ano, string usuarioLogin)
        {
            return _caixaRepository.RecuperarListaSaidaRelatorio(departamentoTipoId, mes, ano,usuarioLogin);
        }

        public void ExcluirSaida(SaidaVO vo)
        {
            _caixaRepository.ExcluirSaida(vo);
        }
    }
}
