
namespace TokoBuku.BaseForm.Report.Transaksi
{
    partial class LaporanDataPembelian
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
            this.reportDataPelanggan = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // reportDataPelanggan
            // 
            this.reportDataPelanggan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportDataPelanggan.LocalReport.ReportEmbeddedResource = "TokoBuku.BaseForm.Report.Transaksi.ReportDataPenjualan.rdlc";
            this.reportDataPelanggan.Location = new System.Drawing.Point(0, 0);
            this.reportDataPelanggan.Name = "reportDataPelanggan";
            this.reportDataPelanggan.ServerReport.BearerToken = null;
            this.reportDataPelanggan.Size = new System.Drawing.Size(800, 450);
            this.reportDataPelanggan.TabIndex = 0;
            // 
            // LaporanDataPembelian
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportDataPelanggan);
            this.Name = "LaporanDataPembelian";
            this.Text = "LaporanDataPenjualan";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.LaporanDataPenjualan_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportDataPelanggan;
    }
}