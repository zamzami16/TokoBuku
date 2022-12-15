
namespace TokoBuku
{
    partial class TokoBukuWindows
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.iNPUTDATAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DataBarangToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.KategoriBukuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PenerbitBukuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rAKBUKUToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DataSupplierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DataPelangganToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DataKasirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DataKasMasterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tRANSAKSIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PenjualanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PembelianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lAPORANToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iNPUTDATAToolStripMenuItem,
            this.tRANSAKSIToolStripMenuItem,
            this.lAPORANToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(564, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // iNPUTDATAToolStripMenuItem
            // 
            this.iNPUTDATAToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DataBarangToolStripMenuItem,
            this.DataSupplierToolStripMenuItem,
            this.DataPelangganToolStripMenuItem,
            this.DataKasirToolStripMenuItem,
            this.DataKasMasterToolStripMenuItem});
            this.iNPUTDATAToolStripMenuItem.Name = "iNPUTDATAToolStripMenuItem";
            this.iNPUTDATAToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.iNPUTDATAToolStripMenuItem.Text = "INPUT DATA";
            this.iNPUTDATAToolStripMenuItem.ToolTipText = "Tampilkan Data";
            // 
            // DataBarangToolStripMenuItem
            // 
            this.DataBarangToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.KategoriBukuToolStripMenuItem,
            this.PenerbitBukuToolStripMenuItem,
            this.rAKBUKUToolStripMenuItem});
            this.DataBarangToolStripMenuItem.Name = "DataBarangToolStripMenuItem";
            this.DataBarangToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.DataBarangToolStripMenuItem.Text = "DATA BARANG";
            this.DataBarangToolStripMenuItem.ToolTipText = "Tampilkan Data Barang";
            this.DataBarangToolStripMenuItem.Click += new System.EventHandler(this.DataBarangToolStripMenuItem_Click);
            // 
            // KategoriBukuToolStripMenuItem
            // 
            this.KategoriBukuToolStripMenuItem.Name = "KategoriBukuToolStripMenuItem";
            this.KategoriBukuToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.KategoriBukuToolStripMenuItem.Text = "KATEGORI BUKU";
            this.KategoriBukuToolStripMenuItem.Click += new System.EventHandler(this.DataKategoriToolStripMenuItem_Click);
            // 
            // PenerbitBukuToolStripMenuItem
            // 
            this.PenerbitBukuToolStripMenuItem.Name = "PenerbitBukuToolStripMenuItem";
            this.PenerbitBukuToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.PenerbitBukuToolStripMenuItem.Text = "PENERBIT BUKU";
            this.PenerbitBukuToolStripMenuItem.Click += new System.EventHandler(this.DataPenerbitToolStripMenuItem_Click);
            // 
            // rAKBUKUToolStripMenuItem
            // 
            this.rAKBUKUToolStripMenuItem.Name = "rAKBUKUToolStripMenuItem";
            this.rAKBUKUToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.rAKBUKUToolStripMenuItem.Text = "RAK BUKU";
            this.rAKBUKUToolStripMenuItem.Click += new System.EventHandler(this.DataRakToolStripMenuItem_Click);
            // 
            // DataSupplierToolStripMenuItem
            // 
            this.DataSupplierToolStripMenuItem.Name = "DataSupplierToolStripMenuItem";
            this.DataSupplierToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.DataSupplierToolStripMenuItem.Text = "DATA SUPPLIER";
            this.DataSupplierToolStripMenuItem.ToolTipText = "Tampilkan Data Supplier";
            this.DataSupplierToolStripMenuItem.Click += new System.EventHandler(this.DataSupplierToolStripMenuItem_Click);
            // 
            // DataPelangganToolStripMenuItem
            // 
            this.DataPelangganToolStripMenuItem.Name = "DataPelangganToolStripMenuItem";
            this.DataPelangganToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.DataPelangganToolStripMenuItem.Text = "DATA PELANGGAN";
            this.DataPelangganToolStripMenuItem.ToolTipText = "Tampilkan Data Pelanggan";
            this.DataPelangganToolStripMenuItem.Click += new System.EventHandler(this.DataPelangganToolStripMenuItem_Click);
            // 
            // DataKasirToolStripMenuItem
            // 
            this.DataKasirToolStripMenuItem.Name = "DataKasirToolStripMenuItem";
            this.DataKasirToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.DataKasirToolStripMenuItem.Text = "DATA KASIR";
            this.DataKasirToolStripMenuItem.Click += new System.EventHandler(this.DataKasirToolStripMenuItem_Click);
            // 
            // DataKasMasterToolStripMenuItem
            // 
            this.DataKasMasterToolStripMenuItem.Name = "DataKasMasterToolStripMenuItem";
            this.DataKasMasterToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.DataKasMasterToolStripMenuItem.Text = "DATA KAS";
            this.DataKasMasterToolStripMenuItem.Click += new System.EventHandler(this.KasMasterToolStripMenuItem_Click);
            // 
            // tRANSAKSIToolStripMenuItem
            // 
            this.tRANSAKSIToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PenjualanToolStripMenuItem,
            this.PembelianToolStripMenuItem});
            this.tRANSAKSIToolStripMenuItem.Name = "tRANSAKSIToolStripMenuItem";
            this.tRANSAKSIToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.tRANSAKSIToolStripMenuItem.Text = "TRANSAKSI";
            // 
            // PenjualanToolStripMenuItem
            // 
            this.PenjualanToolStripMenuItem.Name = "PenjualanToolStripMenuItem";
            this.PenjualanToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.PenjualanToolStripMenuItem.Text = "PENJUALAN";
            this.PenjualanToolStripMenuItem.Click += new System.EventHandler(this.PenjualanToolStripMenuItem_Click);
            // 
            // PembelianToolStripMenuItem
            // 
            this.PembelianToolStripMenuItem.Name = "PembelianToolStripMenuItem";
            this.PembelianToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.PembelianToolStripMenuItem.Text = "PEMBELIAN";
            this.PembelianToolStripMenuItem.Click += new System.EventHandler(this.PembelianToolStripMenuItem_Click);
            // 
            // lAPORANToolStripMenuItem
            // 
            this.lAPORANToolStripMenuItem.Name = "lAPORANToolStripMenuItem";
            this.lAPORANToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.lAPORANToolStripMenuItem.Text = "LAPORAN";
            this.lAPORANToolStripMenuItem.Click += new System.EventHandler(this.lAPORANToolStripMenuItem_Click);
            // 
            // TokoBukuWindows
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 371);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "TokoBukuWindows";
            this.Text = "TokoBukuWindows";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.TokoBukuWindows_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem iNPUTDATAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DataBarangToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DataSupplierToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DataPelangganToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DataKasirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tRANSAKSIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PenjualanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PembelianToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lAPORANToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripMenuItem DataKasMasterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem KategoriBukuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PenerbitBukuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rAKBUKUToolStripMenuItem;
    }
}