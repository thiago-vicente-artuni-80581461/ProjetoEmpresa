using IgrejaBatista1.Models.ValueObjects;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IgrejaBatista1.Models.Repository
{
    public interface ICadastroMembroRepository
    {
        void ExcluirCadastroMembro(CadastroMembrosVO cadastroMembro);
        IEnumerable<SelectListItem> RecuperarListaCargos();
        List<CadastroMembrosVO> RecuperarListaMembros();

        void SalvarCadastroMembro(CadastroMembro cadastroMembro);
    }
}
