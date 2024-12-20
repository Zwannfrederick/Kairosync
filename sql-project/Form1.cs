using System.Data;
using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace sql_project
{
    public partial class Form1 : Form
    {
        MySqlConnection conn = new MySqlConnection("Server=localhost;Database=kairosync;Uid=root;Pwd=1q2q3q4q5Q+-");
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        DataTable dt;


        public void arama(string searching, string table, string searched)
        {
            try
            {
                // Tablo ve sütun adlarýný doðrudan sorguya yazýyoruz
                string query = $"SELECT * FROM {table} WHERE {searched} LIKE @searching";

                MySqlCommand cmd = new MySqlCommand(query, conn);

                // Parametreyi ekliyoruz, sadece deðer parametre olarak geçebilir
                cmd.Parameters.AddWithValue("@searching", "%" + searching + "%");

                adapter = new MySqlDataAdapter(cmd);

                dt = new DataTable();
                adapter.Fill(dt);

                dataGridView1.DataSource = dt;
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

        void SetDateTimePickerFromCellValue(DataGridView dataGridView, int rowIndex, int columnIndex, DateTimePicker dateTimePicker)
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
            dt = new DataTable();
            conn.Open();
            adapter = new MySqlDataAdapter("SELECT * FROM çalýþanlar", conn);
            dataGridView2.DataSource = dt;
            adapter.Fill(dt);
            conn.Close();
        }

        void projeler()
        {
            dt = new DataTable();
            conn.Open();
            adapter = new MySqlDataAdapter("SELECT * FROM projeler", conn);
            dataGridView1.DataSource = dt;
            adapter.Fill(dt);
            conn.Close();
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
            arama(textBox1.Text, "projeler", "Ad");
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
            SetDateTimePickerFromCellValue(dataGridView1, dataGridView1.CurrentRow.Index, 2, dateTimePicker1);
            SetDateTimePickerFromCellValue(dataGridView1, dataGridView1.CurrentRow.Index, 3, dateTimePicker2);
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
                SetDateTimePickerFromCellValue(dataGridView2, dataGridView2.CurrentRow.Index, 5, dateTimePicker5);


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
            arama(textBox5.Text, "çalýþanlar", "Ad");
        }
    }
}
