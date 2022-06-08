using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bl {
    internal class format {

        private static string string_replace(string s) {
            if (string.IsNullOrWhiteSpace(s) == true) {
                return "";
            } else {
                s = s.Replace("#&", "");
                s = s.Replace("�", "");
                s = MySql.Data.MySqlClient.MySqlHelper.EscapeString(s);
                return s.Trim();
            }

        }

        public static string db_string_null(string value) {
            if (string.IsNullOrWhiteSpace(value) == true) { return "null"; }
            value = string_replace(value);
            var s = "'" + value + "'";
            return s;
        }

        public static int db_num_null(string value) {
       
            value = string_replace(value);
            var num = Int32.Parse(value);
            return num;
       
        }


    }
}
