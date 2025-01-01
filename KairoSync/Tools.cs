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

        public static void CalisanEkle(string? adSoyad, string? email, string? telNo, DateTime? dogumTarihi, Modder.ModDurumu mod)
        {
            // Parametrelerin geçerliliğini kontrol et
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


            string tableName = "çalışanlar";  // Tablo adı
            List<string> columnNames = new List<string> { "AdSoyad", "Email", "TelNo", "DoğumTarihi" };  // Sütun adları
            List<object> values = new List<object> { adSoyad, email, telNo, dogumTarihi };  // Değerler

            // Ekleme işlemi
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
            // Parametrelerin geçerliliğini kontrol et
            if (string.IsNullOrEmpty(ad))
            {
                MessageBox.Show("Lütfen proje adı giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;  // İşlem sonlandırılır
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

            string tableName = "projeler";  // Tablo adı
            List<string> columnNames = new List<string> { "Ad", "BaşlangıçTarihi", "BitişTarihi", "Açıklama" };  // Sütun adları
#pragma warning disable CS8604 // Olası null başvuru bağımsız değişkeni.
            List<object> values = new List<object> { ad, baslangicTarihi, bitisTarihi.HasValue ? (object)bitisTarihi.Value : DBNull.Value, aciklama };  // Değerler
#pragma warning restore CS8604 // Olası null başvuru bağımsız değişkeni.

            // Ekleme işlemi
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
            public string? Ad { get; set; }  // Nullable string

            public override string ToString()
            {
                return Ad ?? string.Empty; // Eğer Ad null ise boş string döndürülür
            }
        }


        public class Calisan
        {
            public int CalisanID { get; set; }
            public string? AdSoyad { get; set; }

            public override string ToString()
            {
                return AdSoyad ?? string.Empty; // ComboBox'ta görüntülenecek
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
                // DataGridView'deki verileri alalım
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if (row.IsNewRow) continue;  // Yeni satırsa atla

                    // Her satırdaki veriyi al ve güncelle
                    List<string> columnNames = new List<string>();  // Güncellenmek istenen sütunlar
                    List<object> values = new List<object>();  // Güncellenen değerler

                    // Satırdaki her hücreyi kontrol et
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell != null && cell.OwningColumn != null && cell.OwningColumn.Name != null)
                        {
                            columnNames.Add(cell.OwningColumn.Name);
                            values.Add(cell.Value ?? DBNull.Value);
                        }
                        else
                        {
                            // Eğer cell, OwningColumn veya Name null ise, bir hata mesajı veya başka bir işlem gerçekleştirin
                            MessageBox.Show("Hata: Hücre veya sütun adı null.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        // Sütun adı
                          // Değer, boşsa DBNull olarak ekle
                    }

                    // Veritabanını güncelle
                    string condition = $"id = {row.Cells["id"].Value}";  // Örnek: ID'yi kullanarak güncelle
                    int result = CRUD.guncelle(tableName, columnNames, values, condition, mod);

                    if (result <= 0)
                    {
                        MessageBox.Show("Veri güncellenirken bir hata oluştu.");
                    }
                }

                MessageBox.Show("Değişiklikler başarıyla kaydedildi.");
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
                if (control is TextBox textBox) // Eğer kontrol TextBox ise
                {
                    textBox.Clear(); // TextBox'ı temizle
                }
                else if (control is RichTextBox richTextBox) // Eğer kontrol RichTextBox ise
                {
                    richTextBox.Clear(); // RichTextBox'ı temizle
                }
                else if (control is DateTimePicker dateTimePicker) // Eğer kontrol DateTimePicker ise
                {
                    dateTimePicker.Value = DateTime.Now; // DateTimePicker'ı sıfırla
                }
                else if (control is ComboBox comboBox) // Eğer kontrol ComboBox ise
                {
                    if (comboBox.Items.Contains("+90"))
                    {
                        comboBox.SelectedValue = "+90";
                    }
                    else
                    {
                        comboBox.SelectedIndex = -1;
                    } // ComboBox'ı temizle
                }
                // Diğer kontrol türleri için benzer işlemler eklenebilir
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
                    // Düzenleme modundaysa DataGridView'i orijinal haline döndür
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
                    // Silme modundaysa seçili satırı sil
                    foreach (DataGridViewRow row in dataGridView.SelectedRows)
                    {
                        int id = Convert.ToInt32(row.Cells["id"].Value);
                        string condition = $"id = {id}";

                        int result = CRUD.sil(tableName, condition);

                        if (result > 0)
                        {
                            MessageBox.Show("Kayıt başarıyla silindi.");
                        }
                        else
                        {
                            MessageBox.Show("Kayıt silinirken bir hata oluştu.");
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
                // Parametrelerin geçerliliğini kontrol et
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
