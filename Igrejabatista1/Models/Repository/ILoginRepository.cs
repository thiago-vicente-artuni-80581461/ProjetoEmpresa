using IgrejaBatista1.Models.ValueObjects;

namespace IgrejaBatista1.Models.Repository
{
    public interface ILoginRepository
    {
        Perfil RecuperarLoginPerfil(string loginUsuario);
        bool ValidarLoginUsuario(LoginVO login);
    }
}
