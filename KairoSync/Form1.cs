using sql_project;
using System.Formats.Asn1;
using System.Globalization;
using System.Windows.Forms;
using static sql_project.Tools;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace KairoSync
{
    public partial class Form1 : Form
    {
        public Modder calisanmod = new Modder();
        public Modder projemod = new Modder();
        public Modder g�revmod = new Modder();
        List<Control> �al��ancontrols = [];
        List<Control> projecontrols = [];
        List<Control> g�revlercontrols = [];

        public Form1()
        {
            InitializeComponent();
            �al��ancontrols.AddRange(new Control[] { �al��anad, �al��ansoyad, �al��anemail, �al��ando�umdate, Alankod, Telno });
            projecontrols.AddRange(new Control[] { Projead, Projeac�klama, Projeba�lang��date, Projebiti�date });
            g�revlercontrols.AddRange(new Control[] { G�revad, G�revadamg�n, G�revamac, G�revba�lang��date, G�revbiti�date, G�revler�al��anSe�, G�revlerprojese� });
            
        }

        private void LoadCountries()
        {
            string jsonFilePath = "ulke.json";
            List<�lke> countries = Jsonreader.ReadCountries(jsonFilePath);


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
            Tools.Projecombobox(G�revlerprojese�);
            Tools.�al��ancombobox(G�revler�al��anSe�);
            Controller.BaslatGecikmeGuncelleme(G�revlerlist, "g�revler");
            Controller.BaslatGecikmeGuncelleme(Projelerlist, "projeler");
            Controller.GecikmeHesapla(Projelerlist, "projeler");
            Controller.GecikmeHesapla(G�revlerlist, "g�revler");
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

        private void �al��anekle_Click(object sender, EventArgs e)
        {
            if (calisanmod.aktifMod == Modder.ModDurumu.Ekle)
            {
                calisanmod.ModDegistir(Modder.ModDurumu.None, �al��anlarlist);
                �al��anpanel.Visible = false;
            }
            else
            {
                calisanmod.ModDegistir(Modder.ModDurumu.Ekle, �al��anlarlist);
                Alankod.SelectedValue = "+90";
                �al��anpanel.Visible = true;
            }
        }

        private void G�revlerekle_Click(object sender, EventArgs e)
        {
            if (g�revmod.aktifMod == Modder.ModDurumu.Ekle)
            {
                calisanmod.ModDegistir(Modder.ModDurumu.None, �al��anlarlist);
                G�revpanel.Visible = false;
            }
            else
            {
                g�revmod.ModDegistir(Modder.ModDurumu.Ekle, �al��anlarlist);
                Tools.Projecombobox(G�revlerprojese�);
                Tools.�al��ancombobox(G�revler�al��anSe�);
                G�revpanel.Visible = true;
            }
        }

        private void Projelersearchbar_TextChanged(object sender, EventArgs e)
        {

        }

        private void G�revlersearchbar_TextChanged(object sender, EventArgs e)
        {

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
            string? selectedValue = Alankod.SelectedValue?.ToString();
            string? telno;
            string? ad = �al��anad.Text;
            string? soyad = �al��ansoyad.Text;
            ad = ad ?? string.Empty;
            soyad = soyad ?? string.Empty;

            TextInfo textInfo = new CultureInfo("tr-TR", false).TextInfo;
            string capitalizedAd = textInfo.ToTitleCase(ad.ToLower());
            string capitalizedSoyad = textInfo.ToTitleCase(soyad.ToLower());
            if (string.IsNullOrEmpty(selectedValue) || string.IsNullOrEmpty(Telno.Text))
            {
                MessageBox.Show("Hatal� telefon format� l�tfen g�ncelleyiniz");
                telno = string.Empty;
            }
            else
            {
                telno = selectedValue + Telno.Text.Trim();
            }
            
            Tools.CalisanEkle(capitalizedAd + " " + capitalizedSoyad, �al��anemail.Text, telno, �al��ando�umdate.Value.Date, calisanmod.aktifMod);
            Loaders.CalisanlariGetir(�al��anlarlist);
            Loaders.GorevleriGetir(G�revlerlist);
           

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

        private void Men�_SelectedIndexChanged(object sender, EventArgs e)
        {
            g�revmod.ModDegistir(Modder.ModDurumu.None, G�revlerlist);
            calisanmod.ModDegistir(Modder.ModDurumu.None, �al��anlarlist);
            projemod.ModDegistir(Modder.ModDurumu.None, Projelerlist);
            Projepanel.Visible = false;
            G�revpanel.Visible = false;
            �al��anpanel.Visible = false;
        }

        private void G�revlerkaydet_Click(object sender, EventArgs e)
        {
            Tools.Kaydet(G�revlerlist, "g�revler", g�revmod.aktifMod);
            Loaders.GorevleriGetir(G�revlerlist);
            Controller.GecikmeHesapla(G�revlerlist, "g�revler");
            g�revmod.ModDegistir(Modder.ModDurumu.None, G�revlerlist);
        }

        private void �al��ankaydet_Click(object sender, EventArgs e)
        {
            Tools.Kaydet(�al��anlarlist, "�al��anlar", calisanmod.aktifMod);
            Loaders.CalisanlariGetir(�al��anlarlist);
            calisanmod.ModDegistir(Modder.ModDurumu.None, �al��anlarlist);
        }

        private void Projelerkaydet_Click(object sender, EventArgs e)
        {
            Tools.Kaydet(Projelerlist, "projeler", projemod.aktifMod);
            Loaders.ProjeleriGetir(Projelerlist);
            Controller.GecikmeHesapla(Projelerlist, "projeler");
            projemod.ModDegistir(Modder.ModDurumu.None, Projelerlist);
        }

        private void �al��and�zenle_Click(object sender, EventArgs e)
        {
            if (calisanmod.aktifMod == Modder.ModDurumu.Duzenle)
            {
                calisanmod.ModDegistir(Modder.ModDurumu.None, �al��anlarlist);
            }
            else
            {
                calisanmod.ModDegistir(Modder.ModDurumu.Duzenle, �al��anlarlist);
            }
        }

        private void �al��ansil_Click(object sender, EventArgs e)
        {
            Tools.Sil(�al��anlarlist, "�al��anlar", calisanmod.aktifMod, �al��ancontrols);
            Loaders.CalisanlariGetir(�al��anlarlist);
        }

        private void Projeeklekaydet_Click(object sender, EventArgs e)
        {
            Tools.ProjeEkle(Projead.Text, Projeba�lang��date.Value, Projebiti�date.Value, Projeac�klama.Text, projemod, Projelerlist);
            Loaders.ProjeleriGetir(Projelerlist);
            Loaders.GorevleriGetir(G�revlerlist);
            Controller.GecikmeHesapla(Projelerlist, "projeler");
        }

        private void Projelersil_Click(object sender, EventArgs e)
        {
            Tools.Sil(Projelerlist, "projeler", projemod.aktifMod, projecontrols);
            Loaders.ProjeleriGetir(Projelerlist);
        }

        private void Projelersearchicon_Click(object sender, EventArgs e)
        {
            Tools.Arama(Projelersearchbar.Text, "projeler", "Ad", Projelerlist);
        }

        private void �al��anlarsearchicon_Click(object sender, EventArgs e)
        {
            Tools.Arama(�al��anlarsearchbar.Text, "�al��anlar", "AdSoyad", �al��anlarlist);
        }

        private void G�revlersearchicon_Click(object sender, EventArgs e)
        {
            Tools.Arama(G�revlersearchbar.Text, "g�revler", "Ad", G�revlerlist);
        }

        private void G�reveklekaydet_Click(object sender, EventArgs e)
        {
            Proje? selectedProje = G�revlerprojese�.SelectedItem as Proje;
            Calisan? selectedCalisan = G�revler�al��anSe�.SelectedItem as Calisan;

            if (selectedProje != null && selectedCalisan != null)
            {
                int projeID = selectedProje.ProjeID;
                int calisanID = selectedCalisan.CalisanID;
                Tools.GorevEkle(G�revad.Text, G�revba�lang��date.Value, G�revbiti�date.Value, G�revadamg�n.Text, projeID, calisanID, G�revamac.Text, g�revmod.aktifMod);
                Loaders.GorevleriGetir(G�revlerlist);
                Controller.GecikmeHesapla(G�revlerlist, "g�revler");
            }
            else
            {
                MessageBox.Show("L�tfen bir proje ve bir �al��an se�in.", "Uyar�", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void G�revlersil_Click(object sender, EventArgs e)
        {
            Tools.Sil(G�revlerlist, "g�revler", g�revmod.aktifMod, g�revlercontrols);
            Loaders.GorevleriGetir(G�revlerlist);
        }

        private void �al��anlarlist_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void �al��anlarfiltericon_Click(object sender, EventArgs e)
        {
            
            if (�al��anlarlist.SelectedRows.Count > 0)
            {
                
                �al��andetay �al��andetay = new �al��andetay(�al��anlarlist);
                �al��andetay.Show();
            }
            else
            {
                
                MessageBox.Show("L�tfen bir �al��an se�in.", "Uyar�", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
    }
}
