using IgrejaBatista1.Models.ValueObjects;

namespace IgrejaBatista1.Models.Services
{
    public interface IPatrimonioService
    {
        IEnumerable<CadastroPatrimonioVO> RecuperarListaPatrimonio(int departamentoTipoId, string codigo, string nome);
        void SalvarPatrimonio(CadastroPatrimonioVO patrimonio);
    }
}
