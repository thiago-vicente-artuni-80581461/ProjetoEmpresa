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
        const string SessionNome = "Nome";
        public LoginController(IloginService loginService)
        {
            LoginService = loginService;
        }

        public IActionResult Login()
        {
            return View();
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
                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction("Login", "Login");
            }
            catch (ValidationException)
            {
                throw;
            }
        }
    }
}
