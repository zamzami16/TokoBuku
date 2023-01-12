
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
            this.RakBukuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DataSupplierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DataPelangganToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DataKasirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DataKasMasterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tRANSAKSIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PenjualanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PembelianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uBAHHARGAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lAPORANToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cetakNotaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pembelianToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.labaRugiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hutangToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hutangToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.piutangToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.barangToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kartuStockToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.perputaranBarangToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dATABASEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rESETDATABASEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.contextMenu1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iNPUTDATAToolStripMenuItem,
            this.tRANSAKSIToolStripMenuItem,
            this.lAPORANToolStripMenuItem,
            this.dATABASEToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(437, 27);
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
            this.iNPUTDATAToolStripMenuItem.Size = new System.Drawing.Size(83, 23);
            this.iNPUTDATAToolStripMenuItem.Text = "INPUT DATA";
            this.iNPUTDATAToolStripMenuItem.ToolTipText = "Tampilkan Data";
            // 
            // DataBarangToolStripMenuItem
            // 
            this.DataBarangToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.KategoriBukuToolStripMenuItem,
            this.PenerbitBukuToolStripMenuItem,
            this.RakBukuToolStripMenuItem});
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
            // RakBukuToolStripMenuItem
            // 
            this.RakBukuToolStripMenuItem.Name = "RakBukuToolStripMenuItem";
            this.RakBukuToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.RakBukuToolStripMenuItem.Text = "RAK BUKU";
            this.RakBukuToolStripMenuItem.Click += new System.EventHandler(this.DataRakToolStripMenuItem_Click);
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
            this.DataKasirToolStripMenuItem.ToolTipText = "Manage Kasir [Only Admin]";
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
            this.PembelianToolStripMenuItem,
            this.uBAHHARGAToolStripMenuItem});
            this.tRANSAKSIToolStripMenuItem.Name = "tRANSAKSIToolStripMenuItem";
            this.tRANSAKSIToolStripMenuItem.Size = new System.Drawing.Size(79, 23);
            this.tRANSAKSIToolStripMenuItem.Text = "TRANSAKSI";
            // 
            // PenjualanToolStripMenuItem
            // 
            this.PenjualanToolStripMenuItem.Name = "PenjualanToolStripMenuItem";
            this.PenjualanToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.PenjualanToolStripMenuItem.Text = "PENJUALAN";
            this.PenjualanToolStripMenuItem.Click += new System.EventHandler(this.PenjualanToolStripMenuItem_Click);
            // 
            // PembelianToolStripMenuItem
            // 
            this.PembelianToolStripMenuItem.Name = "PembelianToolStripMenuItem";
            this.PembelianToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.PembelianToolStripMenuItem.Text = "PEMBELIAN";
            this.PembelianToolStripMenuItem.Click += new System.EventHandler(this.PembelianToolStripMenuItem_Click);
            // 
            // uBAHHARGAToolStripMenuItem
            // 
            this.uBAHHARGAToolStripMenuItem.Name = "uBAHHARGAToolStripMenuItem";
            this.uBAHHARGAToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.uBAHHARGAToolStripMenuItem.Text = "UBAH HARGA";
            this.uBAHHARGAToolStripMenuItem.Click += new System.EventHandler(this.uBAHHARGAToolStripMenuItem_Click);
            // 
            // lAPORANToolStripMenuItem
            // 
            this.lAPORANToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cetakNotaToolStripMenuItem,
            this.pembelianToolStripMenuItem3,
            this.labaRugiToolStripMenuItem,
            this.hutangToolStripMenuItem,
            this.barangToolStripMenuItem});
            this.lAPORANToolStripMenuItem.Name = "lAPORANToolStripMenuItem";
            this.lAPORANToolStripMenuItem.Size = new System.Drawing.Size(73, 23);
            this.lAPORANToolStripMenuItem.Text = "LAPORAN";
            this.lAPORANToolStripMenuItem.Click += new System.EventHandler(this.lAPORANToolStripMenuItem_Click);
            // 
            // cetakNotaToolStripMenuItem
            // 
            this.cetakNotaToolStripMenuItem.Name = "cetakNotaToolStripMenuItem";
            this.cetakNotaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.cetakNotaToolStripMenuItem.Text = "Penjualan";
            this.cetakNotaToolStripMenuItem.Click += new System.EventHandler(this.historyPenjualanStripMenuItem_Click);
            // 
            // pembelianToolStripMenuItem3
            // 
            this.pembelianToolStripMenuItem3.Name = "pembelianToolStripMenuItem3";
            this.pembelianToolStripMenuItem3.Size = new System.Drawing.Size(180, 22);
            this.pembelianToolStripMenuItem3.Text = "Pembelian";
            this.pembelianToolStripMenuItem3.Click += new System.EventHandler(this.pembelianToolStripMenuItem3_Click);
            // 
            // labaRugiToolStripMenuItem
            // 
            this.labaRugiToolStripMenuItem.Name = "labaRugiToolStripMenuItem";
            this.labaRugiToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.labaRugiToolStripMenuItem.Text = "Laba Rugi";
            this.labaRugiToolStripMenuItem.Click += new System.EventHandler(this.labaRugiToolStripMenuItem_Click);
            // 
            // hutangToolStripMenuItem
            // 
            this.hutangToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hutangToolStripMenuItem1,
            this.piutangToolStripMenuItem});
            this.hutangToolStripMenuItem.Name = "hutangToolStripMenuItem";
            this.hutangToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.hutangToolStripMenuItem.Text = "Hutang Piutang";
            // 
            // hutangToolStripMenuItem1
            // 
            this.hutangToolStripMenuItem1.Name = "hutangToolStripMenuItem1";
            this.hutangToolStripMenuItem1.Size = new System.Drawing.Size(115, 22);
            this.hutangToolStripMenuItem1.Text = "Hutang";
            this.hutangToolStripMenuItem1.Click += new System.EventHandler(this.hutangToolStripMenuItem1_Click);
            // 
            // piutangToolStripMenuItem
            // 
            this.piutangToolStripMenuItem.Name = "piutangToolStripMenuItem";
            this.piutangToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.piutangToolStripMenuItem.Text = "Piutang";
            this.piutangToolStripMenuItem.Click += new System.EventHandler(this.piutangToolStripMenuItem_Click);
            // 
            // barangToolStripMenuItem
            // 
            this.barangToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kartuStockToolStripMenuItem,
            this.perputaranBarangToolStripMenuItem});
            this.barangToolStripMenuItem.Name = "barangToolStripMenuItem";
            this.barangToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.barangToolStripMenuItem.Text = "Barang";
            // 
            // kartuStockToolStripMenuItem
            // 
            this.kartuStockToolStripMenuItem.Name = "kartuStockToolStripMenuItem";
            this.kartuStockToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.kartuStockToolStripMenuItem.Text = "Kartu Stock";
            this.kartuStockToolStripMenuItem.Click += new System.EventHandler(this.kartuStockToolStripMenuItem_Click);
            // 
            // perputaranBarangToolStripMenuItem
            // 
            this.perputaranBarangToolStripMenuItem.Name = "perputaranBarangToolStripMenuItem";
            this.perputaranBarangToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.perputaranBarangToolStripMenuItem.Text = "Perputaran Barang";
            this.perputaranBarangToolStripMenuItem.Click += new System.EventHandler(this.perputaranBarangToolStripMenuItem_Click);
            // 
            // dATABASEToolStripMenuItem
            // 
            this.dATABASEToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rESETDATABASEToolStripMenuItem});
            this.dATABASEToolStripMenuItem.Name = "dATABASEToolStripMenuItem";
            this.dATABASEToolStripMenuItem.Size = new System.Drawing.Size(74, 23);
            this.dATABASEToolStripMenuItem.Text = "DATABASE";
            // 
            // rESETDATABASEToolStripMenuItem
            // 
            this.rESETDATABASEToolStripMenuItem.Name = "rESETDATABASEToolStripMenuItem";
            this.rESETDATABASEToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.rESETDATABASEToolStripMenuItem.Text = "RESET DATABASE";
            this.rESETDATABASEToolStripMenuItem.ToolTipText = "Reset Database [Only Admin]";
            this.rESETDATABASEToolStripMenuItem.Click += new System.EventHandler(this.rESETDATABASEToolStripMenuItem_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 647F));
            this.tableLayoutPanel1.Controls.Add(this.menuStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.button1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(964, 27);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.ContextMenuStrip = this.contextMenu1;
            this.button1.Dock = System.Windows.Forms.DockStyle.Right;
            this.button1.Image = global::TokoBuku.Properties.Resources.user;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(1015, 0);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(69, 27);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // contextMenu1
            // 
            this.contextMenu1.Name = "contextMenu1";
            this.contextMenu1.Size = new System.Drawing.Size(61, 4);
            this.contextMenu1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenu1_Opening);
            // 
            // TokoBukuWindows
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 561);
            this.Controls.Add(this.tableLayoutPanel1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(980, 600);
            this.Name = "TokoBukuWindows";
            this.Text = "TokoBukuWindows";
            this.toolTip1.SetToolTip(this, "Logout");
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.TokoBukuWindows_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.ToolStripMenuItem RakBukuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uBAHHARGAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dATABASEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rESETDATABASEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cetakNotaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pembelianToolStripMenuItem3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ContextMenuStrip contextMenu1;
        private System.Windows.Forms.ToolStripMenuItem labaRugiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hutangToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem barangToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hutangToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem piutangToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kartuStockToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem perputaranBarangToolStripMenuItem;
    }
}