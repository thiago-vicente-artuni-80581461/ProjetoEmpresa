using AspNetCoreGeneratedDocument;
using IgrejaBatista1.Models.ValueObjects;

namespace IgrejaBatista1.Models.Services
{
    public interface ICaixaService
    {
        Saida BuscarDadosSaida(int id);
        IEnumerable<DepartamentoIgrejaVO> RecuperarListaCaixa(int departamentoTipoId);
        IEnumerable<SaidaDadosVO> RecuperarListaSaida(int departamentoTipoId, string tipoConta, string dataSaida, string dataSaidaFim);
        void SalvarSaida(SaidaVO saida);
    }
}
