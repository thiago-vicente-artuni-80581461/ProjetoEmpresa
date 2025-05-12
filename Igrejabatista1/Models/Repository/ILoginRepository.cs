using IgrejaBatista1.Models.ValueObjects;

namespace IgrejaBatista1.Models.Repository
{
    public interface ILoginRepository
    {
        PerfilVO RecuperarLoginPerfil(string loginUsuario);
        IEnumerable<LoginVO> RecuperarUsuariosLogin(string nome, string usuarioLogin, int departamentoTipoId);
        void SalvarRegistroAcesso(LoginVO login);
        bool ValidarLoginUsuario(LoginVO login);
    }
}
