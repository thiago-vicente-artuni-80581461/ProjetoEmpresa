using IgrejaBatista1.Models.Repository;
using IgrejaBatista1.Models.ValueObjects;

namespace IgrejaBatista1.Models.Services
{
    public class LoginService : IloginService
    {
        public readonly ILoginRepository LoginRepository;
        public LoginService(ILoginRepository loginRepository)
        {
            LoginRepository = loginRepository;
        }

        public bool ValidarLoginUsuario(LoginVO login)
        {
            return LoginRepository.ValidarLoginUsuario(login);
        }

        public PerfilVO RecuperarLoginPerfil(string loginUsuario)
        {
            return LoginRepository.RecuperarLoginPerfil(loginUsuario);
        }

        public void SalvarRegistroAcesso(LoginVO login)
        {
            LoginRepository.SalvarRegistroAcesso(login);
        }

        public IEnumerable<LoginVO> RecuperarUsuariosLogin(string nome, string usuarioLogin, int departamentoTipoId)
        {
            return LoginRepository.RecuperarUsuariosLogin(nome, usuarioLogin, departamentoTipoId);
        }
    }
}
