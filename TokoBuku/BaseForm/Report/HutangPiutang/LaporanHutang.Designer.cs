namespace TokoBuku.BaseForm.Report.HutangPiutang
{
    partial class LaporanHutang
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
            this.buttonTerapkan = new System.Windows.Forms.Button();
            this.dtpSampai = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpMulai = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.checkBoxBelumLunasSaja = new System.Windows.Forms.CheckBox();
            this.checkBoxSemauTanggal = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.checkBoxSemauTanggal);
            this.splitContainer1.Panel1.Controls.Add(this.checkBoxBelumLunasSaja);
            this.splitContainer1.Panel1.Controls.Add(this.buttonTerapkan);
            this.splitContainer1.Panel1.Controls.Add(this.dtpSampai);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.dtpMulai);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.reportViewer1);
            this.splitContainer1.Size = new System.Drawing.Size(800, 450);
            this.splitContainer1.SplitterDistance = 181;
            this.splitContainer1.TabIndex = 0;
            // 
            // buttonTerapkan
            // 
            this.buttonTerapkan.Location = new System.Drawing.Point(15, 187);
            this.buttonTerapkan.Name = "buttonTerapkan";
            this.buttonTerapkan.Size = new System.Drawing.Size(152, 23);
            this.buttonTerapkan.TabIndex = 4;
            this.buttonTerapkan.Text = "Terapkan";
            this.buttonTerapkan.UseVisualStyleBackColor = true;
            this.buttonTerapkan.Click += new System.EventHandler(this.buttonTerapkan_Click);
            // 
            // dtpSampai
            // 
            this.dtpSampai.Location = new System.Drawing.Point(15, 103);
            this.dtpSampai.Name = "dtpSampai";
            this.dtpSampai.Size = new System.Drawing.Size(152, 20);
            this.dtpSampai.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Pilih Tanggal Selesai:";
            // 
            // dtpMulai
            // 
            this.dtpMulai.Location = new System.Drawing.Point(15, 53);
            this.dtpMulai.Name = "dtpMulai";
            this.dtpMulai.Size = new System.Drawing.Size(152, 20);
            this.dtpMulai.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pilih Tanggal Mulai:";
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "TokoBuku.BaseForm.Report.HutangPiutang.RiwayatHutang.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(615, 450);
            this.reportViewer1.TabIndex = 0;
            this.reportViewer1.WaitControlDisplayAfter = 100;
            // 
            // checkBoxBelumLunasSaja
            // 
            this.checkBoxBelumLunasSaja.AutoSize = true;
            this.checkBoxBelumLunasSaja.Checked = true;
            this.checkBoxBelumLunasSaja.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxBelumLunasSaja.Location = new System.Drawing.Point(15, 162);
            this.checkBoxBelumLunasSaja.Name = "checkBoxBelumLunasSaja";
            this.checkBoxBelumLunasSaja.Size = new System.Drawing.Size(111, 17);
            this.checkBoxBelumLunasSaja.TabIndex = 5;
            this.checkBoxBelumLunasSaja.Text = "Belum Lunas Saja";
            this.checkBoxBelumLunasSaja.UseVisualStyleBackColor = true;
            // 
            // checkBoxSemauTanggal
            // 
            this.checkBoxSemauTanggal.AutoSize = true;
            this.checkBoxSemauTanggal.Checked = true;
            this.checkBoxSemauTanggal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSemauTanggal.Location = new System.Drawing.Point(15, 135);
            this.checkBoxSemauTanggal.Name = "checkBoxSemauTanggal";
            this.checkBoxSemauTanggal.Size = new System.Drawing.Size(101, 17);
            this.checkBoxSemauTanggal.TabIndex = 6;
            this.checkBoxSemauTanggal.Text = "Semua Tanggal";
            this.checkBoxSemauTanggal.UseVisualStyleBackColor = true;
            this.checkBoxSemauTanggal.CheckedChanged += new System.EventHandler(this.checkBoxSemauTanggal_CheckedChanged);
            // 
            // LaporanHutang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.Name = "LaporanHutang";
            this.Text = "Laporan Hutang ke Supplier";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.LaporanLabaRugi_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button buttonTerapkan;
        private System.Windows.Forms.DateTimePicker dtpSampai;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpMulai;
        private System.Windows.Forms.Label label1;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.CheckBox checkBoxBelumLunasSaja;
        private System.Windows.Forms.CheckBox checkBoxSemauTanggal;
    }
}