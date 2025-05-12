using IgrejaBatista1.Models;
using IgrejaBatista1.Models.Services;
using IgrejaBatista1.Models.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace IgrejaBatista1.Controllers
{
    [Authorize]
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
                ViewData["Nome"] = User.Identity.Name;

                if (ViewData["Nome"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }

                int departamentoTipoId = int.Parse(User.FindFirst("DepartamentoTipoId")?.Value);
                var lista = _caixaService.RecuperarListaCaixa(departamentoTipoId, User.Identity.Name);

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
                ViewData["Nome"] = User.Identity.Name;

                if (ViewData["Nome"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }

                int perfilId = int.Parse(User.FindFirst("Perfil")?.Value);
                int departamentoTipoId = int.Parse(User.FindFirst("DepartamentoTipoId")?.Value);
                var lista = _caixaService.RecuperarListaSaida(departamentoTipoId, tipoConta, dataSaida, User.Identity.Name);

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
                ViewData["Nome"] = User.Identity.Name;

                if (ViewData["Nome"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }

                SaidaVO saida = new SaidaVO();
                int perfilId = int.Parse(User.FindFirst("Perfil")?.Value);
                int departamentoTipoId = int.Parse(User.FindFirst("DepartamentoTipoId")?.Value);
                saida.DepartamentoTipo = _entradaService.RecuperarDadosDepartamentoTipo(User.Identity.Name, departamentoTipoId);

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
                ViewData["Nome"] = User.Identity.Name;

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


        [HttpPost]
        public IActionResult RemoverSaida(int Id)
        {
            try
            {
                ViewData["Nome"] = User.Identity.Name;

                if (ViewData["Nome"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                int departamentoTipoId = int.Parse(User.FindFirst("DepartamentoTipoId")?.Value);

                var lista = _caixaService.RecuperarListaSaida(departamentoTipoId, "", "", User.Identity.Name);

                var registro = lista.FirstOrDefault(th => th.Id == Id);

                if (registro != null)
                {
                    SaidaVO vo = new SaidaVO()
                    {
                        Id = registro.Id,
                        DataCriacao = registro.DataCriacao,
                        DataSaida = registro.DataSaida,
                        DepartamentoTipoId = registro.DepartamentoTipoId,
                        Descricao = registro.Descricao,
                        TipoConta = registro.TipoConta,
                        ValorPago = registro.ValorPago

                    };
                    _caixaService.ExcluirSaida(vo);
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
