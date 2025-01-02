using System;
using System.Data;
using System.Data.SQLite;
using System.Timers;
using System.Windows.Forms;

namespace sql_project
{
    public class Controller
    {
        private static System.Timers.Timer? _timer;
        private static DataGridView? _dataGridView;
        private static string? _tableName;

        public static void BaslatGecikmeGuncelleme(DataGridView dataGridView, string tableName)
        {
            _dataGridView = dataGridView;
            _tableName = tableName;

            TimeSpan hedefSaat = new TimeSpan(0, 0, 0);
            TimeSpan simdikiSaat = DateTime.Now.TimeOfDay;
            TimeSpan ilkCalisma = hedefSaat > simdikiSaat ? hedefSaat - simdikiSaat : new TimeSpan(24, 0, 0) - (simdikiSaat - hedefSaat);

            double ilkCalismaMilisaniye = ilkCalisma.TotalMilliseconds;

            // Timer'ı ayarlıyoruz
            _timer = new System.Timers.Timer(ilkCalismaMilisaniye);
            _timer.Elapsed += TimerElapsed;
            _timer.AutoReset = false;
            _timer.Start();
        }

        private static void TimerElapsed(object? sender, ElapsedEventArgs e)
        {
            try
            {
                if (_timer is not null)
                {
                    
                    GecikmeHesapla(_dataGridView, _tableName);

                    
                    if (_dataGridView != null)
                    {
                        Loaders.GorevleriGetir(_dataGridView);
                    }

                    
                    _timer.Interval = TimeSpan.FromDays(1).TotalMilliseconds;
                    _timer.Start();
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Güncelleme sırasında bir hata oluştu: {ex.Message}");
            }
        }

        public static void GecikmeHesapla(DataGridView dataGridView, string tableName)
        {
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                DateTime? tamamlanmaTarihi = ConvertToDateOnly(row.Cells["TamamTarihi"].Value);
                DateTime? bitisTarihi = ConvertToDateOnly(row.Cells["BitişTarihi"].Value);

                int gecikme = 0; 
                if (row.Cells["Gecikme"].Value != null && int.TryParse(row.Cells["Gecikme"].Value.ToString(), out int parsedValue))
                {
                    gecikme = parsedValue; 
                }
                

               
                if (tamamlanmaTarihi.HasValue && bitisTarihi.HasValue)
                {
                    if (tamamlanmaTarihi.Value > bitisTarihi.Value)
                    {
                        gecikme = (int)(tamamlanmaTarihi.Value - bitisTarihi.Value).TotalDays;
                    }
                    else
                    {
                        gecikme = 0;
                    }
                }
                else if (bitisTarihi.HasValue)
                {
                    if (bitisTarihi.Value < DateTime.Now.Date)
                    {
                        gecikme += (int)(DateTime.Now.Date - bitisTarihi.Value).TotalDays;
                    }
                }

                if (gecikme < 0)
                {
                    gecikme = 0;
                }

                row.Cells["Gecikme"].Value = gecikme;

                if (bitisTarihi.HasValue && bitisTarihi.Value < DateTime.Now.Date)
                {
                    row.Cells["BitişTarihi"].Value = DateTime.Now.Date;
                }

                List<string> columnNames = new List<string> { "Gecikme", "BitişTarihi" };
                List<object> values = new List<object> { gecikme, row.Cells["BitişTarihi"].Value };
                string firstColumnName = dataGridView.Columns[0].Name;
                string condition = $"{firstColumnName} = {row.Cells[firstColumnName].Value}";
                int result = CRUD.guncelle(tableName, columnNames, values, condition, Modder.ModDurumu.Duzenle);

                if (result <= 0)
                {
                    MessageBox.Show($"Veritabanı güncellemesi başarısız oldu: {condition}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private static DateTime? ConvertToDateOnly(object value)
        {
            if (value != null && DateTime.TryParse(value.ToString(), out DateTime dateValue))
            {
                return dateValue.Date;
            }
            return null;
        }




    }
}
