using E_Library.Data;
using Microsoft.AspNetCore.Mvc;

namespace E_Library.ControllersApi {

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthorsController : ControllerBase {

        private readonly LibraryContext _context;

        public AuthorsController(LibraryContext context) {
            _context = context;
        }


        public object list_all() {

            var authors = _context.Authors.OrderBy(x => x.Id).Take(100);//bl.authors.list_all();

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(authors);

            return json;
        }

        
        public object get(string id) {

            var author = _context.Authors.Where(x => x.Id == Int32.Parse(id)).FirstOrDefault();//bl.authors.get_author(id);

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(author);

            return json;
        }

    }


}