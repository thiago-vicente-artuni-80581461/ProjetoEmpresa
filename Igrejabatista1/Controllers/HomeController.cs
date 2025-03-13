using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IgrejaBatista1.Models;
using IgrejaBatista1.Models.Services;
using IgrejaBatista1.Models.ValueObjects;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace IgrejaBatista1.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IloginService _loginService;

    public HomeController(ILogger<HomeController> logger, IloginService loginService)
    {
        _logger = logger;
        _loginService = loginService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        ViewData["Nome"] = HttpContext.Session.GetString("Nome");
   
        return View("Index", ViewData["Nome"]);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
