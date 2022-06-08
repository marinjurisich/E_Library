
using Microsoft.AspNetCore.Mvc;


namespace E_Library.Controllers {
    public class HomeController : Controller {

        public IActionResult Index() {
            return View();
        }

        public IActionResult Login() {
            return View();
        }

        public IActionResult Signup() {
            return View();
        }

        public new IActionResult User() {
            return View();
        }

    }
}