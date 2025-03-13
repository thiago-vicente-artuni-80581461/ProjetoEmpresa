using IgrejaBatista1.Models.ValueObjects;

namespace IgrejaBatista1.Models.Repository
{
    public interface ICaixaRepository
    {
        Saida BuscarDadosSaida(int id);
        IEnumerable<DepartamentoIgrejaVO> RecuperarListaCaixa(int departamentoTipoId);
        IEnumerable<SaidaVO> RecuperarListaSaida(int departamentoTipoId, string tipoConta, string dataSaida, string dataSaidaFim);
        void SalvarSaida(SaidaVO saida);
    }
}
