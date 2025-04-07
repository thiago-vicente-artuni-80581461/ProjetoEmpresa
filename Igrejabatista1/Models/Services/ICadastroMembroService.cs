using IgrejaBatista1.Models.ValueObjects;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IgrejaBatista1.Models.Services
{
    public interface ICadastroMembroService
    {
        void ExcluirCadastroMembro(CadastroMembrosVO cadastroMembro);
        IEnumerable<SelectListItem> RecuperarDepartamentoTipo();
        IEnumerable<SelectListItem> RecuperarListaCargos();
        List<CadastroMembrosVO> RecuperarListaMembros();
        IEnumerable<SelectListItem> RecuperarPerfilTipo();
        void SalvarCadastroMembro(CadastroMembro cadastroMembro);
        void SalvarCadastroUsuario(LoginVO login);
    }
}
