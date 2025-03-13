using IgrejaBatista1.Models.Repository;
using IgrejaBatista1.Models.ValueObjects;

namespace IgrejaBatista1.Models.Services
{
    public class CadastroMembroService : ICadastroMembroService
    {
        public readonly ICadastroMembroRepository _cadastroMembroRepository;
        public CadastroMembroService(ICadastroMembroRepository cadastroMembroRepository)
        {
            _cadastroMembroRepository = cadastroMembroRepository;
        }

        public List<CadastroMembro> RecuperarListaMembros()
        {
            return _cadastroMembroRepository.RecuperarListaMembros();
        }
        public void SalvarCadastroMembro(CadastroMembro cadastroMembro)
        {
            _cadastroMembroRepository.SalvarCadastroMembro(cadastroMembro);
        }

        public void ExcluirCadastroMembro(CadastroMembro cadastroMembro)
        {
            _cadastroMembroRepository.ExcluirCadastroMembro(cadastroMembro);
        }
    }
}
