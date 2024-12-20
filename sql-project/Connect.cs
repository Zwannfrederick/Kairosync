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
        public static SQLiteConnection conn = new SQLiteConnection("Data Source=.//Kairosync.db;Version=3");
    }
}
