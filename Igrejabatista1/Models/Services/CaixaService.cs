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

        public IEnumerable<DepartamentoIgrejaVO> RecuperarListaCaixa(int departamentoTipoId)
        {
            return _caixaRepository.RecuperarListaCaixa(departamentoTipoId);
        }

        public IEnumerable<SaidaDadosVO> RecuperarListaSaida(int departamentoTipoId, string tipoConta, string dataSaida)
        {
            return _caixaRepository.RecuperarListaSaida(departamentoTipoId, tipoConta, dataSaida);
        }

        public void SalvarSaida(SaidaVO saida)
        {
           _caixaRepository.SalvarSaida(saida);
        }

        public Saida BuscarDadosSaida(int id)
        {
            return _caixaRepository.BuscarDadosSaida(id);
        }

        public IEnumerable<DepartamentoIgrejaVO> RecuperarListaCaixaRelatorio(int departamentoTipoId, int? mes, int? ano)
        {
            return _caixaRepository.RecuperarListaCaixaRelatorio(departamentoTipoId, mes, ano);
        }
    }
}
