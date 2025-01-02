using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using sql_project;

namespace KairoSync
{
    public partial class Çalışandetay : Form
    {
        DataGridView çalışan;
        public Çalışandetay(DataGridView çalışan)
        {
            InitializeComponent();
            this.çalışan = çalışan;
        }

        private void Çalışandetay_Load(object sender, EventArgs e)
        {
            Loaders.CalisanGorevleriniGetir(çalışan, çalışanprojegörev);
        }
    }
}
