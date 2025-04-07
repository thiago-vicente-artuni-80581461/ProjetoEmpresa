using IgrejaBatista1.Models.Services;
using Microsoft.AspNetCore.Mvc;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel.DataAnnotations;
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
            try
            {
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
                        worksheet.Cell(i + 2, 2).Value = lista[i].ValorReceita.ToString("C");
                        worksheet.Cell(i + 2, 3).Value = lista[i].ValorContas.HasValue ? Convert.ToDecimal(lista[i].ValorContas).ToString("C") : 0;
                        worksheet.Cell(i + 2, 4).Value = lista[i].ValorTotal.HasValue ? Convert.ToDecimal(lista[i].ValorTotal).ToString("C") : 0; ;
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
            catch (ValidationException)
            {

                throw;
            }
           
        }

        [HttpGet]
        public IActionResult IndexEntrada(int? mes = null, int? ano = null)
        {
            ViewData["Nome"] = HttpContext.Session.GetString("Nome");

            return View();
        }

        [HttpGet]
        public IActionResult GerarRelatorioEntrada(int? mes = null, int? ano = null)
        {
            try
            {
                int departamentoTipoId = Convert.ToInt32(HttpContext.Session.GetString("DepartamentoTipoId"));
                var lista = _caixaService.RecuperarListaEntradaRelatorio(departamentoTipoId, mes, ano).ToList();

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Relatório de Entrada");

                    worksheet.Cell(1, 1).Value = "Tipo do Departamento";
                    worksheet.Cell(1, 2).Value = "Tipo de Contribuição";
                    worksheet.Cell(1, 3).Value = "Total de Dízimos/Ofertas";


                    for (int i = 0; i < lista.Count; i++)
                    {
                        worksheet.Cell(i + 2, 1).Value = lista[i].DepartamentoTipoDescricao;
                        worksheet.Cell(i + 2, 2).Value = lista[i].Tipo;
                        worksheet.Cell(i + 2, 3).Value = lista[i].ValorReceita.ToString("C");
                    }

                    worksheet.Columns().AdjustToContents();

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var fileBytes = stream.ToArray();

                        return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "RelatorioEntrada.xlsx");
                    }
                }
            }
            catch (ValidationException)
            {

                throw;
            }
            
        }

        [HttpGet]
        public IActionResult IndexSaidaRelatorio(int? mes = null, int? ano = null)
        {
            ViewData["Nome"] = HttpContext.Session.GetString("Nome");

            return View();
        }

        [HttpGet]
        public IActionResult GerarRelatorioSaida(int? mes = null, int? ano = null)
        {
            try
            {
                int departamentoTipoId = Convert.ToInt32(HttpContext.Session.GetString("DepartamentoTipoId"));
                var lista = _caixaService.RecuperarListaSaidaRelatorio(departamentoTipoId, mes, ano).ToList();

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Relatório de Saída");

                    worksheet.Cell(1, 1).Value = "Tipo do Departamento";
                    worksheet.Cell(1, 2).Value = "Tipo de Conta";
                    worksheet.Cell(1, 3).Value = "Descrição";
                    worksheet.Cell(1, 4).Value = "Data Saída";
                    worksheet.Cell(1, 5).Value = "Valor Pago";


                    for (int i = 0; i < lista.Count; i++)
                    {
                        worksheet.Cell(i + 2, 1).Value = lista[i].DepartamentoTipoDescricao;
                        worksheet.Cell(i + 2, 2).Value = lista[i].TipoConta;
                        worksheet.Cell(i + 2, 3).Value = lista[i].Descricao;
                        worksheet.Cell(i + 2, 4).Value = lista[i].DataSaida;
                        worksheet.Cell(i + 2, 5).Value = lista[i].ValorPago.ToString("C");
                    }

                    worksheet.Columns().AdjustToContents();

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var fileBytes = stream.ToArray();

                        return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "RelatorioSaida.xlsx");
                    }
                }
            }
            catch (ValidationException)
            {
                throw;
            }
           
        }
    }
}
