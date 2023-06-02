using Microsoft.AspNetCore.Mvc;

namespace AdminWebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Login", "Account");
        }
    }
}