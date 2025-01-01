using System;
using System.Data;
using System.Timers;
using System.Windows.Forms;

namespace sql_project
{
    public class Controller
    {
        private static System.Timers.Timer? _timer; // Timer için System.Timers.Timer kullanıyoruz.
        private static DataGridView? _dataGridView; // Global DataGridView referansı.

        public static void BaslatGecikmeGuncelleme(DataGridView dataGridView)
        {
            _dataGridView = dataGridView; // DataGridView referansını kaydediyoruz.

            // Timer her gün gece 12'de çalışacak şekilde ayarlanır
            TimeSpan hedefSaat = new TimeSpan(0, 0, 0); // Gece 12:00
            TimeSpan simdikiSaat = DateTime.Now.TimeOfDay;
            TimeSpan ilkCalisma = hedefSaat > simdikiSaat ? hedefSaat - simdikiSaat : new TimeSpan(24, 0, 0) - (simdikiSaat - hedefSaat);

            // İlk çalışmaya kadar milisaniye hesaplama
            double ilkCalismaMilisaniye = ilkCalisma.TotalMilliseconds;

            // Timer'ı ayarlıyoruz
            _timer = new System.Timers.Timer(ilkCalismaMilisaniye);
            _timer.Elapsed += TimerElapsed; // Timer tetiklenince çalışacak yöntem
            _timer.AutoReset = false; // İlk tetikleme sonrası AutoReset kapatılır.
            _timer.Start();
        }

        private static void TimerElapsed(object? sender, ElapsedEventArgs e)
        {
            try
            {
                if (_timer is not null)
                {
                    // Projeler ve görevler tablosundaki gecikmeleri güncelle
                    GecikmeleriGuncelle();

                    // DataGridView'i güncelle
                    if (_dataGridView != null)
                    {
                        Loaders.GorevleriGetir(_dataGridView); // Görevleri yenile
                    }

                    // Timer'ı günlük çalışacak şekilde yeniden başlat
                    _timer.Interval = TimeSpan.FromDays(1).TotalMilliseconds;
                    _timer.Start();
                }
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                Console.WriteLine($"Güncelleme sırasında bir hata oluştu: {ex.Message}");
            }
        }

        private static void GecikmeleriGuncelle()
        {
            try
            {
                // Projeler tablosundaki gecikmeleri güncelle
                string projeSorgu = @"
                    UPDATE projeler
                    SET Gecikme = CAST((JULIANDAY('now') - JULIANDAY(BitişTarihi)) AS INTEGER)
                    WHERE TamamTarihi IS NULL;
                ";
                CRUD.yürüt(projeSorgu);

                // Görevler tablosundaki gecikmeleri güncelle
                string gorevSorgu = @"
                    UPDATE görevler
                    SET Gecikme = CAST((JULIANDAY('now') - JULIANDAY(BitişTarihi)) AS INTEGER)
                    WHERE Durum <> 'Tamamlandı';
                ";
                CRUD.yürüt(gorevSorgu);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Gecikme güncellenirken bir hata oluştu: {ex.Message}");
            }
        }
    }
}
