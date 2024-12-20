using System.Data; 
using System;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.SQLite;

namespace sql_project
{
    public partial class Form1 : Form
    {

        public void arama(string searching, string table, string searched, DataGridView dataGridView)
        {
            try
            {
                // Tablo ve sütun adlarýný sorguya yazýyoruz
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

            if (int.TryParse(label21.Text, out int gecikme) && gecikme > 0)
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

        void TarihNuller(DataGridView dataGridView, int rowIndex, int columnIndex, DateTimePicker dateTimePicker)
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
                dateTimePicker.CustomFormat = "Null";
                dateTimePicker.Format = DateTimePickerFormat.Custom;
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
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            richTextBox1.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            TarihNuller(dataGridView1, dataGridView1.CurrentRow.Index, 2, dateTimePicker1);
            TarihNuller(dataGridView1, dataGridView1.CurrentRow.Index, 3, dateTimePicker2);
            label21.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            gecikme();
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
    }
}
