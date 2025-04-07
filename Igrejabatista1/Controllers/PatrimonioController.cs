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

namespace IgrejaBatista1.Controllers
{
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
        public async Task<IActionResult> Index(string codigo = "", string nome = "")
        {
            try
            {
                ViewData["Nome"] = HttpContext.Session.GetString("Nome");
                int departamentoTipoId = Convert.ToInt32(HttpContext.Session.GetString("DepartamentoTipoId"));

                IEnumerable<CadastroPatrimonioVO> patrimonio = null;
                patrimonio = _patrimonioService.RecuperarListaPatrimonio(departamentoTipoId, codigo, nome).ToList();

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
                ViewData["Nome"] = HttpContext.Session.GetString("Nome");
                int perfilId = Convert.ToInt32(HttpContext.Session.GetString("Perfil"));

                ViewBag.Mensagem = mensagem;

                int departamentoTipoId = Convert.ToInt32(HttpContext.Session.GetString("DepartamentoTipoId"));

                patrimonio.DepartamentoTipo = _entradaService.RecuperarDadosDepartamentoTipo(perfilId, departamentoTipoId);

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

                return RedirectToAction("CadastroPatrimonio", "Patrimonio", new { patrimonio.Id, mensagem = "A foto não foi carregada!!" });
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
    }
}
