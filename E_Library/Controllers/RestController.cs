
using Microsoft.AspNetCore.Mvc;


namespace E_Library.Controllers {
    public class RestController : Controller {

        public IActionResult AllBooks() {
            return View();
        }

        public IActionResult Book(string id) {
            return View();
        }

        public IActionResult AuthorBooks(string id) {
            return View();
        }

        public IActionResult AddBook(string id) {
            return View();
        }

        

    }
}