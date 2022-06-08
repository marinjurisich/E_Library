using Microsoft.AspNetCore.Mvc;

namespace E_Library.ControllersApi {

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoansController : ControllerBase {


        public object get(string id) {

            var loans = bl.loans.get_user_loans(id);

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(loans);

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