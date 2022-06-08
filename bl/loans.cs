
using System.Data;
using System.Data.SqlClient;

namespace bl {
    public class loans {

        public static DataTable get_user_loans(string id) {

            var dt = new DataTable();
            var sql = $@"select  books.id as book_id, books.authorId, books.title, books.year, books.isbn, authors.name, loans.id
                        from books
                        join authors on authors.id = books.authorId
                        join loans on loans.book_id = books.id
                        where loans.user_id = {bl.format.db_string_null(id)}
                        order by id asc";

            var db = new db.conn().db;
            var ad = new SqlDataAdapter(sql.Trim(), db);
            ad.Fill(dt);
            ad.Dispose();

            db.Close();

            return dt;

        }

        public static void loan_book(string book_id, string user_id) {

            using (var db = new db.conn().db) {

                var sql = $@"insert into loans (book_id, user_id)
                        values ({bl.format.db_string_null(book_id)}
                        , {bl.format.db_string_null(user_id)});";

                using (var cmd = new SqlCommand(sql, db)) {

                    cmd.ExecuteNonQuery();

                    db.Close();

                }
            }

        }

        public static void return_book(string id) {


            using (var db = new db.conn().db) {

                var sql = $@"delete from loans where
                        loans.id = {bl.format.db_string_null(id)};";

                using (var cmd = new SqlCommand(sql, db)) {
                    
                    cmd.ExecuteNonQuery();

                    db.Close();

                }
            }

        }

    }
}
