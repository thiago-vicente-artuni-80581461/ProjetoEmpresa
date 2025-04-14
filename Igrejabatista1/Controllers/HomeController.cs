using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IgrejaBatista1.Models;
using IgrejaBatista1.Models.Services;
using IgrejaBatista1.Models.ValueObjects;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.ComponentModel.DataAnnotations;

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
        try
        {
            ViewData["Nome"] = HttpContext.Session.GetString("Nome");

            if (ViewData["Nome"] == null)
            {
                return RedirectToAction("Login", "Login");
            }

            return View("Index", ViewData["Nome"]);
        }
        catch (ValidationException)
        {
            throw;
        }
      
    }
}
