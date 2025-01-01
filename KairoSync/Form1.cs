using sql_project;
using System.Formats.Asn1;
using System.Windows.Forms;
using static sql_project.Tools;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace KairoSync
{
    public partial class Form1 : Form
    {
        public Modder calisanmod = new Modder();
        public Modder projemod = new Modder();
        public Modder görevmod = new Modder();
        List<Control> çalýþancontrols = [];
        List<Control> projecontrols = [];
        List<Control> görevlercontrols = [];

        public Form1()
        {
            InitializeComponent();
            çalýþancontrols.AddRange(new Control[] { Çalýþanad, Çalýþansoyad, Çalýþanemail, Çalýþandoðumdate, Alankod, Telno });
            projecontrols.AddRange(new Control[] { Projead, Projeacýklama, Projebaþlangýçdate, Projebitiþdate });
            görevlercontrols.AddRange(new Control[] { Görevad, Görevadamgün, Görevamac, Görevbaþlangýçdate, Görevbitiþdate, GörevlerÇalýþanSeç, Görevlerprojeseç });
        }

        private void LoadCountries()
        {
            string jsonFilePath = "ulke.json";
            List<Ülke> countries = Jsonreader.ReadCountries(jsonFilePath);


            Alankod.DataSource = countries;
            Alankod.DisplayMember = "name";
            Alankod.ValueMember = "dial_code";
        }


        public void Form1_Load(object sender, EventArgs e)
        {
            Loaders.ProjeleriGetir(Projelerlist);
            Loaders.CalisanlariGetir(Çalýþanlarlist);
            Loaders.GorevleriGetir(Görevlerlist);
            LoadCountries();
            Controller.BaslatGecikmeGuncelleme(Projelerlist);
            Controller.BaslatGecikmeGuncelleme(Görevlerlist);
            Tools.Projecombobox(Görevlerprojeseç);
            Tools.Çalýþancombobox(GörevlerÇalýþanSeç);
        }

        private void Projelerfilterbox_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Projelerinfo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void Projelermainscreen_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Gecikmesüresilabel_Click(object sender, EventArgs e)
        {

        }

        private void Projelerlist_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Projepanel_Enter(object sender, EventArgs e)
        {

        }

        private void Projelerekle_Click(object sender, EventArgs e)
        {
            if (projemod.aktifMod == Modder.ModDurumu.Ekle)
            {
                Projepanel.Visible = false;
                projemod.ModDegistir(Modder.ModDurumu.None, Projelerlist);
            }
            else
            {
                projemod.ModDegistir(Modder.ModDurumu.Ekle, Projelerlist);
                Projepanel.Visible = true;
            }
        }

        private void Çalýþanekle_Click(object sender, EventArgs e)
        {
            if (calisanmod.aktifMod == Modder.ModDurumu.Ekle)
            {
                calisanmod.ModDegistir(Modder.ModDurumu.None, Çalýþanlarlist);
                Çalýþanpanel.Visible = false;
            }
            else
            {
                calisanmod.ModDegistir(Modder.ModDurumu.Ekle, Çalýþanlarlist);
                Alankod.SelectedValue = "+90";
                Çalýþanpanel.Visible = true;
            }
        }

        private void Görevlerekle_Click(object sender, EventArgs e)
        {
            if (görevmod.aktifMod == Modder.ModDurumu.Ekle)
            {
                calisanmod.ModDegistir(Modder.ModDurumu.None, Çalýþanlarlist);
                Görevpanel.Visible = false;
            }
            else
            {
                görevmod.ModDegistir(Modder.ModDurumu.Ekle, Çalýþanlarlist);
                Görevpanel.Visible = true;
            }
        }

        private void Projelersearchbar_TextChanged(object sender, EventArgs e)
        {

        }

        private void Görevlersearchbar_TextChanged(object sender, EventArgs e)
        {

        }

        private void Görevler_Click(object sender, EventArgs e)
        {

        }

        private void Alankod_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Telno_TextChanged(object sender, EventArgs e)
        {

        }

        private void Çalýþaneklekaydet_Click(object sender, EventArgs e)
        {
            string? selectedValue = Alankod.SelectedValue?.ToString();
            string telno = (string.IsNullOrEmpty(selectedValue) ? string.Empty : selectedValue) + " " + (string.IsNullOrEmpty(Telno.Text) ? "Hatalý telefon formatý lütfen güncelleyiniz" : Telno.Text);
            Tools.CalisanEkle(Çalýþanad.Text + " " + Çalýþansoyad.Text, Çalýþanemail.Text, telno, Çalýþandoðumdate.Value.Date, calisanmod.aktifMod);
            Loaders.CalisanlariGetir(Çalýþanlarlist);
        }

        private void Görevlerdüzenle_Click(object sender, EventArgs e)
        {
            if (görevmod.aktifMod == Modder.ModDurumu.Duzenle)
            {
                görevmod.ModDegistir(Modder.ModDurumu.None, Görevlerlist);
            }
            else
            {
                görevmod.ModDegistir(Modder.ModDurumu.Duzenle, Görevlerlist);
            }
        }

        private void Projelerdüzenle_Click(object sender, EventArgs e)
        {
            if (projemod.aktifMod == Modder.ModDurumu.Duzenle)
            {
                projemod.ModDegistir(Modder.ModDurumu.None, Projelerlist);
            }
            else
            {
                projemod.ModDegistir(Modder.ModDurumu.Duzenle, Projelerlist);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Menü_SelectedIndexChanged(object sender, EventArgs e)
        {
            görevmod.ModDegistir(Modder.ModDurumu.None, Görevlerlist);
            calisanmod.ModDegistir(Modder.ModDurumu.None, Çalýþanlarlist);
            projemod.ModDegistir(Modder.ModDurumu.None, Projelerlist);
            Projepanel.Visible = false;
            Görevpanel.Visible = false;
            Çalýþanpanel.Visible = false;
        }

        private void Görevlerkaydet_Click(object sender, EventArgs e)
        {
            Tools.Kaydet(Görevlerlist, "görevler", görevmod.aktifMod);
        }

        private void Çalýþankaydet_Click(object sender, EventArgs e)
        {
            Tools.Kaydet(Çalýþanlarlist, "çalýþanlar", calisanmod.aktifMod);
        }

        private void Projelerkaydet_Click(object sender, EventArgs e)
        {
            Tools.Kaydet(Projelerlist, "projeler", projemod.aktifMod);
        }

        private void Çalýþandüzenle_Click(object sender, EventArgs e)
        {
            if (projemod.aktifMod == Modder.ModDurumu.Duzenle)
            {
                projemod.ModDegistir(Modder.ModDurumu.None, Çalýþanlarlist);
            }
            else
            {
                projemod.ModDegistir(Modder.ModDurumu.Duzenle, Çalýþanlarlist);
            }
        }

        private void Çalýþansil_Click(object sender, EventArgs e)
        {
            Tools.Sil(Çalýþanlarlist, "çalýþanlar", calisanmod.aktifMod, çalýþancontrols);
        }

        private void Projeeklekaydet_Click(object sender, EventArgs e)
        {
            Tools.ProjeEkle(Projead.Text, Projebaþlangýçdate.Value, Projebitiþdate.Value, Projeacýklama.Text, projemod, Projelerlist);
        }

        private void Projelersil_Click(object sender, EventArgs e)
        {
            Tools.Sil(Projelerlist, "projeler", projemod.aktifMod, projecontrols);
        }

        private void Projelersearchicon_Click(object sender, EventArgs e)
        {
            Tools.Arama(Projelersearchbar.Text, "projeler", "Ad", Projelerlist);
        }

        private void Çalýþanlarsearchicon_Click(object sender, EventArgs e)
        {
            Tools.Arama(Çalýþanlarsearchbar.Text, "çalýþanlar", "AdSoyad", Çalýþanlarlist);
        }

        private void Görevlersearchicon_Click(object sender, EventArgs e)
        {
            Tools.Arama(Görevlersearchbar.Text, "görevler", "Ad", Görevlerlist);
        }

        private void Göreveklekaydet_Click(object sender, EventArgs e)
        {
            Proje? selectedProje = Görevlerprojeseç.SelectedItem as Proje;
            Calisan? selectedCalisan = GörevlerÇalýþanSeç.SelectedItem as Calisan;

            if (selectedProje != null && selectedCalisan != null)
            {

                int projeID = selectedProje.ProjeID;
                int calisanID = selectedCalisan.CalisanID;


                Tools.GorevEkle(Görevad.Text, Görevbaþlangýçdate.Value, Görevbitiþdate.Value, Görevadamgün.Text, projeID, calisanID, Görevamac.Text, görevmod.aktifMod);
            }
            else
            {
                MessageBox.Show("Lütfen bir proje ve bir çalýþan seçin.", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Görevlersil_Click(object sender, EventArgs e)
        {
            Tools.Sil(Projelerlist, "görevler", görevmod.aktifMod, görevlercontrols);
        }
    }
}
