using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
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
        public static bool IsValidEmail(string email)
        {
            // E-posta formatı için regex deseni
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            // Regex ile e-posta kontrolü
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }
        public static void CalisanEkle(string? adSoyad, string? email, string? telNo, DateTime? dogumTarihi, Modder.ModDurumu mod)
        {
            
            if (string.IsNullOrEmpty(adSoyad))
            {
                MessageBox.Show("Lütfen çalışan adı ve soyadı giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Lütfen geçerli bir e-posta adresi giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(telNo))
            {
                MessageBox.Show("Lütfen telefon numarası giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dogumTarihi == default(DateTime) || !dogumTarihi.HasValue)
            {
                MessageBox.Show("Lütfen doğum tarihi giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            string tableName = "çalışanlar";  
            List<string> columnNames = new List<string> { "AdSoyad", "Email", "TelNo", "DoğumTarihi" };  
            List<object> values = new List<object> { adSoyad, email, telNo, dogumTarihi };  

            
            int result = CRUD.ekle(tableName, columnNames, values, mod);

            if (result > 0)
            {
                MessageBox.Show("Çalışan başarıyla eklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Çalışan eklenirken bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        public static void ProjeEkle(string? ad, DateTime? baslangicTarihi, DateTime? bitisTarihi, string? aciklama, Modder mod, DataGridView dataGridView)
        {
            
            if (string.IsNullOrEmpty(ad))
            {
                MessageBox.Show("Lütfen proje adı giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;  
            }
            if (baslangicTarihi == default(DateTime) || !baslangicTarihi.HasValue)
            {
                MessageBox.Show("Lütfen başlangıç tarihi giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(aciklama))
            {
                MessageBox.Show("Lütfen açıklama giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string tableName = "projeler";  
            List<string> columnNames = new List<string> { "Ad", "BaşlangıçTarihi", "BitişTarihi", "Açıklama" };  
#pragma warning disable CS8604 // Olası null başvuru bağımsız değişkeni.
            List<object> values = new List<object> { ad, baslangicTarihi, bitisTarihi.HasValue ? (object)bitisTarihi.Value : DBNull.Value, aciklama };  // Değerler
#pragma warning restore CS8604 // Olası null başvuru bağımsız değişkeni.

            
            int result = CRUD.ekle(tableName, columnNames, values, mod.aktifMod);

            if (result > 0)
            {
                MessageBox.Show("Proje başarıyla eklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mod.ModDegistir(Modder.ModDurumu.None, dataGridView);
            }
            else
            {
                MessageBox.Show("Proje eklenirken bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mod.ModDegistir(Modder.ModDurumu.None, dataGridView);
            }
        }


        public class Proje
        {
            public int ProjeID { get; set; }
            public string? Ad { get; set; }  

            public override string ToString()
            {
                return Ad ?? string.Empty; 
            }
        }


        public class Calisan
        {
            public int CalisanID { get; set; }
            public string? AdSoyad { get; set; }

            public override string ToString()
            {
                return AdSoyad ?? string.Empty; 
            }
        }

        public static void Projecombobox(ComboBox comboBox)
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
                        Ad = row["Ad"] == DBNull.Value ? string.Empty : row["Ad"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Projeler yüklenirken bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public static void Çalışancombobox(ComboBox comboBox)
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


        public static void Kaydet(DataGridView dataGridView, string tableName, Modder.ModDurumu mod)
        {
            try
            {
                
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if (row.IsNewRow) continue;

                    List<string> columnNames = new List<string>();
                    List<object> values = new List<object>();

                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell != null && cell.OwningColumn != null && cell.OwningColumn.Name != null)
                        {
                            if (cell.OwningColumn.Name != "ProjeAdi" && cell.OwningColumn.Name != "CalisanAdi")
                            {
                                columnNames.Add(cell.OwningColumn.Name);
                                values.Add(cell.Value ?? DBNull.Value);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Hata: Hücre veya sütun adı null.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    string condition = tableName switch
                    {
                        "görevler" => $"GörevID = {row.Cells["GörevID"].Value}",
                        "projeler" => $"ProjeID = {row.Cells["ProjeID"].Value}",
                        "çalışanlar" => $"ÇalışanID = {row.Cells["ÇalışanID"].Value}",
                        _ => throw new InvalidOperationException("Geçersiz tablo adı.")
                    };

                    int result = CRUD.guncelle(tableName, columnNames, values, condition, mod);

                    if (result <= 0)
                    {
                        MessageBox.Show($"Veri {tableName} tablosunda güncellenirken bir hata oluştu.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }







        public static void Temizle(List<Control> controls)
        {
            foreach (var control in controls)
            {
                if (control is TextBox textBox) 
                {
                    textBox.Clear(); 
                }
                else if (control is RichTextBox richTextBox) 
                {
                    richTextBox.Clear(); 
                }
                else if (control is DateTimePicker dateTimePicker) 
                {
                    dateTimePicker.Value = DateTime.Now; 
                }
                else if (control is ComboBox comboBox) 
                {
                    if (comboBox.Items.Contains("+90"))
                    {
                        comboBox.SelectedValue = "+90";
                    }
                    else
                    {
                        comboBox.SelectedIndex = -1;
                    } 
                }
                
            }
        }


        public static void Sil(DataGridView dataGridView, string tableName, Modder.ModDurumu modDurumu, List<Control> controls)
        {
            try
            {
                
                if (modDurumu == Modder.ModDurumu.Ekle)
                {
                    Temizle(controls);
                }
                else if (modDurumu == Modder.ModDurumu.Duzenle)
                {
                    
                    if (tableName.Equals("görevler", StringComparison.OrdinalIgnoreCase))
                    {
                        Loaders.GorevleriGetir(dataGridView);
                    }
                    else if (tableName.Equals("projeler", StringComparison.OrdinalIgnoreCase))
                    {
                        Loaders.ProjeleriGetir(dataGridView);
                    }
                    else if (tableName.Equals("çalışanlar", StringComparison.OrdinalIgnoreCase))
                    {
                        Loaders.CalisanlariGetir(dataGridView);
                    }
                }
                else
                {
                    
                    foreach (DataGridViewRow row in dataGridView.SelectedRows)
                    {
                        
                        string firstColumnName = dataGridView.Columns[0].Name;
                        var firstCellValue = row.Cells[firstColumnName].Value;

                        
                        string condition = firstCellValue != DBNull.Value
                            ? $"{firstColumnName} = {Convert.ToInt32(firstCellValue)}"
                            : $"{firstColumnName} IS NULL";

                        
                        int result = CRUD.sil(tableName, condition);
                        if (result <= 0)
                        {
                            MessageBox.Show("Veri silinirken bir hata oluştu.");
                        }
                        else
                        {
                            MessageBox.Show("Veri başarıyla silindi.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }








        public static void GorevEkle(string? ad, DateTime? baslangicTarihi, DateTime? bitisTarihi, string? adamGun, int projeID, int calisanID, string? aciklama, Modder.ModDurumu mod)
        {
            try
            {
                
                if (string.IsNullOrEmpty(ad))
                {
                    MessageBox.Show("Lütfen görev adı giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!baslangicTarihi.HasValue || baslangicTarihi == default(DateTime))
                {
                    MessageBox.Show("Lütfen başlangıç tarihi giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (string.IsNullOrEmpty(aciklama))
                {
                    MessageBox.Show("Lütfen açıklama giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (string.IsNullOrEmpty(adamGun))
                {
                    MessageBox.Show("Lütfen görevin adam-gün değerini giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!double.TryParse(adamGun, out double adamGunValue))
                {
                    MessageBox.Show("Lütfen geçerli bir Adam Gün değeri girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (projeID <= 0)
                {
                    MessageBox.Show("Lütfen geçerli bir proje seçiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (string.IsNullOrEmpty(aciklama))
                {
                    MessageBox.Show("Lütfen açıklama giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string tableName = "görevler";
                List<string> columnNames = new List<string>
                {
                    "Ad", "BaşlangıçTarihi", "BitişTarihi", "AdamGün", "Durum", "ProjeID", "ÇalışanID", "Açıklama"
                };

                List<object> values = new List<object>
                {
                    ad, baslangicTarihi, bitisTarihi.HasValue ? (object)bitisTarihi.Value : DBNull.Value, adamGunValue, "Tamamlanacak", projeID, calisanID, aciklama
                };

                int result = CRUD.ekle(tableName, columnNames, values, mod);

                if (result > 0)
                {
                    MessageBox.Show("Görev başarıyla eklendi. Tebrikler, yeni göreviniz başarıyla kaydedildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Görev eklenirken bir hata oluştu. Lütfen tekrar deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }
    }
}
