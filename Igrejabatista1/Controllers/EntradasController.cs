
using IgrejaBatista1.Models;
using IgrejaBatista1.Models.Services;
using IgrejaBatista1.Models.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace IgrejaBatista1.Controllers
{
    [Authorize]
    public class EntradasController : Controller
    {
        private readonly IEntradaService _entradaService;

        private int perfilId = 0;
        public EntradasController(IEntradaService entradaService)
        {
            _entradaService = entradaService;
        }
        [HttpGet]
        public IActionResult Index(string membro = "", int? mes = null, int? ano = null)
        {
            try
            {
                ViewData["Nome"] = User.Identity.Name;

                if (ViewData["Nome"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                int departamentoTipoId = int.Parse(User.FindFirst("DepartamentoTipoId")?.Value);

                perfilId = int.Parse(User.FindFirst("Perfil")?.Value);

                IEnumerable<EntradaVO> entrada = null;
                entrada = _entradaService.RecuperarListaEntrada(perfilId, departamentoTipoId, mes, ano, membro, User.Identity.Name).ToList();

                return View(entrada);
            }
            catch (ValidationException)
            {
                throw;
            }
           
        }

        [HttpGet]

        public IActionResult CadastroEntrada([FromRoute] int Id)
        {
            try
            {
                EntradaVO entrada = new EntradaVO();

                ViewData["Nome"] = User.Identity.Name;

                if (ViewData["Nome"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }

                perfilId = int.Parse(User.FindFirst("Perfil")?.Value);
                int departamentoTipoId = int.Parse(User.FindFirst("DepartamentoTipoId")?.Value);

                entrada.CadastroMembro = _entradaService.RecuperarDadosCadastroMembro();

                entrada.ListaTipo = _entradaService.RecuperarDadosContribuicaoTipo();

                entrada.Evento = _entradaService.RecuperarDadosEvento();

                entrada.DepartamentoTipo = _entradaService.RecuperarDadosDepartamentoTipo(User.Identity.Name, departamentoTipoId);

                entrada.PerfilId = perfilId;

                if (Id != 0)
                {
                    var recuperarInformacoesEntrada = _entradaService.RecuperarInformacoesEntrada(Id);

                    entrada.MembroId = recuperarInformacoesEntrada.MembroId;
                    entrada.TipoId = recuperarInformacoesEntrada.TipoId;
                    entrada.DepartamentoTipoId = recuperarInformacoesEntrada.DepartamentoTipoId;
                    entrada.EventoId = recuperarInformacoesEntrada.EventoId;
                    entrada.ValorTotal = recuperarInformacoesEntrada.ValorTotal;
                    entrada.Mes = recuperarInformacoesEntrada.Mes;
                    entrada.Ano = recuperarInformacoesEntrada.Ano;
                }
                return View(entrada);
            }
            catch (ValidationException)
            {
                throw;
            }
           
        }

        [HttpPost]
        public IActionResult SalvarEntrada(EntradaVO entrada)
        {
            try
            {
                ViewData["Nome"] = User.Identity.Name;

                if (ViewData["Nome"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                entrada.UsuarioLogin = User.Identity.Name;
                entrada.PerfilTipo = User.FindFirst("PerfilTipo")?.Value;

                _entradaService.SalvarEntrada(entrada);
                return RedirectToAction("Index", "Entradas");
            }
            catch (ValidationException)
            {
                throw;
            }
           
        }

        [HttpPost]
        public IActionResult RemoverEntrada(int Id)
        {
            try
            {
                ViewData["Nome"] = User.Identity.Name;

                if (ViewData["Nome"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }

                perfilId = int.Parse(User.FindFirst("Perfil")?.Value);
                int departamentoTipoId = int.Parse(User.FindFirst("DepartamentoTipoId")?.Value);

                var lista = _entradaService.RecuperarListaEntrada(perfilId, departamentoTipoId, null, null, null, null);

                var registro = lista.FirstOrDefault(th => th.Id == Id);

                if (registro != null)
                {
                    _entradaService.ExcluirEntrada(registro);
                }
                return Json(true);
            }
            catch (ValidationException)
            {
                return RedirectToAction("SalvarCadastro", "Cadastro");
            }
        }
    }
}
