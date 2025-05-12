using IgrejaBatista1.Models.ValueObjects;
using IgrejaBatista1.Models;
using Microsoft.AspNetCore.Mvc;
using IgrejaBatista1.Models.Services;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Net.NetworkInformation;
using NuGet.Packaging.Signing;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace IgrejaBatista1.Controllers
{
    [Authorize]
    public class PatrimonioController : Controller
    {

        public readonly IPatrimonioService _patrimonioService = null;
        public readonly IEntradaService _entradaService = null;
        private readonly string _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

        public PatrimonioController(IPatrimonioService patrimonioService, IEntradaService entradaService)
        {
            _patrimonioService = patrimonioService;
            _entradaService = entradaService;
        }

        [HttpGet]
        public IActionResult Index(string codigo = "", string nome = "")
        {
            try
            {
                ViewData["Nome"] = User.Identity.Name;

                if (ViewData["Nome"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }

                int departamentoTipoId = int.Parse(User.FindFirst("DepartamentoTipoId")?.Value);

                IEnumerable<CadastroPatrimonioVO> patrimonio = _patrimonioService.RecuperarListaPatrimonio(departamentoTipoId, codigo, nome).ToList();

                return View(patrimonio.ToList());
            }
            catch (ValidationException)
            {

                throw;
            }

        }

        [HttpGet]

        public IActionResult CadastroPatrimonio([FromRoute] int Id, string mensagem = "")
        {
            try
            {
                CadastroPatrimonioVO patrimonio = new CadastroPatrimonioVO();

                ViewData["Nome"] = User.Identity.Name;

                if (ViewData["Nome"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }

                int perfilId = int.Parse(User.FindFirst("Perfil")?.Value);
                int departamentoTipoId = int.Parse(User.FindFirst("DepartamentoTipoId")?.Value);

                ViewBag.Mensagem = mensagem;

                patrimonio.DepartamentoTipo = _entradaService.RecuperarDadosDepartamentoTipo(User.Identity.Name, departamentoTipoId);

                if (Id != 0)
                {
                    var recuperarInformacoes = _patrimonioService.RecuperarListaPatrimonio(departamentoTipoId, "", "").FirstOrDefault(th => th.Id == Id);

                    patrimonio.Id = recuperarInformacoes.Id;
                    patrimonio.DepartamentoTipoId = recuperarInformacoes.DepartamentoTipoId;
                    patrimonio.Codigo = recuperarInformacoes.Codigo;
                    patrimonio.Nome = recuperarInformacoes.Nome;
                    patrimonio.Descricao = recuperarInformacoes.Descricao;
                    patrimonio.Setor = recuperarInformacoes.Setor;
                    patrimonio.DataBaixa = recuperarInformacoes.DataBaixa;
                    patrimonio.Foto = recuperarInformacoes.Foto;
                    patrimonio.DataCriacao = recuperarInformacoes.DataCriacao;
                }
                return View(patrimonio);
            }
            catch (ValidationException)
            {
                throw;
            }

        }

        [HttpPost]
        public async Task<IActionResult> SalvarPatrimonio(CadastroPatrimonioVO patrimonio)
        {
            try
            {
                ViewData["Nome"] = User.Identity.Name;

                if (ViewData["Nome"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }

                if (patrimonio.Imagem != null && patrimonio.Imagem.Length > 0)
                {
                    var extensao = Path.GetExtension(patrimonio.Imagem.FileName);

                    var fileGuid = $"{Guid.NewGuid()}{extensao}";

                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileGuid);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await patrimonio.Imagem.CopyToAsync(stream);

                        extensao = Path.GetExtension(patrimonio.Imagem.FileName);

                        if (!new[] { ".jpeg", ".png", ".jpg", ".gif", ".bmp", ".tiff", ".psd", ".exif", ".raw" }.Contains(extensao))
                        {
                            return RedirectToAction("CadastroPatrimonio", "Patrimonio", new { mensagem = "Formato de imagem inválido!!" });
                        }
                        patrimonio.TamanhoFoto = patrimonio.Imagem.Length;

                        patrimonio.Foto = fileGuid;
                    }

                    _patrimonioService.SalvarPatrimonio(patrimonio);

                    return RedirectToAction("Index", "Patrimonio");
                }

                else
                {
                    patrimonio.Foto = "Sem Foto";
                   
                }
                _patrimonioService.SalvarPatrimonio(patrimonio);
                return RedirectToAction("Index", "Patrimonio");
            }
            catch (ValidationException)
            {

                throw;
            }
        }

        public async Task<IActionResult> Imagem(string imagemGuid)
        {
            try
            {
                var fileName = imagemGuid;
                var filePath = Path.Combine(_uploadPath, fileName);

                var imageBytes = await System.IO.File.ReadAllBytesAsync(filePath);

                return File(imageBytes, "image/" + Path.GetExtension(imagemGuid));
            }
            catch (ValidationException)
            {

                throw;
            }
        }

        [HttpPost]
        public IActionResult RemoverPatrimonio(int Id)
        {
            try
            {
                ViewData["Nome"] = User.Identity.Name;

                if (ViewData["Nome"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                int departamentoTipoId = int.Parse(User.FindFirst("DepartamentoTipoId")?.Value);

                var lista = _patrimonioService.RecuperarListaPatrimonio(departamentoTipoId, "", "");
                var registro = lista.FirstOrDefault(th => th.Id == Id);

                if (registro != null)
                {
                    CadastroPatrimonioVO vo = new CadastroPatrimonioVO()
                    {
                        Id = registro.Id,
                        Codigo = registro.Codigo,
                        DataBaixa = registro.DataBaixa,
                        DataCriacao = registro.DataCriacao,
                        DepartamentoTipoId = registro.DepartamentoTipoId,
                        Descricao = registro.Descricao,
                        Foto = registro.Foto,
                        Nome = registro.Nome,
                        Setor = registro.Setor,
                        TamanhoFoto = registro.TamanhoFoto
                        
                    };
                    _patrimonioService.ExcluirCadastroPatrimonio(vo);
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
