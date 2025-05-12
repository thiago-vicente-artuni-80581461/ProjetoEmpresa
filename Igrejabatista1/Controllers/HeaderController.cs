using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace IgrejaBatista1.Controllers
{
   [Authorize]
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
                ViewBag.UsuarioLogin = User.Identity.Name;
                return View();
            }
            catch (ValidationException)
            {

                throw;
            }
           
        }
    }
}
