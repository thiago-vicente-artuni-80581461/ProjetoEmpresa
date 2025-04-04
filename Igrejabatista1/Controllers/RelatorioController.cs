using IgrejaBatista1.Models.Services;
using Microsoft.AspNetCore.Mvc;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace IgrejaBatista1.Controllers
{
    public class RelatorioController : Controller
    {
        private readonly ICaixaService _caixaService;
        public RelatorioController(ICaixaService caixaService)
        {
            _caixaService = caixaService;
        }
        [HttpGet]
        public IActionResult Index(int? mes = null, int? ano = null)
        {
            ViewData["Nome"] = HttpContext.Session.GetString("Nome");

            return View();
        }

        [HttpGet]
        public IActionResult GerarRelatorioCaixa(int? mes = null, int? ano = null) {
            int departamentoTipoId = Convert.ToInt32(HttpContext.Session.GetString("DepartamentoTipoId"));
            var lista = _caixaService.RecuperarListaCaixaRelatorio(departamentoTipoId, mes, ano).ToList();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Relatório de Caixa");

                worksheet.Cell(1, 1).Value = "Tipo do Departamento";
                worksheet.Cell(1, 2).Value = "Total de Dízimos/Ofertas";
                worksheet.Cell(1, 3).Value = "Total de Contas Pagas";
                worksheet.Cell(1, 4).Value = "Total Caixa";

                for (int i = 0; i < lista.Count; i++)
                {
                    worksheet.Cell(i + 2, 1).Value = lista[i].DepartamentoTipoDescricao;
                    worksheet.Cell(i + 2, 2).Value = lista[i].ValorReceita;
                    worksheet.Cell(i + 2, 3).Value = lista[i].ValorContas;
                    worksheet.Cell(i + 2, 4).Value = lista[i].ValorTotal;
                }

                worksheet.Columns().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var fileBytes = stream.ToArray();

                    return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "RelatorioCaixa.xlsx");
                }
            }
        } 
    }
}
