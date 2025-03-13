using IgrejaBatista1.Models.ValueObjects;

namespace IgrejaBatista1.Models.Repository
{
    public interface ICadastroMembroRepository
    {
        void ExcluirCadastroMembro(CadastroMembro cadastroMembro);
        List<CadastroMembro> RecuperarListaMembros();

        void SalvarCadastroMembro(CadastroMembro cadastroMembro);
    }
}
