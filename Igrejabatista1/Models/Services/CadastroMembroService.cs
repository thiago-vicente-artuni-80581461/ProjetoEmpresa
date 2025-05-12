using IgrejaBatista1.Models.Repository;
using IgrejaBatista1.Models.ValueObjects;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IgrejaBatista1.Models.Services
{
    public class CadastroMembroService : ICadastroMembroService
    {
        public readonly ICadastroMembroRepository _cadastroMembroRepository;
        public CadastroMembroService(ICadastroMembroRepository cadastroMembroRepository)
        {
            _cadastroMembroRepository = cadastroMembroRepository;
        }

        public List<CadastroMembrosVO> RecuperarListaMembros()
        {
            return _cadastroMembroRepository.RecuperarListaMembros();
        }
        public void SalvarCadastroMembro(CadastroMembrosVO cadastroMembro)
        {
            _cadastroMembroRepository.SalvarCadastroMembro(cadastroMembro);
        }

        public void ExcluirCadastroMembro(CadastroMembrosVO cadastroMembro)
        {
            _cadastroMembroRepository.ExcluirCadastroMembro(cadastroMembro);
        }

        public IEnumerable<SelectListItem> RecuperarListaCargos()
        {
           return _cadastroMembroRepository.RecuperarListaCargos();
        }

        public IEnumerable<SelectListItem> RecuperarPerfilTipo()
        {
            return _cadastroMembroRepository.RecuperarPerfilTipo();
        }

        public IEnumerable<SelectListItem> RecuperarDepartamentoTipo()
        {
            return _cadastroMembroRepository.RecuperarDepartamentoTipo();
        }

        public void SalvarCadastroUsuario(LoginVO login)
        {
            _cadastroMembroRepository.SalvarCadastroUsuario(login);
        }

        public List<string> RecuperarCargosCadastroMembro(int id)
        {
           return _cadastroMembroRepository.RecuperarCargosCadastroMembro(id);
        }
    }
}
