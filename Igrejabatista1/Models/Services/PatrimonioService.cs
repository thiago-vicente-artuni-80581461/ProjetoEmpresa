using IgrejaBatista1.Models.Repository;
using IgrejaBatista1.Models.ValueObjects;

namespace IgrejaBatista1.Models.Services
{
    public class PatrimonioService : IPatrimonioService
    {
        public readonly IPatrimonioRepository patrimonioRepository = null;

        public PatrimonioService(IPatrimonioRepository patrimonioRepository)
        {
            this.patrimonioRepository = patrimonioRepository;
        }

        public IEnumerable<CadastroPatrimonioVO> RecuperarListaPatrimonio(int departamentoTipoId, string codigo, string nome)
        {
            return patrimonioRepository.RecuperarListaPatrimonio(departamentoTipoId, codigo, nome);
        }

        public void SalvarPatrimonio(CadastroPatrimonioVO patrimonio)
        {
            patrimonioRepository.SalvarPatrimonio(patrimonio);
        }
    }
}
