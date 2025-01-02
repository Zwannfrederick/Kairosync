namespace KairoSync
{
    partial class Çalışandetay
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox1 = new GroupBox();
            çalışanprojegörev = new DataGridView();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)çalışanprojegörev).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(çalışanprojegörev);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(800, 450);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Çalışan Detay";
            // 
            // çalışanprojegörev
            // 
            çalışanprojegörev.AllowUserToAddRows = false;
            çalışanprojegörev.AllowUserToDeleteRows = false;
            çalışanprojegörev.AllowUserToResizeColumns = false;
            çalışanprojegörev.AllowUserToResizeRows = false;
            çalışanprojegörev.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            çalışanprojegörev.Dock = DockStyle.Fill;
            çalışanprojegörev.Location = new Point(3, 23);
            çalışanprojegörev.Name = "çalışanprojegörev";
            çalışanprojegörev.ReadOnly = true;
            çalışanprojegörev.RowHeadersWidth = 51;
            çalışanprojegörev.Size = new Size(794, 424);
            çalışanprojegörev.TabIndex = 0;
            // 
            // Çalışandetay
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(groupBox1);
            Name = "Çalışandetay";
            Text = "Çalışandetay";
            Load += Çalışandetay_Load;
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)çalışanprojegörev).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private DataGridView çalışanprojegörev;
    }
}