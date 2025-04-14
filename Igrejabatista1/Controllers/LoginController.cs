using IgrejaBatista1.Models;
using IgrejaBatista1.Models.Services;
using IgrejaBatista1.Models.ValueObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Web;
using Microsoft.AspNetCore;

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
        public IActionResult Logar(LoginVO login)
        {
            try
            {
                bool retorno = LoginService.ValidarLoginUsuario(login);

                if (retorno)
                {
                    var recuperarPefilLogin = LoginService.RecuperarLoginPerfil(login.LoginUsuario);

                    HttpContext.Session.SetString("Perfil", recuperarPefilLogin.Id.ToString());

                    HttpContext.Session.SetString("DepartamentoTipoId", recuperarPefilLogin.DepartamentoTipoId.ToString());

                    HttpContext.Session.SetString(SessionNome, login.LoginUsuario);

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
