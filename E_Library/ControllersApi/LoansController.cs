using E_Library.Data;
using Microsoft.AspNetCore.Mvc;

namespace E_Library.ControllersApi {

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoansController : ControllerBase {

        private readonly LibraryContext _context;

        public LoansController(LibraryContext context) {
            _context = context;
        }

        public object get(string id) {

            //var loans = _context.Loans.Where(l => l.User_id == int.Parse(id).Join);//bl.loans.get_user_loans(id);

            var result = (from l in _context.Loans
                          join b in _context.Books on l.Book_id
                          equals b.Id
                          join a in _context.Authors on b.AuthorId
                          equals a.Id
                          where l.User_id == int.Parse(id)
                          select new  { Id = l.Id, Book_id = l.Book_id, Title = b.Title, AuthorId = a.Id, Name = a.Name, Year = b.Year, Isbn = b.Isbn }).ToList();

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(result);

            if(json == "[]") {
                return BadRequest();
            } else {
                return Ok(json);
            }

        }

        public void delete(string id) {

            bl.loans.return_book(id);

        }

        public void loan(string book_id, string user_id) {
            bl.loans.loan_book(book_id, user_id);
        }



    }


}