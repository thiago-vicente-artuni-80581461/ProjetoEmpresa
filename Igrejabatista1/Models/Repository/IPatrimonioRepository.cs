using IgrejaBatista1.Models.ValueObjects;

namespace IgrejaBatista1.Models.Repository
{
    public interface IPatrimonioRepository
    {
        IEnumerable<CadastroPatrimonioVO> RecuperarListaPatrimonio(int departamentoTipoId, string codigo, string nome);
        void SalvarPatrimonio(CadastroPatrimonioVO patrimonio);
    }
}
