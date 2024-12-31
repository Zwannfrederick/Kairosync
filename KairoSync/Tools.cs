using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace sql_project
{
    public class Tools
    {
        public static void Arama(string searching, string table, string searched, DataGridView dataGridView)
        {
            try
            {
                string query = $"SELECT * FROM {table} WHERE {searched} LIKE '%{searching}%'";
                dataGridView.DataSource = CRUD.list(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        public static void CalisanEkle(string adSoyad, string email, string telNo, DateTime dogumTarihi)
        {
            string tableName = "çalışanlar";  // Tablo adı
            List<string> columnNames = new List<string> { "AdSoyad", "Email", "TelNo", "DoğumTarihi" };  // Sütun adları
            List<object> values = new List<object> { adSoyad, email, telNo, dogumTarihi };  // Değerler

            // Ekleme işlemi
            int result = CRUD.ekle(tableName, columnNames, values);

            if (result > 0)
            {
                MessageBox.Show("Çalışan başarıyla eklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Çalışan eklenirken bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void ProjeEkle(string ad, DateTime baslangicTarihi, DateTime? bitisTarihi, int? gecikme, string aciklama)
        {
            string tableName = "projeler";  // Tablo adı
            List<string> columnNames = new List<string> { "Ad", "BaşlangıçTarihi", "BitişTarihi", "Gecikme", "Açıklama" };  // Sütun adları
            List<object> values = new List<object> { ad, baslangicTarihi, bitisTarihi, gecikme, aciklama };  // Değerler

            // Ekleme işlemi
            int result = CRUD.ekle(tableName, columnNames, values);

            if (result > 0)
            {
                MessageBox.Show("Proje başarıyla eklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Proje eklenirken bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public class Proje
        {
            public int ProjeID { get; set; }
            public string Ad { get; set; }

            public override string ToString()
            {
                return Ad; // ComboBox'ta görüntülenecek
            }
        }

        public class Calisan
        {
            public int CalisanID { get; set; }
            public string AdSoyad { get; set; }

            public override string ToString()
            {
                return AdSoyad; // ComboBox'ta görüntülenecek
            }
        }

        private void Projecombobox(ComboBox comboBox)
        {
            try
            {
                string query = "SELECT ProjeID, Ad FROM projeler";
                DataTable dt = CRUD.list(query);

                comboBox.Items.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    comboBox.Items.Add(new Proje
                    {
                        ProjeID = Convert.ToInt32(row["ProjeID"]),
                        Ad = row["Ad"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Projeler yüklenirken bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Çalışancombobox(ComboBox comboBox)
        {
            try
            {
                string query = "SELECT ÇalışanID, AdSoyad FROM çalışanlar";
                DataTable dt = CRUD.list(query);

                comboBox.Items.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    comboBox.Items.Add(new Calisan
                    {
                        CalisanID = Convert.ToInt32(row["ÇalışanID"]),
                        AdSoyad = row["AdSoyad"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Çalışanlar yüklenirken bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public static void GorevEkle(string ad, DateTime baslangicTarihi, DateTime? bitisTarihi, double adamGun, string durum, int projeID, int? calisanID, string aciklama)
        {
            try
            {
                string tableName = "görevler";
                List<string> columnNames = new List<string>
                {
                    "Ad", "BaşlangıçTarihi", "BitişTarihi", "AdamGün", "Durum", "ProjeID", "ÇalışanID", "Açıklama"
                };

                List<object> values = new List<object>
                {
                    ad, baslangicTarihi, bitisTarihi, adamGun, durum, projeID, calisanID, aciklama
                };

                int result = CRUD.ekle(tableName, columnNames, values);

                if (result > 0)
                {
                    MessageBox.Show("Görev başarıyla eklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Görev eklenirken bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }




    }
}
