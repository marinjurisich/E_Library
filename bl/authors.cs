using System.Data;
using System.Data.SqlClient;

namespace bl {
    public class authors {

        public static DataTable list_all() {

            var dt = new DataTable();
            var sql = "select top 100 * from Authors";

            var db = new db.conn().db;
            var ad = new SqlDataAdapter(sql.Trim(), db);
            ad.Fill(dt);
            ad.Dispose();

            db.Close();

            return dt;

        }

        public static DataTable get_author(string id) {

            var dt = new DataTable();
            var sql = $@"select * from authors where authors.id = {id}";

            var db = new db.conn().db;
            var ad = new SqlDataAdapter(sql.Trim(), db);
            ad.Fill(dt);
            ad.Dispose();

            db.Close();

            return dt;

        }



    }
}
