using System;
using System.Data;
using System.Data.SQLite;
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
                SetReadOnlyColumn(dataGridView, "ProjeID");;
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

                // Tarih formatlarını ayarla
                SetDateColumnFormat(dataGridView, "DoğumTarihi");

                // Sadece okuma yapılacak kolonları ayarla
                SetReadOnlyColumn(dataGridView, "ÇalışanID");

                // Kolon başlıklarını düzenle
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
                if (dataGridView.Columns.Contains("ProjeAdi")) 
                { 
                    dataGridView.Columns["ProjeAdi"].Visible = false; 
                }
                if (dataGridView.Columns.Contains("CalisanAdi")) 
                { 
                    dataGridView.Columns["CalisanAdi"].Visible = false; 
                }
                dataGridView.Columns["ProjeID"].HeaderText = "Proje Adı";
                dataGridView.Columns["ÇalışanID"].HeaderText = "Çalışan Adı";
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        public static void CalisanGorevleriniGetir(DataGridView dataGridViewCalisanlar, DataGridView dataGridViewGorevler)
        {
            try
            {
                if (dataGridViewCalisanlar.SelectedRows.Count > 0)
                {
                    int selectedCalisanID = Convert.ToInt32(dataGridViewCalisanlar.SelectedRows[0].Cells["ÇalışanID"].Value);

                    string query = $@"
                        SELECT 
                            p.Ad AS ProjeAdi, g.Ad AS GörevAdi, 
                            g.BaşlangıçTarihi, g.BitişTarihi, g.TamamTarihi, g.Durum
                        FROM görevler g
                        INNER JOIN projeler p ON g.ProjeID = p.ProjeID
                        WHERE g.ÇalışanID = {selectedCalisanID};
                    ";

                    dataGridViewGorevler.DataSource = CRUD.list(query);

                    
                    SetDateColumnFormat(dataGridViewGorevler, "BaşlangıçTarihi");
                    SetDateColumnFormat(dataGridViewGorevler, "BitişTarihi");
                    SetDateColumnFormat(dataGridViewGorevler, "TamamTarihi");

                    
                    SetReadOnlyColumn(dataGridViewGorevler, "ProjeAdi");
                    SetReadOnlyColumn(dataGridViewGorevler, "GörevAdi");

                    dataGridViewGorevler.Columns["ProjeAdi"].HeaderText = "Proje Adı";
                    dataGridViewGorevler.Columns["GörevAdi"].HeaderText = "Görev Adı";
                    dataGridViewGorevler.Columns["BaşlangıçTarihi"].HeaderText = "Başlangıç Tarihi";
                    dataGridViewGorevler.Columns["BitişTarihi"].HeaderText = "Bitiş Tarihi";
                    dataGridViewGorevler.Columns["TamamTarihi"].HeaderText = "Tamamlanma Tarihi";
                    dataGridViewGorevler.Columns["Durum"].HeaderText = "Görev Durumu";  
                }
                else
                {
                    MessageBox.Show("Lütfen bir çalışan seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }




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

            // Boş veya NULL değeri içeren satırı ekle
            DataRow nullRow = data.NewRow();
            nullRow[valueMember] = DBNull.Value;
            nullRow[displayMember] = "Yok";
            data.Rows.InsertAt(nullRow, 0);

            if (dataGridView.Columns.Contains(columnName))
            {
                DataGridViewComboBoxColumn comboBoxColumn = new DataGridViewComboBoxColumn
                {
                    DataSource = data,
                    DisplayMember = displayMember,
                    ValueMember = valueMember,
                    HeaderText = columnName,
                    DataPropertyName = columnName,
                    Name = columnName,
                    ValueType = typeof(int?)
                };

                int columnIndex = dataGridView.Columns[columnName].Index;
                dataGridView.Columns.RemoveAt(columnIndex);
                dataGridView.Columns.Insert(columnIndex, comboBoxColumn);

                // Mevcut satırları kontrol et ve eğer geçerli değilse NULL yap
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if (row.Cells[columnName].Value != null && row.Cells[columnName].Value != DBNull.Value)
                    {
                        bool found = false;
                        foreach (DataRow dataRow in data.Rows)
                        {
                            if (dataRow[valueMember].Equals(row.Cells[columnName].Value))
                            {
                                found = true;
                                break;
                            }
                        }
                        if (!found)
                        {
                            row.Cells[columnName].Value = DBNull.Value; // Geçerli olmayan değerleri NULL yap
                        }
                    }
                }
            }
        }



    }
}
