//using Newtonsoft.Json;
//using System.Data;
//using System.Net;
//using System.Net.Http;
//using System.Web.Http;

using E_Library.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace E_Library.ControllersApi {

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BooksController : ControllerBase {


        public object list_all() {

            var books = bl.books.list_all();

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(books);

            return json;
        }


        public object get(string id) {

            var book = bl.books.get_book(id);

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(book);

            return json;
        }

        public object get_by_author(string id) {

            var books = bl.books.get_books_by_author(id);

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(books);

            return json;
        }

        
        public object save() {

            var book = new StreamReader(Request.Body).ReadToEnd();

            bl.books.save_book(book);

            return Ok();
        }

        public object add() {

            var book = new StreamReader(Request.Body).ReadToEnd();

            bl.books.add_book(book);

            return Ok();
        }

        public object delete(string id) {

            var status = bl.books.delete_book(id);

            if (status == true) {
                return Ok();
            } else {
                return BadRequest();
            }

            
        }




    }
}

