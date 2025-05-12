using IgrejaBatista1.Models.ValueObjects;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IgrejaBatista1.Models.Repository
{
    public interface ICadastroMembroRepository
    {
        void ExcluirCadastroMembro(CadastroMembrosVO cadastroMembro);
        List<string> RecuperarCargosCadastroMembro(int id);
        IEnumerable<SelectListItem> RecuperarDepartamentoTipo();
        IEnumerable<SelectListItem> RecuperarListaCargos();
        List<CadastroMembrosVO> RecuperarListaMembros();
        IEnumerable<SelectListItem> RecuperarPerfilTipo();
        void SalvarCadastroMembro(CadastroMembrosVO cadastroMembro);
        void SalvarCadastroUsuario(LoginVO login);
    }
}
