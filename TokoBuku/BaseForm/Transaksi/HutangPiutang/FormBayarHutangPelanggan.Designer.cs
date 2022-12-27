namespace TokoBuku.BaseForm.Transaksi.HutangPiutang
{
    partial class FormBayarHutangPelanggan
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.DgvListHutang = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxNamaPelanggan = new System.Windows.Forms.TextBox();
            this.comboBoxNoTransaksi = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxTotal = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxNominalBayar = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxJenisKas = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxKembalian = new System.Windows.Forms.TextBox();
            this.buttonBayar = new System.Windows.Forms.Button();
            this.buttonBatal = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvListHutang)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.DgvListHutang, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 146F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(668, 319);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(662, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "PEMBAYARAN HUTANG PELANGGAN";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // DgvListHutang
            // 
            this.DgvListHutang.AllowUserToAddRows = false;
            this.DgvListHutang.AllowUserToDeleteRows = false;
            this.DgvListHutang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvListHutang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DgvListHutang.Location = new System.Drawing.Point(3, 27);
            this.DgvListHutang.Name = "DgvListHutang";
            this.DgvListHutang.ReadOnly = true;
            this.DgvListHutang.Size = new System.Drawing.Size(662, 140);
            this.DgvListHutang.TabIndex = 1;
            this.DgvListHutang.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.DgvListHutang_RowPostPaint);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonBatal);
            this.panel1.Controls.Add(this.buttonBayar);
            this.panel1.Controls.Add(this.textBoxKembalian);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.comboBoxJenisKas);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.textBoxNominalBayar);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.textBoxTotal);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.comboBoxNoTransaksi);
            this.panel1.Controls.Add(this.textBoxNamaPelanggan);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 173);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(662, 143);
            this.panel1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(47, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Nama :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "No. Transaksi :";
            // 
            // textBoxNamaPelanggan
            // 
            this.textBoxNamaPelanggan.Enabled = false;
            this.textBoxNamaPelanggan.Location = new System.Drawing.Point(94, 8);
            this.textBoxNamaPelanggan.Name = "textBoxNamaPelanggan";
            this.textBoxNamaPelanggan.Size = new System.Drawing.Size(173, 20);
            this.textBoxNamaPelanggan.TabIndex = 3;
            // 
            // comboBoxNoTransaksi
            // 
            this.comboBoxNoTransaksi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxNoTransaksi.FormattingEnabled = true;
            this.comboBoxNoTransaksi.Location = new System.Drawing.Point(94, 36);
            this.comboBoxNoTransaksi.Name = "comboBoxNoTransaksi";
            this.comboBoxNoTransaksi.Size = new System.Drawing.Size(173, 21);
            this.comboBoxNoTransaksi.TabIndex = 4;
            this.comboBoxNoTransaksi.SelectedIndexChanged += new System.EventHandler(this.comboBoxNoTransaksi_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(51, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Total :";
            // 
            // textBoxTotal
            // 
            this.textBoxTotal.Enabled = false;
            this.textBoxTotal.Location = new System.Drawing.Point(94, 66);
            this.textBoxTotal.Name = "textBoxTotal";
            this.textBoxTotal.Size = new System.Drawing.Size(173, 20);
            this.textBoxTotal.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(317, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Bayar :";
            // 
            // textBoxNominalBayar
            // 
            this.textBoxNominalBayar.Location = new System.Drawing.Point(363, 8);
            this.textBoxNominalBayar.Name = "textBoxNominalBayar";
            this.textBoxNominalBayar.Size = new System.Drawing.Size(173, 20);
            this.textBoxNominalBayar.TabIndex = 8;
            this.textBoxNominalBayar.TextChanged += new System.EventHandler(this.textBoxNominalBayar_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(299, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Jenis Kas :";
            // 
            // comboBoxJenisKas
            // 
            this.comboBoxJenisKas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxJenisKas.FormattingEnabled = true;
            this.comboBoxJenisKas.Location = new System.Drawing.Point(363, 36);
            this.comboBoxJenisKas.Name = "comboBoxJenisKas";
            this.comboBoxJenisKas.Size = new System.Drawing.Size(173, 21);
            this.comboBoxJenisKas.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(295, 67);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Kembalian :";
            // 
            // textBoxKembalian
            // 
            this.textBoxKembalian.Enabled = false;
            this.textBoxKembalian.Location = new System.Drawing.Point(363, 66);
            this.textBoxKembalian.Name = "textBoxKembalian";
            this.textBoxKembalian.Size = new System.Drawing.Size(173, 20);
            this.textBoxKembalian.TabIndex = 12;
            // 
            // buttonBayar
            // 
            this.buttonBayar.Location = new System.Drawing.Point(363, 105);
            this.buttonBayar.Name = "buttonBayar";
            this.buttonBayar.Size = new System.Drawing.Size(77, 29);
            this.buttonBayar.TabIndex = 13;
            this.buttonBayar.Text = "BAYAR";
            this.buttonBayar.UseVisualStyleBackColor = true;
            this.buttonBayar.Click += new System.EventHandler(this.buttonBayar_Click);
            // 
            // buttonBatal
            // 
            this.buttonBatal.Location = new System.Drawing.Point(459, 105);
            this.buttonBatal.Name = "buttonBatal";
            this.buttonBatal.Size = new System.Drawing.Size(77, 29);
            this.buttonBatal.TabIndex = 14;
            this.buttonBatal.Text = "BATAL";
            this.buttonBatal.UseVisualStyleBackColor = true;
            this.buttonBatal.Click += new System.EventHandler(this.buttonBatal_Click);
            // 
            // FormBayarHutang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 319);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormBayarHutang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bayar Hutang";
            this.Load += new System.EventHandler(this.FormBayarHutang_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvListHutang)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView DgvListHutang;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxNoTransaksi;
        private System.Windows.Forms.TextBox textBoxNamaPelanggan;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonBatal;
        private System.Windows.Forms.Button buttonBayar;
        private System.Windows.Forms.TextBox textBoxKembalian;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBoxJenisKas;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxNominalBayar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxTotal;
        private System.Windows.Forms.Label label4;
    }
}