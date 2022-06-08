  using bl.db;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json;
using Newtonsoft.Json;


namespace bl
    {
    public class books {

        public static DataTable list_all() {            

            var dt = new DataTable();
            var sql = $@"select top 1000 books.id, books.authorId, books.title, books.year, books.isbn, authors.name
                        from books
                        left join authors on authors.id = books.authorId";
            var db = new db.conn().db;
            var ad = new SqlDataAdapter(sql.Trim(), db);
            ad.Fill(dt);
            ad.Dispose();

            db.Close();

            return dt;

        }

        public static DataTable get_book(string id) {

            var dt = new DataTable();
            var sql = $@"select books.id, books.isbn, books.year, books.title, books.number_of_copies,
                        books.price, books.item_call_number, books.publisher_code, books.edition_statement, authors.name from Books
                        join authors on authors.id = books.authorId
                        where books.id = {id}"; 

            var db = new db.conn().db;
            var ad = new SqlDataAdapter(sql.Trim(), db);
            ad.Fill(dt);
            ad.Dispose();

            db.Close();

            return dt;

        }

        public static DataTable get_books_by_author(string id) {

            var dt = new DataTable();
            var sql = $@"select books.id, books.title, books.year, books.authorId, books.isbn, authors.name
                        from books join authors on authors.id = books.authorId
                        where books.authorId = {id}";

            var db = new db.conn().db;
            var ad = new SqlDataAdapter(sql.Trim(), db);
            ad.Fill(dt);
            ad.Dispose();

            db.Close();

            return dt;

        }

        public static DataTable get_book_by_author_name(string author_name) {

            var dt = new DataTable();
            var sql = $@"select * from books where books.author like '%{author_name}%'";

            var db = new db.conn().db;
            var ad = new SqlDataAdapter(sql.Trim(), db);
            ad.Fill(dt);
            ad.Dispose();

            db.Close();

            return dt;

        }


        public static bool delete_book(string id) {

            using (var db = new db.conn().db) {
                //db.Open();
                var sql = $@"delete from books where books.id = {bl.format.db_string_null(id)}";

                using (var cmd = new SqlCommand(sql, db)) {                    
                    var rows = cmd.ExecuteNonQuery();
                    db.Close();

                    if (rows != 1) {
                        return false;
                    } else {
                        return true;
                    }

                }
            }

        }

        public static void save_book(string json) {

            Newtonsoft.Json.Linq.JObject b = (Newtonsoft.Json.Linq.JObject) JsonConvert.DeserializeObject(json);

            var isbn = (string) b["isbn"];
            var year = (string) b["year"];
            var price = (string) b["price"];
            var edition_statement = (string) b["edition_statement"];
            var item_call_number = (string) b["item_call_number"];
            var publisher_code = (string) b["publisher_code"];
            var number_of_copies = (string) b["number_of_copies"];
            var id = (string) b["id"];

            using (var db = new db.conn().db) {
                //db.Open();
                var sql = $@"UPDATE books
                            SET isbn = {bl.format.db_string_null(isbn)}
                            ,year = {bl.format.db_string_null(year)}
                            ,price = {bl.format.db_string_null(price)}
                            ,edition_statement = {bl.format.db_string_null(edition_statement)}
                            ,item_call_number = {bl.format.db_string_null(item_call_number)}
                            ,publisher_code = {bl.format.db_string_null(publisher_code)}
                            ,number_of_copies = {bl.format.db_string_null(number_of_copies)}
                            WHERE books.id = {bl.format.db_string_null(id)};";

                using (var cmd = new SqlCommand(sql, db)) {

                    var status = cmd.ExecuteNonQuery();
                }
                db.Close();
            }

        }

        public static void add_book(string json) {
            Newtonsoft.Json.Linq.JObject b = (Newtonsoft.Json.Linq.JObject) JsonConvert.DeserializeObject(json);

            var isbn = (string) b["isbn"];
            var year = (string) b["year"];
            var authorId = (string) b["authorId"];
            var title = (string) b["title"];
            var price = (string) b["price"];
            var edition_statement = (string) b["edition_statement"];
            var item_call_number = (string) b["item_call_number"];
            var publisher_code = (string) b["publisher_code"];
            var number_of_copies = (string) b["number_of_copies"];
            var id = (string) b["id"];

            using (var db = new db.conn().db) {
                var sql = $@"INSERT INTO Books(
                        Isbn, Year
                        , Title, Price
                        , Edition_statement, Item_call_number
                        , Publisher_code, Number_of_copies
                        , AuthorId)
                    VALUES(
                        @Isbn, @Year
                        , @Title, @Price
                        , @Edition_statement, @Item_call_number
                        , @Publisher_code, @Number_of_copies
                        , @AuthorId)";

                using (var cmd = new SqlCommand(sql, db)) {
                    cmd.Parameters.AddWithValue("@Isbn", isbn);
                    cmd.Parameters.AddWithValue("@Year", year);
                    cmd.Parameters.AddWithValue("@Title", title);
                    cmd.Parameters.AddWithValue("@Price", price);
                    cmd.Parameters.AddWithValue("@Edition_statement", edition_statement);
                    cmd.Parameters.AddWithValue("@Item_call_number",item_call_number);
                    cmd.Parameters.AddWithValue("@Publisher_code", publisher_code);
                    cmd.Parameters.AddWithValue("@Number_of_copies", number_of_copies);
                    cmd.Parameters.AddWithValue("@AuthorId", authorId);

                    cmd.ExecuteNonQuery();
                }
                db.Close();
            }
        }

       

    }
}
