using IgrejaBatista1.Models.ValueObjects;

namespace IgrejaBatista1.Models.Services
{
    public interface IloginService
    {
        PerfilVO RecuperarLoginPerfil(string loginUsuario);
        IEnumerable<LoginVO> RecuperarUsuariosLogin(string nome, string usuarioLogin, int departamentoTipoId);
        void SalvarRegistroAcesso(LoginVO login);
        bool ValidarLoginUsuario(LoginVO login);
    }
}
