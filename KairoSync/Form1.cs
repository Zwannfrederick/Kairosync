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
        public Modder g�revmod = new Modder();
        public Form1()
        {
            InitializeComponent();
        }

        private void LoadCountries()
        {
            string jsonFilePath = "ulke.json";
            List<�lke> countries = jsonreader.ReadCountries(jsonFilePath);


            Alankod.DataSource = countries;
            Alankod.DisplayMember = "name";
            Alankod.ValueMember = "dial_code";
        }


        public void Form1_Load(object sender, EventArgs e)
        {
            Loaders.ProjeleriGetir(Projelerlist);
            Loaders.CalisanlariGetir(�al��anlarlist);
            Loaders.GorevleriGetir(G�revlerlist);
            LoadCountries();
            Controller.BaslatGecikmeGuncelleme(Projelerlist);
            Controller.BaslatGecikmeGuncelleme(G�revlerlist);
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
            Tools.Arama(Projelersearchbar.Text, "�al��anlar", "AdSoyad", Projelerlist);
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

        private void Gecikmes�resilabel_Click(object sender, EventArgs e)
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

        private void �al��anekle_Click(object sender, EventArgs e)
        {
            if (�al��anpanel.Visible == true)
            {
                �al��anpanel.Visible = false;
            }
            else
            {
                Alankod.SelectedValue = "+90";
                �al��anpanel.Visible = true;
            }
        }

        private void G�revlerekle_Click(object sender, EventArgs e)
        {
            if (G�revpanel.Visible == true)
            {
                G�revpanel.Visible = false;
            }
            else
            {
                G�revpanel.Visible = true;
            }
        }

        private void Projelersearchbar_TextChanged(object sender, EventArgs e)
        {
            Tools.Arama(Projelersearchbar.Text, "projeler", "Ad", Projelerlist);
        }

        private void G�revlersearchbar_TextChanged(object sender, EventArgs e)
        {
            Tools.Arama(Projelersearchbar.Text, "g�revler", "Ad", Projelerlist);
        }

        private void G�revler_Click(object sender, EventArgs e)
        {

        }

        private void Alankod_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Telno_TextChanged(object sender, EventArgs e)
        {

        }

        private void �al��aneklekaydet_Click(object sender, EventArgs e)
        {
            string selectedValue = Alankod.SelectedValue?.ToString();
            string telno = selectedValue + " " + Telno.Text;
            Tools.CalisanEkle(�al��anad.Text + " " + �al��ansoyad.Text, �al��anemail.Text, telno, �al��ando�umdate.Value.Date);
            Loaders.CalisanlariGetir(�al��anlarlist);
        }

        private void G�revlerd�zenle_Click(object sender, EventArgs e)
        {
            if (g�revmod.aktifMod == Modder.ModDurumu.Duzenle)
            {
                g�revmod.ModDegistir(Modder.ModDurumu.None, G�revlerlist);
            }
            else
            {
                g�revmod.ModDegistir(Modder.ModDurumu.Duzenle, G�revlerlist);
            }
        }

        private void Projelerd�zenle_Click(object sender, EventArgs e)
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
