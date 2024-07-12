using Microsoft.AspNetCore.Mvc;

namespace MeroBook.Areas.Authentication.Controllers
{
    [Area("Authentication")]
    public class AuthenticationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult SignUp()
        {
            return View(new MeroBook.Models.Authentication());
        }
        
    }
}
