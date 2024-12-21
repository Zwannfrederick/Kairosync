using System.Data; 
using System;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.SQLite;

namespace sql_project
{
    public partial class Form1 : Form
    {
        Boolean projeDuzenlemeModu = false;
        Boolean projeEklemeModu = false;
        public void arama(string searching, string table, string searched, DataGridView dataGridView)
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


        private void gecikme()
        {
            try
            {
                if (int.TryParse(label21.Text, out int gecikme))
                {
                    if (gecikme > 0)
                    {
                        
                        label21.Visible = true;
                        label20.Visible = true;
                    }
                    else
                    {
                        
                        label21.Visible = false;
                        label20.Visible = false;
                    }
                }
                else
                {
                    
                    label21.Visible = false;
                    label20.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gecikme kontrolü sýrasýnda bir hata oluþtu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                label21.Visible = false;
                label20.Visible = false;
            }
        }


        void TarihNuller(DataGridView dataGridView, int rowIndex, int columnIndex, DateTimePicker dateTimePicker)
        {
            try
            {
                var cellValue = dataGridView.Rows[rowIndex].Cells[columnIndex].Value?.ToString();

                if (!string.IsNullOrEmpty(cellValue) && DateTime.TryParse(cellValue, out DateTime parsedDate))
                {
                    dateTimePicker.Value = parsedDate;
                    dateTimePicker.Format = DateTimePickerFormat.Short;
                }
                else
                {
                    
                    dateTimePicker.Value = DateTime.Now;
                    dateTimePicker.Format = DateTimePickerFormat.Short; 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Tarih ayarlanýrken bir hata oluþtu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void temizleProjeAlanlari()
        {
            textBox2.Clear();
            richTextBox1.Clear();
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
        }


        void görevler()
        {
            try
            {
                dataGridView3.DataSource = CRUD.list("SELECT GörevID, Ad, BaþlangýçTarihi, BitiþTarihi, AdamGün, Durum FROM görevler;");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }
        void calisan()
        {
            try
            {
                dataGridView2.DataSource = CRUD.list("SELECT * FROM çalýþanlar");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void AyarlaDataGridView(DataGridView dataGridView)
        {
            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells; 
            dataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True; 
            dataGridView.DefaultCellStyle.Font = new Font("Arial", 10); 
            dataGridView.RowTemplate.Height = 30;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        }
        void projeler()
        {
            try
            {
                dataGridView1.DataSource = CRUD.list("SELECT * FROM projeler");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            arama(textBox1.Text, "projeler", "Ad", dataGridView1);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (!projeEklemeModu)
                {
                    projeEklemeModu = true;
                    textBox2.Clear();
                    richTextBox1.Clear();
                    textBox2.ReadOnly = false;
                    richTextBox1.ReadOnly = false;
                    dateTimePicker1.Enabled = true;
                    dateTimePicker2.Enabled = true;
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(textBox2.Text) && dateTimePicker1.Value <= dateTimePicker2.Value)
                    {
                        List<string> columns = new List<string> { "Ad", "BaþlangýçTarihi", "BitiþTarihi", "aciklama" };
                        List<object> values = new List<object>
                {
                    textBox2.Text,
                    dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                    dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                    richTextBox1.Text
                };

                        int result = CRUD.ekle("projeler", columns, values);

                        if (result > 0)
                        {
                            MessageBox.Show("Proje baþarýyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            
                            projeler();

                            
                            temizleProjeAlanlari();

                            
                            gecikme();
                        }
                        else
                        {
                            MessageBox.Show("Proje eklenemedi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Baþlangýç tarihi bitiþ tarihinden büyük olamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    projeEklemeModu = false;
                    textBox2.ReadOnly = true;
                    richTextBox1.ReadOnly = true;
                    dateTimePicker1.Enabled = false;
                    dateTimePicker2.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluþtu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            calisan();
            projeler();
            görevler();

            AyarlaDataGridView(dataGridView1);
            AyarlaDataGridView(dataGridView2);
            AyarlaDataGridView(dataGridView3);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            AyarlaDataGridView(dataGridView1);
            AyarlaDataGridView(dataGridView2);
            AyarlaDataGridView(dataGridView3);
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBox2.Text = dataGridView1.CurrentRow.Cells["Ad"].Value.ToString();
                richTextBox1.Text = dataGridView1.CurrentRow.Cells["aciklama"].Value.ToString();
                TarihNuller(dataGridView1, dataGridView1.CurrentRow.Index, 2, dateTimePicker1);
                TarihNuller(dataGridView1, dataGridView1.CurrentRow.Index, 3, dateTimePicker2);

                
                var bitisTarihi = dataGridView1.CurrentRow.Cells["BitiþTarihi"].Value?.ToString();
                if (!string.IsNullOrEmpty(bitisTarihi) && DateTime.TryParse(bitisTarihi, out DateTime parsedBitiþ))
                {
                    int gecikme = (int)(DateTime.Now - parsedBitiþ).TotalDays;
                    label21.Text = gecikme.ToString();
                }
                else
                {
                    label21.Text = "0";
                }

                gecikme();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluþtu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void dataGridView2_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBox3.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
                textBox4.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
                textBox6.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();
                textBox8.Text = dataGridView2.CurrentRow.Cells[4].Value.ToString();
                TarihNuller(dataGridView2, dataGridView2.CurrentRow.Index, 5, dateTimePicker5);


            }
            catch
            {
                throw;
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker5_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            arama(textBox5.Text, "çalýþanlar", "Ad", dataGridView2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.ReadOnly = false;
            textBox2.ReadOnly = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox3.ReadOnly = false;
            textBox4.ReadOnly = false;
            textBox6.ReadOnly = false;
            textBox8.ReadOnly = false;
            dateTimePicker5.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox3.ReadOnly = true;
            textBox4.ReadOnly = true;
            textBox6.ReadOnly = true;
            textBox8.ReadOnly = true;
            dateTimePicker5.Enabled = false;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            textBox9.ReadOnly = false;
            richTextBox2.ReadOnly = false;
            dateTimePicker4.Enabled = true;
            dateTimePicker3.Enabled = true;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBox9.ReadOnly = true;
            richTextBox2.ReadOnly = true;
            dateTimePicker4.Enabled = false;
            dateTimePicker3.Enabled = false;
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (projeEklemeModu || projeDuzenlemeModu)
                {
                    temizleProjeAlanlari();
                    return;
                }

                
                if (dataGridView1.CurrentRow != null)
                {
                    string projeID = dataGridView1.CurrentRow.Cells["ProjeID"].Value?.ToString();

                    if (!string.IsNullOrEmpty(projeID))
                    {
                        
                        DialogResult dialogResult = MessageBox.Show("Bu projeyi silmek istediðinize emin misiniz?", "Silme Onayý", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                        if (dialogResult == DialogResult.Yes)
                        {
                           
                            int result = CRUD.sil("projeler", $"ProjeID = {projeID}");
                            if (result > 0)
                            {
                                MessageBox.Show("Proje baþarýyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                projeler();

                                
                                temizleProjeAlanlari();

                               
                                label21.Text = "0";
                                gecikme();
                            }
                            else
                            {
                                MessageBox.Show("Proje silinemedi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Lütfen silmek için bir proje seçin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen bir satýr seçin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluþtu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
