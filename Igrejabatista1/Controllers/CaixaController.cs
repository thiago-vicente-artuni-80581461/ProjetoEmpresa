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
            ViewData["Nome"] = HttpContext.Session.GetString("Nome");
            int departamentoTipoId = Convert.ToInt32(HttpContext.Session.GetString("DepartamentoTipoId"));
            var lista = _caixaService.RecuperarListaCaixa(departamentoTipoId);

            return View(lista);
        }

        [HttpGet]
        public IActionResult IndexSaida(string tipoConta, string dataSaida, string dataSaidaFim)
        {
            ViewData["Nome"] = HttpContext.Session.GetString("Nome");
            int perfilId = Convert.ToInt32(HttpContext.Session.GetString("Perfil"));
            int departamentoTipoId = Convert.ToInt32(HttpContext.Session.GetString("DepartamentoTipoId"));
            var lista = _caixaService.RecuperarListaSaida(departamentoTipoId, tipoConta, dataSaida, dataSaidaFim);

            return View(lista);
        }

        [HttpGet]

        public IActionResult CadastroSaida([FromRoute] int Id)
        {
            SaidaVO saida = new SaidaVO();
            int perfilId = Convert.ToInt32(HttpContext.Session.GetString("Perfil"));
            int departamentoTipoId = Convert.ToInt32(HttpContext.Session.GetString("DepartamentoTipoId"));
            saida.DepartamentoTipo = _entradaService.RecuperarDadosDepartamentoTipo(perfilId, departamentoTipoId);

            if (Id != 0)
            {
                //saida.DepartamentoTipoId = 
            }

            return View(saida);
        }

        [HttpPost]
        public IActionResult SalvarSaida(SaidaVO saida)
        {
            _caixaService.SalvarSaida(saida);
            return RedirectToAction("IndexSaida", "Caixa");
        }
    }
}
