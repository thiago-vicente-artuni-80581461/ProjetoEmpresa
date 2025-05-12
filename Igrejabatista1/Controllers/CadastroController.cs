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
    public class CadastroController : Controller
    {
        private readonly ICadastroMembroService _cadastroMembroService;
        private readonly IloginService _loginService;
        private readonly IEntradaService _entradaService;

        public CadastroController(ICadastroMembroService cadastroMembroService, IloginService loginService, IEntradaService entradaService  )
        {
            _cadastroMembroService = cadastroMembroService;
            _loginService = loginService;
            _entradaService = entradaService;
        }

        [HttpGet]
        public IActionResult Index(string nomeCompleto = "", string cpf = "", string dataBatismo = "")
        {
            try
            {
                ViewData["Nome"] = User.Identity.Name;

                if (ViewData["Nome"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }

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
            catch (ValidationException)
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult NovoCadastroMembro()
        {
            try
            {
                ViewData["Nome"] = User.Identity.Name;

                if (ViewData["Nome"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }

                CadastroMembrosVO novo = new CadastroMembrosVO();

                novo.Cargo = _cadastroMembroService.RecuperarListaCargos();

                return View(novo);
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult SalvarCadastro(CadastroMembrosVO cadastroMembro)
        {
            try
            {
                ViewData["Nome"] = User.Identity.Name;

                if (ViewData["Nome"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }

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
                ViewData["Nome"] = User.Identity.Name;

                if (ViewData["Nome"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }

                var lista = _cadastroMembroService.RecuperarListaMembros();
                var registro = lista.FirstOrDefault(th => th.Id == Id);

                int departamentoTipoId = int.Parse(User.FindFirst("DepartamentoTipoId")?.Value);
                int perfilId = int.Parse(User.FindFirst("Perfil")?.Value);

                var verificarEntrada = _entradaService.VerificarEntradaMembro(Id);

                if (verificarEntrada != null)
                {
                    return Json(new { sucesso = false, mensagem = "Erro ao excluir o registro, pois, está relacionado com outros registros do sistema!!!" });
                }

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
                        RG = registro.RG,
                        Ativo = registro.Ativo
                    };
                    _cadastroMembroService.ExcluirCadastroMembro(vo);
                }
                return Json(new { sucesso = true});
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
                ViewData["Nome"] = User.Identity.Name;

                if (ViewData["Nome"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                var registro = _cadastroMembroService.RecuperarListaMembros().FirstOrDefault(th => th.Id == id);

                var recuperarListaCargo = _cadastroMembroService.RecuperarCargosCadastroMembro(id);

                CadastroMembrosVO vo = new CadastroMembrosVO()
                {
                    Id = registro.Id,
                    CargoId = recuperarListaCargo,
                    CPF = registro.CPF,
                    DataBatismo = registro.DataBatismo,
                    DataEmissao = registro.DataEmissao,
                    DataNascimento = registro.DataNascimento,
                    NomeCompleto = registro.NomeCompleto,
                    NomeMae = registro.NomeMae,
                    NomePai = registro.NomePai,
                    RG = registro.RG,
                    Ativo = registro.Ativo
                };

                vo.Cargo = _cadastroMembroService.RecuperarListaCargos();

                return View(vo);
            }
            catch (ValidationException)
            {
                throw;
            }
        }


        [HttpGet]
        public IActionResult IndexUsuario(string nome = "")
        {
            try
            {
                ViewData["Nome"] = User.Identity.Name;

                if (ViewData["Nome"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }

                ViewBag.DepartamentoTipoId = int.Parse(User.FindFirst("DepartamentoTipoId")?.Value);

                var lista = _loginService.RecuperarUsuariosLogin(nome, User.Identity.Name, ViewBag.DepartamentoTipoId);
                return View(lista);
            }
            catch (ValidationException)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult CadastroUsuario()
        {
            try
            {
                ViewData["Nome"] = User.Identity.Name;

                if (ViewData["Nome"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }

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
                ViewData["Nome"] = User.Identity.Name;

                if (ViewData["Nome"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }

                _cadastroMembroService.SalvarCadastroUsuario(login);
                return RedirectToAction("IndexUsuario", "Cadastro");
            }
            catch (ValidationException)
            {
                return RedirectToAction("SalvarUsuario", "Cadastro");
            }
        }

    }
}
