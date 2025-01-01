using System;
using System.Windows.Forms;

namespace sql_project
{
    public class Modder
    {
        // Enum tanımını sınıf düzeyine taşıdım
        public enum ModDurumu
        {
            None,       // Hiçbir mod aktif değil
            Ekle,       // Ekleme modu
            Duzenle   // Düzenleme modu
        }

        // Aktif mod değişkenini sınıf düzeyinde tanımlıyorum
        public ModDurumu aktifMod = ModDurumu.None;

        public void ModKontrolu<T>(ref bool modDurumu, Action temizlemeFonksiyonu, params T[] bileşenler)
        {
            modDurumu = !modDurumu;

            foreach (var bilesen in bileşenler)
            {
                switch (bilesen)
                {
                    case TextBox tb:
                        tb.Clear();
                        tb.ReadOnly = !modDurumu;
                        break;
                    case RichTextBox rtb:
                        rtb.Clear();
                        rtb.ReadOnly = !modDurumu;
                        break;
                    case DateTimePicker dtp:
                        dtp.Enabled = modDurumu;
                        break;
                    case DataGridView dgv:
                        dgv.ClearSelection();
                        break;
                    default:
                        throw new ArgumentException("Desteklenmeyen bileşen türü");
                }
            }

            temizlemeFonksiyonu?.Invoke();
        }

        public void ModDegistir(ModDurumu yeniMod, DataGridView dataGridView)
        {
            aktifMod = yeniMod;

            switch (aktifMod)
            {
                case ModDurumu.Ekle:
                    dataGridView.ClearSelection();
                    // Ekleme moduna geçiş
                    break;
                case ModDurumu.Duzenle:
                    dataGridView.ClearSelection();
                    dataGridView.ReadOnly = false;  // Düzenleme için DataGridView'in kilidi açılır
                    break;
                case ModDurumu.None:
                    dataGridView.ClearSelection();
                    // Mod kapalı, sadece verileri görüntüle
                    break;
            }
        }

    }
}
