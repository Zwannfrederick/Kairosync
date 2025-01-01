using sql_project;
using System.Formats.Asn1;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace KairoSync
{
    public partial class Form1 : Form
    {
        public Modder calisanmod = new Modder();
        public Modder projemod = new Modder();
        public Modder görevmod = new Modder();
        public Form1()
        {
            InitializeComponent();
        }

        private void LoadCountries()
        {
            string jsonFilePath = "ulke.json";
            List<Ülke> countries = jsonreader.ReadCountries(jsonFilePath);


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
            Tools.Arama(Projelersearchbar.Text, "çalýþanlar", "AdSoyad", Projelerlist);
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
            if (Projepanel.Visible == true)
            {
                Projepanel.Visible = false;
            }
            else
            {
                Projepanel.Visible = true;
            }
        }

        private void Çalýþanekle_Click(object sender, EventArgs e)
        {
            if (Çalýþanpanel.Visible == true)
            {
                Çalýþanpanel.Visible = false;
            }
            else
            {
                Alankod.SelectedValue = "+90";
                Çalýþanpanel.Visible = true;
            }
        }

        private void Görevlerekle_Click(object sender, EventArgs e)
        {
            if (Görevpanel.Visible == true)
            {
                Görevpanel.Visible = false;
            }
            else
            {
                Görevpanel.Visible = true;
            }
        }

        private void Projelersearchbar_TextChanged(object sender, EventArgs e)
        {
            Tools.Arama(Projelersearchbar.Text, "projeler", "Ad", Projelerlist);
        }

        private void Görevlersearchbar_TextChanged(object sender, EventArgs e)
        {
            Tools.Arama(Projelersearchbar.Text, "görevler", "Ad", Projelerlist);
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
            string selectedValue = Alankod.SelectedValue?.ToString();
            string telno = selectedValue + " " + Telno.Text;
            Tools.CalisanEkle(Çalýþanad.Text + " " + Çalýþansoyad.Text, Çalýþanemail.Text, telno, Çalýþandoðumdate.Value.Date);
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
    }
}
