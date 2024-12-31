using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace sql_project
{
    public class Connect
    {
        public static string ConnectionString = "Data Source=.//Kairosync.db;Version=3";
        public static SQLiteConnection conn = new SQLiteConnection(ConnectionString);
    }
}
