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
                // Veritabanı sorgusu: ProjeID nullable olabiliyor
                dataGridView.DataSource = CRUD.list("SELECT ProjeID, Ad, BaşlangıçTarihi, BitişTarihi, Gecikme, Açıklama FROM projeler;");

                // Tarih formatları ayarlanıyor
                SetDateColumnFormat(dataGridView, "BaşlangıçTarihi");
                SetDateColumnFormat(dataGridView, "BitişTarihi");

                // ProjeID yalnızca okunabilir
                SetReadOnlyColumn(dataGridView, "ProjeID");

                // Nullable int kontrolü yapıyoruz
                SetNullableColumnValues(dataGridView, "ProjeID");
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
                // Çalışanları getir: ÇalışanID nullable olabiliyor
                dataGridView.DataSource = CRUD.list("SELECT ÇalışanID, AdSoyad, Email, TelNo, DoğumTarihi FROM çalışanlar;");

                // Tarih formatı ayarlanıyor
                SetDateColumnFormat(dataGridView, "DoğumTarihi");

                // ÇalışanID yalnızca okunabilir
                SetReadOnlyColumn(dataGridView, "ÇalışanID");

                // Nullable int kontrolü yapıyoruz
                SetNullableColumnValues(dataGridView, "ÇalışanID");
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

                // Tarih formatları
                SetDateColumnFormat(dataGridView, "BaşlangıçTarihi");
                SetDateColumnFormat(dataGridView, "BitişTarihi");

                // GörevID yalnızca okunabilir
                SetReadOnlyColumn(dataGridView, "GörevID");

                // Nullable int kontrolü yapıyoruz
                SetNullableColumnValues(dataGridView, "GörevID");
                SetNullableColumnValues(dataGridView, "ProjeID");
                SetNullableColumnValues(dataGridView, "ÇalışanID");

                // Durum sütunu için ComboBox
                SetComboBoxColumn(dataGridView, "Durum", new string[] { "Tamamlanacak", "Devam Ediyor", "Tamamlandı" });

                // ProjeAdi ve CalisanAdi sadece görüntüleme amaçlı, bunları düzenlenemez yapalım
                SetReadOnlyColumn(dataGridView, "ProjeAdi");
                SetReadOnlyColumn(dataGridView, "CalisanAdi");

                // Proje ve Çalışan ComboBox'ları ekle
                SetComboBoxColumnForLookup(dataGridView, "Proje", "ProjeID", "Ad", "projeler");
                SetComboBoxColumnForLookup(dataGridView, "Çalışan", "ÇalışanID", "AdSoyad", "çalışanlar");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        // Tarih formatlarını ayarlayan fonksiyon
        private static void SetDateColumnFormat(DataGridView dataGridView, string columnName)
        {
            if (dataGridView.Columns.Contains(columnName))
            {
                dataGridView.Columns[columnName].DefaultCellStyle.Format = "yyyy-MM-dd";
            }
        }

        // Sütunları yalnızca okunabilir yapmak için yardımcı fonksiyon
        private static void SetReadOnlyColumn(DataGridView dataGridView, string columnName)
        {
            if (dataGridView.Columns.Contains(columnName))
            {
                dataGridView.Columns[columnName].ReadOnly = true;
            }
        }

        // Nullable int kontrolü ve null değer atama fonksiyonu
        private static void SetNullableColumnValues(DataGridView dataGridView, string columnName)
        {
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Cells[columnName].Value == DBNull.Value)
                {
                    row.Cells[columnName].Value = null;  // Null değeri göstermek
                }
            }
        }

        // ComboBox sütunu eklemek için yardımcı fonksiyon
        private static void SetComboBoxColumn(DataGridView dataGridView, string columnName, string[] values)
        {
            if (dataGridView.Columns.Contains(columnName))
            {
                int columnIndex = dataGridView.Columns[columnName].Index;
                DataGridViewComboBoxColumn comboBoxColumn = new DataGridViewComboBoxColumn
                {
                    DataSource = values,
                    HeaderText = columnName,
                    DataPropertyName = columnName,
                    Name = columnName
                };
                dataGridView.Columns.RemoveAt(columnIndex);
                dataGridView.Columns.Insert(columnIndex, comboBoxColumn);
            }
        }

        // Projeler ve Çalışanlar için ComboBox eklemek
        private static void SetComboBoxColumnForLookup(DataGridView dataGridView, string columnName, string valueMember, string displayMember, string tableName)
        {
            var data = CRUD.list($"SELECT {valueMember}, {displayMember} FROM {tableName};");
            if (dataGridView.Columns.Contains(columnName))
            {
                int columnIndex = dataGridView.Columns[columnName].Index;
                DataGridViewComboBoxColumn comboBoxColumn = new DataGridViewComboBoxColumn
                {
                    DataSource = data,
                    DisplayMember = displayMember,
                    ValueMember = valueMember,
                    HeaderText = columnName,
                    DataPropertyName = valueMember,
                    Name = columnName
                };
                dataGridView.Columns.RemoveAt(columnIndex);
                dataGridView.Columns.Insert(columnIndex, comboBoxColumn);
            }
        }
    }
}
