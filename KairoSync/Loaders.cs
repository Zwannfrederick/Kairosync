using System;
using System.Data;
using System.Windows.Forms;

namespace sql_project
{
    public static class Loaders
    {
        public static void ProjeleriGetir(DataGridView dataGridView)
        {
            try
            {
                string query = @"
                    SELECT 
                        ProjeID, Ad, BaşlangıçTarihi, BitişTarihi, Gecikme, Açıklama, TamamTarihi 
                    FROM projeler;
                ";

                dataGridView.DataSource = CRUD.list(query);

                // Tarih formatları
                SetDateColumnFormat(dataGridView, "BaşlangıçTarihi");
                SetDateColumnFormat(dataGridView, "BitişTarihi");
                SetDateColumnFormat(dataGridView, "TamamTarihi");

                // Düzenlenemez sütunlar
                SetReadOnlyColumn(dataGridView, "ProjeID");
                SetReadOnlyColumn(dataGridView, "Gecikme");
                dataGridView.Columns["TamamTarihi"].HeaderText = "Tamamlanma Tarihi";
                dataGridView.Columns["BaşlangıçTarihi"].HeaderText = "Başlangıç Tarihi";
                dataGridView.Columns["BitişTarihi"].HeaderText = "Bitiş Tarihi";
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
                string query = @"
                    SELECT 
                        ÇalışanID, AdSoyad, Email, TelNo, DoğumTarihi 
                    FROM çalışanlar;
                ";

                dataGridView.DataSource = CRUD.list(query);

                // Tarih formatları
                SetDateColumnFormat(dataGridView, "DoğumTarihi");

                // Düzenlenemez sütunlar
                SetReadOnlyColumn(dataGridView, "ÇalışanID");
                dataGridView.Columns["AdSoyad"].HeaderText = "Ad Soyad";
                dataGridView.Columns["TelNo"].HeaderText = "Telefon Numarası";
                dataGridView.Columns["DoğumTarihi"].HeaderText = "Doğum Tarihi";
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
                        g.ProjeID, g.ÇalışanID, g.Açıklama, g.TamamTarihi, g.Gecikme,
                        p.Ad AS ProjeAdi, c.AdSoyad AS CalisanAdi
                    FROM görevler g
                    LEFT JOIN projeler p ON g.ProjeID = p.ProjeID
                    LEFT JOIN çalışanlar c ON g.ÇalışanID = c.ÇalışanID;
                ";

                dataGridView.DataSource = CRUD.list(query);

                // Tarih formatları
                SetDateColumnFormat(dataGridView, "BaşlangıçTarihi");
                SetDateColumnFormat(dataGridView, "BitişTarihi");
                SetDateColumnFormat(dataGridView, "TamamTarihi");

                // Düzenlenemez sütunlar
                SetReadOnlyColumn(dataGridView, "GörevID");
                SetReadOnlyColumn(dataGridView, "Gecikme");
                SetReadOnlyColumn(dataGridView, "ProjeAdi");
                SetReadOnlyColumn(dataGridView, "CalisanAdi");
                dataGridView.Columns["TamamTarihi"].HeaderText = "Tamamlanma Tarihi";
                dataGridView.Columns["BaşlangıçTarihi"].HeaderText = "Başlangıç Tarihi";
                dataGridView.Columns["BitişTarihi"].HeaderText = "Bitiş Tarihi";
                dataGridView.Columns["AdamGün"].HeaderText = "Adam-Gün Değeri";

                // Durum sütunu için ComboBox
                SetComboBoxColumn(dataGridView, "Durum", new string[] { "Tamamlanacak", "Devam Ediyor", "Tamamlandı" });

                // Proje ve Çalışan ComboBox'ları ekle
                SetComboBoxColumnForLookup(dataGridView, "ProjeID", "ProjeID", "Ad", "projeler");
                SetComboBoxColumnForLookup(dataGridView, "ÇalışanID", "ÇalışanID", "AdSoyad", "çalışanlar");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        // Tarih formatlarını ayarlayan fonksiyon
        private static void SetDateColumnFormat(DataGridView dataGridView, string columnName)
        {
            if (dataGridView != null && dataGridView.Columns.Contains(columnName))
            {
                dataGridView.Columns[columnName].DefaultCellStyle.Format = "yyyy-MM-dd";
            }
        }

        // Sütunları yalnızca okunabilir yapmak için yardımcı fonksiyon
        private static void SetReadOnlyColumn(DataGridView dataGridView, string columnName)
        {
            if (dataGridView != null && dataGridView.Columns.Contains(columnName))
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
                    row.Cells[columnName].Value = null;
                }
            }
        }

        // ComboBox sütunu eklemek için yardımcı fonksiyon
        private static void SetComboBoxColumn(DataGridView dataGridView, string columnName, string[] values)
        {
            if (dataGridView.Columns.Contains(columnName))
            {
                DataGridViewComboBoxColumn comboBoxColumn = new DataGridViewComboBoxColumn
                {
                    DataSource = values,
                    HeaderText = columnName,
                    DataPropertyName = columnName,
                    Name = columnName
                };

                int columnIndex = dataGridView.Columns[columnName].Index;
                dataGridView.Columns.RemoveAt(columnIndex);
                dataGridView.Columns.Insert(columnIndex, comboBoxColumn);
            }
        }

        // Lookup için ComboBox sütunu eklemek
        private static void SetComboBoxColumnForLookup(DataGridView dataGridView, string columnName, string valueMember, string displayMember, string tableName)
        {
            var data = CRUD.list($"SELECT {valueMember}, {displayMember} FROM {tableName};");
            if (dataGridView.Columns.Contains(columnName))
            {
                DataGridViewComboBoxColumn comboBoxColumn = new DataGridViewComboBoxColumn
                {
                    DataSource = data,
                    DisplayMember = displayMember,
                    ValueMember = valueMember,
                    HeaderText = columnName,
                    DataPropertyName = valueMember,
                    Name = columnName
                };

                int columnIndex = dataGridView.Columns[columnName].Index;
                dataGridView.Columns.RemoveAt(columnIndex);
                dataGridView.Columns.Insert(columnIndex, comboBoxColumn);
            }
        }
    }
}
