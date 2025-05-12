using IgrejaBatista1.Models.Services;
using IgrejaBatista1.Models.ValueObjects;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IgrejaBatista1.Controllers
{
    public class LoginController : Controller
    {
        private readonly IloginService LoginService;
        private readonly ICadastroMembroService _cadastroMembroService;

        const string SessionNome = "Nome";
        public LoginController(IloginService loginService, ICadastroMembroService cadastroMembroService)
        {
            LoginService = loginService;
            _cadastroMembroService = cadastroMembroService;
        }

        [HttpGet]
        public IActionResult Login(string mensagem = "")
        {
            try
            {
                LoginVO login = new LoginVO();

                if (!string.IsNullOrEmpty(mensagem))
                {
                    login.Mensagem = mensagem;
                }
                return View(login);
            }
            catch (ValidationException)
            {
                throw;
            }

        }

        [HttpPost]
        public async Task<IActionResult> Logar(LoginVO login)
        {
            try
            {
                bool retorno = LoginService.ValidarLoginUsuario(login);

                if (retorno)
                {
                    var recuperarPefilLogin = LoginService.RecuperarLoginPerfil(login.LoginUsuario);

                    var claims = new[]
                       {
                          new Claim(ClaimTypes.Name, login.LoginUsuario),
                          new Claim("Perfil", recuperarPefilLogin.Id.ToString()),
                          new Claim("DepartamentoTipoId",  recuperarPefilLogin.DepartamentoTipoId.ToString() == "0" ? "1" : recuperarPefilLogin.DepartamentoTipoId.ToString()),
                          new Claim("PerfilTipo",  recuperarPefilLogin.PerfilTipo)
                      };

                    var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("L+H57jscLmFhbncdg2BxzKq/WPqOqaQzKsXYu0aL7gA="));

                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var tok = new JwtSecurityToken(
                        claims: claims,
                        expires: DateTime.UtcNow.AddHours(1),
                        signingCredentials: creds
                    );

                    var token = new JwtSecurityTokenHandler().WriteToken(tok);

                    Response.Cookies.Append("jwtToken", token, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Lax,
                        Expires = DateTimeOffset.UtcNow.AddHours(1)
                    });

                    LoginService.SalvarRegistroAcesso(login);

                    return RedirectToAction("Index", "Home");
                }
                var mensagem = "Usuário e/ou senha inválidos!!! Por favor, tente novamente!!!";

                return RedirectToAction("Login", "Login", new { mensagem });
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult CadastroUsuarioLogin()
        {
            try
            {
                LoginVO novo = new LoginVO();

                novo.PerfilTipo = _cadastroMembroService.RecuperarPerfilTipo();
                novo.DepartamentoTipo = _cadastroMembroService.RecuperarDepartamentoTipo();

                return View(novo);
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult SalvarUsuario(LoginVO login)
        {
            try
            {
                _cadastroMembroService.SalvarCadastroUsuario(login);
                return RedirectToAction("Login", "Login");
            }
            catch (ValidationException)
            {
                return RedirectToAction("SalvarUsuario", "Login");
            }
        }
    }
}
