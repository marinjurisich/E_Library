

using System.Data.SqlClient;

namespace bl.db {
    public class conn {

        private SqlConnection _db;
        ////username:root; pass: admin
        //private static string connection_string = "Server=myServerAddress;Database=myDataBase;Trusted_Connection=True;";//"Server=localhost;Port=3306;Database=library;Uid=marin;Pwd=bipI1A;";

        //SqlConnection conn = new SqlConnection();
        //conn.ConnectionString =
        //      "Data Source=ServerName;" +
        //      "Initial Catalog=DataBaseName;" +
        //      "User id=UserName;" +
        //      "Password=Secret;";
        //    conn.Open();

        public conn() {
            
            _db = new SqlConnection("Server=DESKTOP-SH6HTAN;Database=library;Trusted_Connection=True;MultipleActiveResultSets=True;");

        }

        public SqlConnection db {
            get {
                try {
                    if (_db.State == System.Data.ConnectionState.Closed) {
                        _db.Open();
                    }
                    return _db;
                } catch (Exception ex) {
                    throw new Exception(ex.Message);
                }
            }
        }

    }
}
