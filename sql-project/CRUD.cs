﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace sql_project
{
    public class CRUD
    {
        static DataTable dt;

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
                Console.WriteLine("Listeleme sırasında bir hata oluştu: " + ex.Message);
            }
            finally
            {
                if (Connect.conn.State == ConnectionState.Open)
                {
                    Connect.conn.Close();
                }
            }
            return dt;
        }

        public static int sil(string table, string condition)
        {
            int result = 0;
            try
            {

                Connect.conn.Open();
                string query = $"DELETE FROM {table} WHERE {condition}";
                using (SQLiteCommand cmd = new SQLiteCommand(query, Connect.conn))
                {
                    result = cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Silme işlemi sırasında bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {   
                Connect.conn.Close();
            }
            return result;
        }

        public static int ekle(string tableName, List<string> columnNames, List<object> values)
        {
            if (columnNames.Count != values.Count)
            {
                throw new ArgumentException("Sütun sayısı ile değer sayısı eşleşmiyor.");
            }

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
                    Console.WriteLine("Hata: " + ex.Message);
                    return 0;
                }
                finally
                {
                    Connect.conn.Close();
                }
            }
        }

    }
}
