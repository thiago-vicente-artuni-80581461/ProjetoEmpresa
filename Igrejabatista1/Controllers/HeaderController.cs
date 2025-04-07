using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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
            try
            {
                var login = HttpContext.Session.GetString("Nome");
                ViewBag.UsuarioLogin = login;
                return View();
            }
            catch (ValidationException)
            {

                throw;
            }
           
        }
    }
}
