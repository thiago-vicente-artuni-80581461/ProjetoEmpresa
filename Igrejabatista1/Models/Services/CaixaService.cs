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

        public IEnumerable<SaidaVO> RecuperarListaSaida(int departamentoTipoId, string tipoConta, string dataSaida, string dataSaidaFim)
        {
            return _caixaRepository.RecuperarListaSaida(departamentoTipoId, tipoConta, dataSaida, dataSaidaFim);
        }

        public void SalvarSaida(SaidaVO saida)
        {
           _caixaRepository.SalvarSaida(saida);
        }

        public Saida BuscarDadosSaida(int id)
        {
            return _caixaRepository.BuscarDadosSaida(id);
        }
    }
}
