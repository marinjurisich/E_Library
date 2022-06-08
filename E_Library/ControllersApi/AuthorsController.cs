using Microsoft.AspNetCore.Mvc;

namespace E_Library.ControllersApi {

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthorsController : ControllerBase {


        public object list_all() {

            var authors = bl.authors.list_all();

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(authors);

            return json;
        }

        
        public object get(string id) {

            var author = bl.authors.get_author(id);

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(author);

            return json;
        }

    }


}