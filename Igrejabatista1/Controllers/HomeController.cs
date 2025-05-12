using IgrejaBatista1.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace IgrejaBatista1.Controllers;

[Authorize]
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
           
            ViewData["Nome"] = User.Identity.Name;

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
