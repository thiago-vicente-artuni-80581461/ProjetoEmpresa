using IgrejaBatista1.Models;
using IgrejaBatista1.Models.Services;
using IgrejaBatista1.Models.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace IgrejaBatista1.Controllers
{
    public class CaixaController : Controller
    {
        private readonly ICaixaService _caixaService;
        private readonly IEntradaService _entradaService;

        public CaixaController(ICaixaService caixaService, IEntradaService entradaService)
        {
            _caixaService = caixaService;
            _entradaService = entradaService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                ViewData["Nome"] = HttpContext.Session.GetString("Nome");

                if (ViewData["Nome"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }

                int departamentoTipoId = Convert.ToInt32(HttpContext.Session.GetString("DepartamentoTipoId"));
                var lista = _caixaService.RecuperarListaCaixa(departamentoTipoId);

                return View(lista);
            }
            catch (ValidationException)
            {
                throw;
            }
           
        }

        [HttpGet]
        public IActionResult IndexSaida(string tipoConta, string dataSaida)
        {
            try
            {
                ViewData["Nome"] = HttpContext.Session.GetString("Nome");

                if (ViewData["Nome"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }

                int perfilId = Convert.ToInt32(HttpContext.Session.GetString("Perfil"));
                int departamentoTipoId = Convert.ToInt32(HttpContext.Session.GetString("DepartamentoTipoId"));
                var lista = _caixaService.RecuperarListaSaida(departamentoTipoId, tipoConta, dataSaida);

                return View(lista);
            }
            catch (ValidationException)
            {
                throw;
            }
           
        }

        [HttpGet]

        public IActionResult CadastroSaida([FromRoute] int Id)
        {
            try
            {
                ViewData["Nome"] = HttpContext.Session.GetString("Nome");

                if (ViewData["Nome"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }

                SaidaVO saida = new SaidaVO();
                int perfilId = Convert.ToInt32(HttpContext.Session.GetString("Perfil"));
                int departamentoTipoId = Convert.ToInt32(HttpContext.Session.GetString("DepartamentoTipoId"));
                saida.DepartamentoTipo = _entradaService.RecuperarDadosDepartamentoTipo(perfilId, departamentoTipoId);

                if (Id != 0)
                {
                    var buscarDadosSaida = _caixaService.BuscarDadosSaida(Id);
                    saida.DepartamentoTipoId = buscarDadosSaida.DepartamentoTipoId;
                    saida.Descricao = buscarDadosSaida.Descricao;
                    saida.DataCriacao = buscarDadosSaida.DataCriacao;
                    saida.DataSaida = buscarDadosSaida.DataSaida;
                    saida.ValorPago = buscarDadosSaida.ValorPago;
                    saida.TipoConta = buscarDadosSaida.TipoConta;
                    saida.Id = buscarDadosSaida.Id;


                }

                return View(saida);
            }
            catch (ValidationException)
            {
                throw;
            }
          
        }

        [HttpPost]
        public IActionResult SalvarSaida(SaidaVO saida)
        {
            try
            {
                ViewData["Nome"] = HttpContext.Session.GetString("Nome");

                if (ViewData["Nome"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }

                _caixaService.SalvarSaida(saida);

                return RedirectToAction("IndexSaida", "Caixa");
            }
            catch (ValidationException)
            {
                throw;
            }
          
        }
    }
}
