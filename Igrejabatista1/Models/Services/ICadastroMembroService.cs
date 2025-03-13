using IgrejaBatista1.Models.ValueObjects;

namespace IgrejaBatista1.Models.Services
{
    public interface ICadastroMembroService
    {
        void ExcluirCadastroMembro(CadastroMembro cadastroMembro);
        List<CadastroMembro> RecuperarListaMembros();

        void SalvarCadastroMembro(CadastroMembro cadastroMembro);
    }
}
