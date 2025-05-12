using IgrejaBatista1.Models.ValueObjects;

namespace IgrejaBatista1.Models.Repository
{
    public interface IPatrimonioRepository
    {
        void ExcluirCadastroPatrimonio(CadastroPatrimonioVO vo);
        IEnumerable<CadastroPatrimonioVO> RecuperarListaPatrimonio(int departamentoTipoId, string codigo, string nome);
        void SalvarPatrimonio(CadastroPatrimonioVO patrimonio);
    }
}
