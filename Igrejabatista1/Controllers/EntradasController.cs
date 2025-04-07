using IgrejaBatista1.Models;
using IgrejaBatista1.Models.Services;
using IgrejaBatista1.Models.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace IgrejaBatista1.Controllers
{
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
                ViewData["Nome"] = HttpContext.Session.GetString("Nome");
                perfilId = Convert.ToInt32(HttpContext.Session.GetString("Perfil"));
                int departamentoTipoId = Convert.ToInt32(HttpContext.Session.GetString("DepartamentoTipoId"));

                IEnumerable<EntradaVO> entrada = null;
                entrada = _entradaService.RecuperarListaEntrada(perfilId, departamentoTipoId, mes, ano, membro).ToList();

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
                ViewData["Nome"] = HttpContext.Session.GetString("Nome");
                perfilId = Convert.ToInt32(HttpContext.Session.GetString("Perfil"));
                int departamentoTipoId = Convert.ToInt32(HttpContext.Session.GetString("DepartamentoTipoId"));

                entrada.CadastroMembro = _entradaService.RecuperarDadosCadastroMembro();

                entrada.ListaTipo = _entradaService.RecuperarDadosContribuicaoTipo();

                entrada.Evento = _entradaService.RecuperarDadosEvento();

                entrada.DepartamentoTipo = _entradaService.RecuperarDadosDepartamentoTipo(perfilId, departamentoTipoId);
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
                perfilId = Convert.ToInt32(HttpContext.Session.GetString("Perfil"));
                int departamentoTipoId = Convert.ToInt32(HttpContext.Session.GetString("DepartamentoTipoId"));
                var lista = _entradaService.RecuperarListaEntrada(perfilId, departamentoTipoId, null, null, null);

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
