using Microsoft.AspNetCore.Mvc;

namespace IgrejaBatista1.Controllers
{
   
    public class HeaderController : Controller
    {
        public HeaderController()
        {
        }

        [HttpPost]
        public IActionResult Header()
        {
            var login = HttpContext.Session.GetString("Nome");
            ViewBag.UsuarioLogin = login;
            return View();
        }
    }
}
