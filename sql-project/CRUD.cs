using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace sql_project
{
    public class CRUD
    {
        static DataTable dt;
        public static DataTable list(string sql)
        {
            Connect.conn.Open();
            dt = new DataTable();
            SQLiteDataAdapter adtr = new SQLiteDataAdapter(sql, Connect.conn);
            adtr.Fill(dt);
            Connect.conn.Close();
            return dt;
        }
    }
}