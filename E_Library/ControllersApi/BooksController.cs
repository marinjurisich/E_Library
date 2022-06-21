using E_Library.Data;
using E_Library.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;

namespace E_Library.ControllersApi {

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BooksController : ControllerBase {

        private readonly LibraryContext _context;

        public BooksController(LibraryContext context) {
            _context = context;
        }

        public object list_all() {

            var books = _context.Books.Join(_context.Authors,
                              b => b.AuthorId,
                              a => a.Id,
                              (b, a) => new { Id = b.Id, Isbn = b.Isbn, Title = b.Title, Year = b.Year, AuthorId = b.AuthorId,   Author_name = a.Name })
                          .OrderBy(x => x.Id)
                          .Take(1000).ToList();// bl.books.list_all();

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(books);

            Console.WriteLine(json);

            return json;
        }


        public object get(string id) {

            var book = _context.Books.Where(x => x.Id == int.Parse(id));//bl.books.get_book(id);

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(book);

            return json;
        }

        public object get_by_author(string id) {

            var books = _context.Books.Where(x => x.AuthorId == int.Parse(id));// bl.books.get_books_by_author(id);

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(books);

            return json;
        }

        
        public async Task<object> saveAsync() {

            

            var input = new StreamReader(Request.Body).ReadToEnd();

            Newtonsoft.Json.Linq.JObject i = (Newtonsoft.Json.Linq.JObject) JsonConvert.DeserializeObject(input);

            

            var isbn = (string) i["isbn"];
            var year = (string) i["year"];
            var price = (string) i["price"];
            var edition_statement = (string) i["edition_statement"];
            var item_call_number = (string) i["item_call_number"];
            var publisher_code = (string) i["publisher_code"];
            var number_of_copies = (string) i["number_of_copies"];
            var id = (int) i["id"];

            var book = _context.Books.Single(b => b.Id == id);

            book.Item_call_number = item_call_number;
            book.Number_of_copies = number_of_copies;
            book.Price = price;
            book.Publisher_code = publisher_code;
            book.Edition_statement = edition_statement;

            await _context.SaveChangesAsync();
            //bl.books.save_book(book);

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

