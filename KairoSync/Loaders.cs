using System;
using System.Windows.Forms;

namespace sql_project
{
    public static class Loaders
    {
        public static void ProjeleriGetir(DataGridView dataGridView)
        {
            try
            {
                dataGridView.DataSource = CRUD.list("SELECT ProjeID, Ad, BaşlangıçTarihi, BitişTarihi, Gecikme, Açıklama FROM projeler;");

                // Tarih formatı ayarlanıyor
                dataGridView.Columns["BaşlangıçTarihi"].DefaultCellStyle.Format = "yyyy-MM-dd";
                dataGridView.Columns["BitişTarihi"].DefaultCellStyle.Format = "yyyy-MM-dd";

                // ProjeID yalnızca okunabilir
                dataGridView.Columns["ProjeID"].ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        public static void CalisanlariGetir(DataGridView dataGridView)
        {
            try
            {
                dataGridView.DataSource = CRUD.list("SELECT ÇalışanID, AdSoyad, Email, TelNo, DoğumTarihi FROM çalışanlar;");

                // Tarih formatı ayarlanıyor
                dataGridView.Columns["DoğumTarihi"].DefaultCellStyle.Format = "yyyy-MM-dd";

                // ÇalışanID yalnızca okunabilir
                dataGridView.Columns["ÇalışanID"].ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        public static void GorevleriGetir(DataGridView dataGridView)
        {
            try
            {
                // Proje ve Çalışan isimlerini de dahil eden sorguyu kullan
                string query = @"
                    SELECT 
                        g.GörevID, g.Ad, g.BaşlangıçTarihi, g.BitişTarihi, g.AdamGün, g.Durum, 
                        p.Ad AS ProjeAdi, c.AdSoyad AS CalisanAdi, g.Açıklama
                    FROM görevler g
                    LEFT JOIN projeler p ON g.ProjeID = p.ProjeID
                    LEFT JOIN çalışanlar c ON g.ÇalışanID = c.ÇalışanID;
                ";

                // Görevler tablosunu getir
                dataGridView.DataSource = CRUD.list(query);

                // Tarih formatı ayarla
                dataGridView.Columns["BaşlangıçTarihi"].DefaultCellStyle.Format = "yyyy-MM-dd";
                dataGridView.Columns["BitişTarihi"].DefaultCellStyle.Format = "yyyy-MM-dd";

                // GörevID yalnızca okunabilir
                dataGridView.Columns["GörevID"].ReadOnly = true;

                // Durum sütunu için ComboBox
                DataGridViewComboBoxColumn durumColumn = new DataGridViewComboBoxColumn
                {
                    DataSource = new string[] { "Tamamlanacak", "Devam Ediyor", "Tamamlandı" },
                    HeaderText = "Durum",
                    DataPropertyName = "Durum",
                    ValueType = typeof(string),
                    Name = "Durum"
                };
                int durumColumnIndex = dataGridView.Columns["Durum"].Index;
                dataGridView.Columns.RemoveAt(durumColumnIndex);
                dataGridView.Columns.Insert(durumColumnIndex, durumColumn);

                // ProjeAdi ve CalisanAdi sadece görüntüleme amaçlı, bunları düzenlenemez yapalım
                dataGridView.Columns["ProjeAdi"].ReadOnly = true;
                dataGridView.Columns["CalisanAdi"].ReadOnly = true;

                // Projeler tablosunu getir ve ProjeID sütununu bir ComboBox olarak ekle
                var projeler = CRUD.list("SELECT ProjeID, Ad FROM projeler;");
                DataGridViewComboBoxColumn projeColumn = new DataGridViewComboBoxColumn
                {
                    DataSource = projeler,
                    DisplayMember = "Ad",
                    ValueMember = "ProjeID",
                    HeaderText = "Proje",
                    DataPropertyName = "ProjeID",
                    Name = "Proje"
                };
                int projeColumnIndex = dataGridView.Columns["ProjeID"].Index;
                dataGridView.Columns.RemoveAt(projeColumnIndex);
                dataGridView.Columns.Insert(projeColumnIndex, projeColumn);

                // Çalışanlar tablosunu getir ve ÇalışanID sütununu bir ComboBox olarak ekle
                var calisanlar = CRUD.list("SELECT ÇalışanID, AdSoyad FROM çalışanlar;");
                DataGridViewComboBoxColumn calisanColumn = new DataGridViewComboBoxColumn
                {
                    DataSource = calisanlar,
                    DisplayMember = "AdSoyad",
                    ValueMember = "ÇalışanID",
                    HeaderText = "Çalışan",
                    DataPropertyName = "ÇalışanID",
                    Name = "Çalışan"
                };
                int calisanColumnIndex = dataGridView.Columns["ÇalışanID"].Index;
                dataGridView.Columns.RemoveAt(calisanColumnIndex);
                dataGridView.Columns.Insert(calisanColumnIndex, calisanColumn);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

    }
}
