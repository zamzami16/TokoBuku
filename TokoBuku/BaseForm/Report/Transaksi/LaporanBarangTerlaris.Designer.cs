namespace TokoBuku.BaseForm.Report.Transaksi
{
    partial class LaporanBarangTerlaris
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.label1 = new System.Windows.Forms.Label();
            this.cbThisMonth = new System.Windows.Forms.CheckBox();
            this.dtpDari = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpSampai = new System.Windows.Forms.DateTimePicker();
            this.btnTerapkan = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.nud = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.nud);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.btnTerapkan);
            this.splitContainer1.Panel1.Controls.Add(this.dtpSampai);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.dtpDari);
            this.splitContainer1.Panel1.Controls.Add(this.cbThisMonth);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.reportViewer1);
            this.splitContainer1.Size = new System.Drawing.Size(800, 450);
            this.splitContainer1.SplitterDistance = 172;
            this.splitContainer1.TabIndex = 0;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "TokoBuku.BaseForm.Report.Transaksi.LaporanBarangTerlaris.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(624, 450);
            this.reportViewer1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tanggal Mulai:";
            // 
            // cbThisMonth
            // 
            this.cbThisMonth.AutoSize = true;
            this.cbThisMonth.Checked = true;
            this.cbThisMonth.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbThisMonth.Location = new System.Drawing.Point(15, 42);
            this.cbThisMonth.Name = "cbThisMonth";
            this.cbThisMonth.Size = new System.Drawing.Size(102, 17);
            this.cbThisMonth.TabIndex = 1;
            this.cbThisMonth.Text = "Bulan Sekarang";
            this.cbThisMonth.UseVisualStyleBackColor = true;
            this.cbThisMonth.CheckedChanged += new System.EventHandler(this.cbThisMonth_CheckedChanged);
            // 
            // dtpDari
            // 
            this.dtpDari.Enabled = false;
            this.dtpDari.Location = new System.Drawing.Point(15, 89);
            this.dtpDari.Name = "dtpDari";
            this.dtpDari.Size = new System.Drawing.Size(143, 20);
            this.dtpDari.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tanggal Sampai:";
            // 
            // dtpSampai
            // 
            this.dtpSampai.Enabled = false;
            this.dtpSampai.Location = new System.Drawing.Point(15, 138);
            this.dtpSampai.Name = "dtpSampai";
            this.dtpSampai.Size = new System.Drawing.Size(143, 20);
            this.dtpSampai.TabIndex = 4;
            // 
            // btnTerapkan
            // 
            this.btnTerapkan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTerapkan.Location = new System.Drawing.Point(15, 198);
            this.btnTerapkan.Name = "btnTerapkan";
            this.btnTerapkan.Size = new System.Drawing.Size(143, 29);
            this.btnTerapkan.TabIndex = 5;
            this.btnTerapkan.Text = "Terapkan";
            this.btnTerapkan.UseVisualStyleBackColor = true;
            this.btnTerapkan.Click += new System.EventHandler(this.btnTerapkan_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 172);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Jumlah Dipilih";
            // 
            // nud
            // 
            this.nud.Location = new System.Drawing.Point(89, 170);
            this.nud.Name = "nud";
            this.nud.Size = new System.Drawing.Size(69, 20);
            this.nud.TabIndex = 7;
            this.nud.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // LaporanBarangTerlaris
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.Name = "LaporanBarangTerlaris";
            this.Text = "Laporan Barang Terlaris";
            this.Load += new System.EventHandler(this.LaporanBarangTerlaris_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nud)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.Button btnTerapkan;
        private System.Windows.Forms.DateTimePicker dtpSampai;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpDari;
        private System.Windows.Forms.CheckBox cbThisMonth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nud;
        private System.Windows.Forms.Label label3;
    }
}