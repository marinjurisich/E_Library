using E_Library.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace E_Library.ControllersApi {

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase {


        public object login(string username, string password) {


            var user = bl.users.login_user(username, password);

            //GraphQL auth
            var row = user.Rows[0];
            var role = row.Field<string>("role");
            int id_string = row.Field<int>("id");
            int id = id_string;
            var usrname = row.Field<string>("username");
            User u = new User(id, usrname, role);

           var token = GraphQL.Query.GetJWTAuthKey(u);

            user.Columns.Add("token", typeof(string));

            user.Rows[0]["token"] = token;

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(user);


            if(json == "[]") {
                return BadRequest();
            } else {
                return Ok(json);
            }

        }

        public object register(string username, string password, string email, string date) {

            var user = bl.users.register_user(username, password, email, date);

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(user);

            if (json == "[]") {
                return BadRequest();
            } else {
                return Ok(json);
            }

        }



    }


}