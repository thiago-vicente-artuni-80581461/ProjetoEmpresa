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

        public Perfil RecuperarLoginPerfil(string loginUsuario)
        {
            return LoginRepository.RecuperarLoginPerfil(loginUsuario);
        }
    }
}
