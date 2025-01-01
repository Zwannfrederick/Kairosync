    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SQLite;
    using System.Linq;
    using System.Windows.Forms;

    namespace sql_project
    {
        public class CRUD
        {
            static DataTable dt = new DataTable();

            public static DataTable list(string sql)
            {
                dt = new DataTable();
                try
                {
                    Connect.conn.Open();
                    SQLiteDataAdapter adtr = new SQLiteDataAdapter(sql, Connect.conn);
                    adtr.Fill(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Listeleme sırasında bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (Connect.conn.State == ConnectionState.Open)
                        Connect.conn.Close();
                }
                return dt;
            }

            public static int ekle(string tableName, List<string> columnNames, List<object> values)
            {
                if (columnNames.Count != values.Count)
                    throw new ArgumentException("Sütun sayısı ile değer sayısı eşleşmiyor.");

                string columns = string.Join(", ", columnNames);
                string parameters = string.Join(", ", columnNames.ConvertAll(name => "@" + name));

                string sql = $"INSERT INTO {tableName} ({columns}) VALUES ({parameters})";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, Connect.conn))
                {
                    try
                    {
                        Connect.conn.Open();
                        for (int i = 0; i < columnNames.Count; i++)
                        {
                            cmd.Parameters.AddWithValue("@" + columnNames[i], values[i] ?? DBNull.Value);
                        }
                        return cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ekleme sırasında bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return 0;
                    }
                    finally
                    {
                        Connect.conn.Close();
                    }
                }
            }

            public static int guncelle(string tableName, List<string> columnNames, List<object> values, string condition)
            {
                try
                {
                    Connect.conn.Open();
                    string setClause = string.Join(", ", columnNames.Select((col, idx) => $"{col} = @{col}"));

                    string query = $"UPDATE {tableName} SET {setClause} WHERE {condition}";

                    Console.WriteLine($"SQL Query: {query}");
                    Console.WriteLine($"Values: {string.Join(", ", values)}");

                    using (SQLiteCommand cmd = new SQLiteCommand(query, Connect.conn))
                    {
                        for (int i = 0; i < columnNames.Count; i++)
                        {
                            cmd.Parameters.AddWithValue($"@{columnNames[i]}", values[i]);
                            Console.WriteLine($"Param: @{columnNames[i]} = {values[i]}");
                        }

                        return cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Güncelleme sırasında bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0;
                }
                finally
                {
                    if (Connect.conn.State == ConnectionState.Open)
                        Connect.conn.Close();
                }
            }


            public static int sil(string table, string condition)
            {
                try
                {
                    Connect.conn.Open();
                    string query = $"DELETE FROM {table} WHERE {condition}";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, Connect.conn))
                    {
                        return cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Silme sırasında bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0;
                }
                finally
                {
                    Connect.conn.Close();
                }
            }

            public static object Sorgula(string sql, params SQLiteParameter[] parameters)
            {
                try
                {
                    Connect.conn.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, Connect.conn))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        return cmd.ExecuteScalar();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Sorgulama sırasında bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
                finally
                {
                    if (Connect.conn.State == ConnectionState.Open)
                        Connect.conn.Close();
                }
            }

            public static DataTable listWithCondition(string table, string condition)
            {
                try
                {
                    string query = $"SELECT * FROM {table} WHERE {condition}";
                    return list(query);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Koşullu listeleme sırasında bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return new DataTable();
                }
            }
        public static int yürüt(string sql)
        {
            try
            {
                Connect.conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, Connect.conn))
                {
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"SQL sorgusu çalıştırılırken bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            finally
            {
                if (Connect.conn.State == ConnectionState.Open)
                    Connect.conn.Close();
            }
        }


    }
}
