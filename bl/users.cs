
using System.Data;
using System.Data.SqlClient;

namespace bl {
    public class users {

        public static DataTable login_user(string username, string password) {

            var dt = new DataTable();
            var sql = $@"select id, username, role from users
                        where users.username  = {bl.format.db_string_null(username)}
                        and users.password = {bl.format.db_string_null(password)}";

            var db = new db.conn().db;
            var ad = new SqlDataAdapter(sql.Trim(), db);
            ad.Fill(dt);
            ad.Dispose();

            db.Close();

            return dt ;

        }

        public static DataTable register_user(string username,string password, string email, string date) {

            using (var db = new db.conn().db) {

                var sql = $@"insert into users (username, password, email, date_created)
                              values ({bl.format.db_string_null(username)}
                                    , {bl.format.db_string_null(password)}
                                    , {bl.format.db_string_null(email)}
                                    , {bl.format.db_string_null(date)});";

                using (var cmd = new SqlCommand(sql, db)) {
                    var rows = cmd.ExecuteNonQuery();
                    db.Close();

                    if (rows == 1) {
                        return login_user(username,password);
                    } else {
                        return new DataTable();
                    }

                }
            }

        }

    }
}
