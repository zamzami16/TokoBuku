namespace TokoBuku.BaseForm.Report.Transaksi
{
    partial class KartuStockBarang
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
            this.btnTerapkan = new System.Windows.Forms.Button();
            this.chBTgl = new System.Windows.Forms.CheckBox();
            this.dtpSampai = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpMulai = new System.Windows.Forms.DateTimePicker();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
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
            this.splitContainer1.Panel1.Controls.Add(this.btnTerapkan);
            this.splitContainer1.Panel1.Controls.Add(this.chBTgl);
            this.splitContainer1.Panel1.Controls.Add(this.dtpSampai);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.dtpMulai);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.reportViewer1);
            this.splitContainer1.Size = new System.Drawing.Size(800, 450);
            this.splitContainer1.SplitterDistance = 173;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnTerapkan
            // 
            this.btnTerapkan.Location = new System.Drawing.Point(15, 169);
            this.btnTerapkan.Name = "btnTerapkan";
            this.btnTerapkan.Size = new System.Drawing.Size(148, 23);
            this.btnTerapkan.TabIndex = 6;
            this.btnTerapkan.Text = "TERAPKAN";
            this.btnTerapkan.UseVisualStyleBackColor = true;
            this.btnTerapkan.Click += new System.EventHandler(this.btnTerapkan_Click);
            // 
            // chBTgl
            // 
            this.chBTgl.AutoSize = true;
            this.chBTgl.Checked = true;
            this.chBTgl.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chBTgl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chBTgl.Location = new System.Drawing.Point(15, 28);
            this.chBTgl.Name = "chBTgl";
            this.chBTgl.Size = new System.Drawing.Size(131, 21);
            this.chBTgl.TabIndex = 5;
            this.chBTgl.Text = "Semua Tanggal:";
            this.chBTgl.UseVisualStyleBackColor = true;
            this.chBTgl.CheckedChanged += new System.EventHandler(this.chBTgl_CheckedChanged);
            // 
            // dtpSampai
            // 
            this.dtpSampai.Enabled = false;
            this.dtpSampai.Location = new System.Drawing.Point(15, 125);
            this.dtpSampai.Name = "dtpSampai";
            this.dtpSampai.Size = new System.Drawing.Size(148, 20);
            this.dtpSampai.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Sampai:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Mulai:";
            // 
            // dtpMulai
            // 
            this.dtpMulai.Enabled = false;
            this.dtpMulai.Location = new System.Drawing.Point(15, 77);
            this.dtpMulai.Name = "dtpMulai";
            this.dtpMulai.Size = new System.Drawing.Size(148, 20);
            this.dtpMulai.TabIndex = 1;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "TokoBuku.BaseForm.Report.Transaksi.ReportKartuStock.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(623, 450);
            this.reportViewer1.TabIndex = 0;
            // 
            // KartuStockBarang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.Name = "KartuStockBarang";
            this.Text = "KartuStockBarang";
            this.Load += new System.EventHandler(this.KartuStockBarang_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DateTimePicker dtpSampai;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpMulai;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.CheckBox chBTgl;
        private System.Windows.Forms.Button btnTerapkan;
    }
}