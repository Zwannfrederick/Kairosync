using System.Data;
using MySql.Data.MySqlClient;

namespace sql_project
{
    public partial class Form1 : Form
    {
        MySqlConnection conn = new MySqlConnection("Server=localhost;Database=kairosync;Uid=root;Pwd=1q2q3q4q5Q+-");
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        DataTable dt;

        void calisan()
        {
            dt = new DataTable();
            conn.Open();
            adapter = new MySqlDataAdapter("SELECT * FROM çalýþanlar" , conn);
            dataGridView2.DataSource = dt;
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
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBox3.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
                textBox4.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
                textBox6.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();
                textBox8.Text = dataGridView2.CurrentRow.Cells[4].Value.ToString();
                dateTimePicker5.Value = DateTime.Parse(dataGridView2.CurrentRow.Cells[5].Value.ToString());
            }
            catch
            {
                throw;
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
