using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace sql_project
{
    public class Controller
    {
        private static int GecikmeHesapla(DateTime bitisTarihi)
        {
            try
            {
                DateTime simdikiTarih = DateTime.Now.Date;
                int hesaplananGecikme = (int)(simdikiTarih - bitisTarihi).TotalDays;

                return hesaplananGecikme > 0 ? hesaplananGecikme : 0;
            }
            catch
            {
                return 0;
            }
        }


        public static void GecikmeIlkle(DataGridViewRow row, DateTimePicker dateTimePicker)
        {
            try
            {
                object bitisTarihiValue = row.Cells["BitişTarihi"]?.Value ?? DBNull.Value;
                object projeIdValue = row.Cells["ProjeID"]?.Value ?? DBNull.Value;

                if (bitisTarihiValue != null &&
                    DateTime.TryParse(bitisTarihiValue.ToString(), out DateTime bitisTarihi) &&
                    projeIdValue != null)
                {
                    int gecikme = GecikmeHesapla(bitisTarihi);

                    string condition = $"ProjeID = {projeIdValue}";
                    List<string> columnNames = new List<string> { "Gecikme", "BitişTarihi" };
                    List<object> values = new List<object> { gecikme, DateTime.Now.Date };

                    int rowsAffected = CRUD.guncelle("projeler", columnNames, values, condition);

                    if (rowsAffected > 0)
                    {
                        row.Cells["Gecikme"].Value = gecikme;
                        row.Cells["BitişTarihi"].Value = DateTime.Now.Date;
                        dateTimePicker.Value = DateTime.Now.Date; // DateTimePicker güncelleniyor
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gecikme ilkleme sırasında bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void GecikmeGuncelle(DataGridViewRow row, DateTimePicker dateTimePicker)
        {
            try
            {
                object bitisTarihiValue = row.Cells["BitişTarihi"]?.Value ?? DBNull.Value;
                object projeIdValue = row.Cells["ProjeID"]?.Value ?? DBNull.Value;

                if (bitisTarihiValue != null &&
                    DateTime.TryParse(bitisTarihiValue.ToString(), out DateTime bitisTarihi) &&
                    projeIdValue != null)
                {
                    string query = $"SELECT Gecikme FROM projeler WHERE ProjeID = {projeIdValue}";
                    object mevcutGecikmeObj = CRUD.Sorgula(query);
                    int mevcutGecikme = mevcutGecikmeObj != null && int.TryParse(mevcutGecikmeObj.ToString(), out int gecikme) ? gecikme : 0;

                    int yeniGecikme = mevcutGecikme + GecikmeHesapla(bitisTarihi);

                    string condition = $"ProjeID = {projeIdValue}";
                    List<string> columnNames = new List<string> { "Gecikme", "BitişTarihi" };
                    List<object> values = new List<object> { yeniGecikme, DateTime.Now.Date };

                    int rowsAffected = CRUD.guncelle("projeler", columnNames, values, condition);

                    if (rowsAffected > 0)
                    {
                        row.Cells["Gecikme"].Value = yeniGecikme;
                        row.Cells["BitişTarihi"].Value = DateTime.Now.Date;
                        dateTimePicker.Value = DateTime.Now.Date; // DateTimePicker güncelleniyor
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gecikme güncelleme sırasında bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void GecikmeIslemleri(DataGridView dataGridView, Label label1, Label label2, DateTimePicker dateTimePicker)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    object gecikmeValue = row.Cells["Gecikme"]?.Value ?? DBNull.Value;

                    if (gecikmeValue == null || !int.TryParse(gecikmeValue.ToString(), out int mevcutGecikme) || mevcutGecikme == 0)
                    {
                        GecikmeIlkle(row, dateTimePicker);
                    }
                    else
                    {
                        GecikmeGuncelle(row, dateTimePicker);
                    }
                }

                LabelAdjuster(label1, label2,dataGridView);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gecikme işlemleri sırasında bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }





        public static void LabelAdjuster(Label label1, Label label2, DataGridView dataGridView)
        {
            try
            {
                if (dataGridView.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dataGridView.SelectedRows[0];
                    object gecikmeValue = selectedRow.Cells["Gecikme"]?.Value ?? DBNull.Value;

                    if (gecikmeValue != null && int.TryParse(gecikmeValue.ToString(), out int gecikme) && gecikme > 0)
                    {
                        label2.Text = gecikme.ToString();
                        label1.Visible = true;
                        label2.Visible = true;
                    }
                    else
                    {
                        label2.Text = "0";
                        label1.Visible = false;
                        label2.Visible = false;
                    }
                }
                else
                {
                    label2.Text = "0";
                    label1.Visible = false;
                    label2.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Label ayarlanırken bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                label1.Visible = false;
                label2.Visible = false;
            }
        }
    }
}
