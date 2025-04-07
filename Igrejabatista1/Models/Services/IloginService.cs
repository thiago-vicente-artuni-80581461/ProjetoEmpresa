using IgrejaBatista1.Models.ValueObjects;

namespace IgrejaBatista1.Models.Services
{
    public interface IloginService
    {
        Perfil RecuperarLoginPerfil(string loginUsuario);
        IEnumerable<LoginVO> RecuperarUsuariosLogin(string nome);
        void SalvarRegistroAcesso(LoginVO login);
        bool ValidarLoginUsuario(LoginVO login);
    }
}
