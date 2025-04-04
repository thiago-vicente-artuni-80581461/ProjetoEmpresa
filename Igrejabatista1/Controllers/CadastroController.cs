using IgrejaBatista1.Models;
using IgrejaBatista1.Models.Services;
using IgrejaBatista1.Models.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace IgrejaBatista1.Controllers
{
    public class CadastroController : Controller
    {
        private readonly ICadastroMembroService _cadastroMembroService;

        public CadastroController(ICadastroMembroService cadastroMembroService)
        {
            _cadastroMembroService = cadastroMembroService;
        }

        [HttpGet]
        public IActionResult Index(string nomeCompleto = "", string cpf = "", string dataBatismo = "")
        {
            ViewData["Nome"] = HttpContext.Session.GetString("Nome");

            var lista = _cadastroMembroService.RecuperarListaMembros();

            DateTime? data = null;

            if (!string.IsNullOrEmpty(nomeCompleto))
            {
                lista = lista.Where(d => d.NomeCompleto.Contains(nomeCompleto)).ToList();
            }
            if (!string.IsNullOrEmpty(cpf))
            {
                lista = lista.Where(d => d.CPF.Contains(cpf)).ToList();
            }
            if (!string.IsNullOrEmpty(dataBatismo))
            {
                data = Convert.ToDateTime(dataBatismo);
                lista = lista.Where(d => d.DataBatismo.Date == data).ToList();
            }

            return View(lista);
        }

        [HttpGet]
        public IActionResult NovoCadastroMembro()
        {
            ViewData["Nome"] = HttpContext.Session.GetString("Nome");

            CadastroMembrosVO novo = new CadastroMembrosVO();

            novo.Cargo = _cadastroMembroService.RecuperarListaCargos();

            return View(novo);
        }

        [HttpPost]
        public IActionResult SalvarCadastro(CadastroMembro cadastroMembro)
        {
            try
            {
                _cadastroMembroService.SalvarCadastroMembro(cadastroMembro);
                return RedirectToAction("Index", "Cadastro");     
            }
            catch (ValidationException)
            {
                return RedirectToAction("SalvarCadastro", "Cadastro");
            }
        }

        [HttpPost]
        public IActionResult RemoverMembro(int Id)
        {
            try
            {
                var lista = _cadastroMembroService.RecuperarListaMembros();
                var registro = lista.FirstOrDefault(th => th.Id == Id);

                if (registro != null)
                {
                    CadastroMembrosVO vo = new CadastroMembrosVO()
                    {
                        Id = registro.Id,
                        CargoId = registro.CargoId,
                        CPF = registro.CPF,
                        DataBatismo = registro.DataBatismo,
                        DataEmissao = registro.DataEmissao,
                        DataNascimento = registro.DataNascimento,
                        NomeCompleto = registro.NomeCompleto,
                        NomeMae = registro.NomeMae,
                        NomePai = registro.NomePai,
                        RG = registro.RG
                    };
                    _cadastroMembroService.ExcluirCadastroMembro(vo);
                }
                return Json(true);
            }
            catch (ValidationException)
            {
                return RedirectToAction("SalvarCadastro", "Cadastro");
            }
        }

        [HttpGet]
        public IActionResult EditarCadastroMembro(int id)
        {
            try
            {
                var lista = _cadastroMembroService.RecuperarListaMembros();
                var registro = lista.FirstOrDefault(th => th.Id == id);

                CadastroMembrosVO vo = new CadastroMembrosVO()
                {
                    Id = registro.Id,
                    CargoId = registro.CargoId,
                    CPF = registro.CPF,
                    DataBatismo = registro.DataBatismo,
                    DataEmissao = registro.DataEmissao,
                    DataNascimento = registro.DataNascimento,
                    NomeCompleto = registro.NomeCompleto,
                    NomeMae = registro.NomeMae,
                    NomePai = registro.NomePai,
                    RG = registro.RG
                };
                vo.Cargo = _cadastroMembroService.RecuperarListaCargos();

                return View(vo);
            }
            catch (ValidationException)
            {
                throw;
            }
        }

    }
}
