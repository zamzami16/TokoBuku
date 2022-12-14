using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TokoBuku.Transaksi;

namespace TokoBuku
{
    public partial class TokoBukuWindows : Form
    {
        public FormMasterDataViewer formDataBarangView, formKasirView, formKategoriView, 
            formPelangganView, formPenerbitView, formRakView, formSupplierView;
        public Penjualan PenjualanView;
        public TokoBukuWindows()
        {
            InitializeComponent();
        }

        private void TokoBukuWindows_Load(object sender, EventArgs e)
        {
            this.formDataBarangView = new FormMasterDataViewer();
            this.formDataBarangView.SetJenisForm("barang");
            this.formDataBarangView.MdiParent = this;
            this.formDataBarangView.MdiParent.LayoutMdi(MdiLayout.TileHorizontal);
            this.formDataBarangView.Dock = DockStyle.Fill;
            this.formDataBarangView.Show();
        }

        private void DataBarangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //AddDataTable();
            this.formDataBarangView = new FormMasterDataViewer();
            this.formDataBarangView.SetJenisForm("barang");
            this.formDataBarangView.MdiParent = this;
            this.formDataBarangView.MdiParent.LayoutMdi(MdiLayout.TileHorizontal);
            this.formDataBarangView.Dock = DockStyle.Fill;
            this.formDataBarangView.Show();
        }

        private void DataRakToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.formRakView = new FormMasterDataViewer();
            this.formRakView.SetJenisForm("rak");
            this.formRakView.MdiParent = this;
            this.formRakView.MdiParent.LayoutMdi(MdiLayout.TileHorizontal);
            this.formRakView.Dock = DockStyle.Fill;
            this.formRakView.Show();
        }

        private void DataKategoriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.formKategoriView = new FormMasterDataViewer();
            this.formKategoriView.SetJenisForm("kategori");
            this.formKategoriView.MdiParent = this;
            this.formKategoriView.MdiParent.LayoutMdi(MdiLayout.TileHorizontal);
            this.formKategoriView.Dock = DockStyle.Fill;
            this.formKategoriView.Show();
        }

        private void dATAKASMASTERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.formKategoriView = new FormMasterDataViewer();
            this.formKategoriView.SetJenisForm("kas");
            this.formKategoriView.MdiParent = this;
            this.formKategoriView.MdiParent.LayoutMdi(MdiLayout.TileHorizontal);
            this.formKategoriView.Dock = DockStyle.Fill;
            this.formKategoriView.Show();
        }

        private void pENJUALANToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.PenjualanView = new Penjualan();
            this.PenjualanView.MdiParent = this;
            this.PenjualanView.MdiParent.LayoutMdi(MdiLayout.Cascade);
            this.PenjualanView.Dock = DockStyle.Fill;
            this.PenjualanView.Show();
        }

        private void pEMBELIANToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("DATA PEMBELIAN MASIH DALAM PENGEMBANGAN", "Information.", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void lAPORANToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("MENU LAPORAN MASIH DALAM PENGEMBANGAN", "Information.", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DataKasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.formDataBarangView = new FormMasterDataViewer();
            this.formDataBarangView.SetJenisForm("kasir");
            this.formDataBarangView.MdiParent = this;
            this.formDataBarangView.MdiParent.LayoutMdi(MdiLayout.TileHorizontal);
            this.formDataBarangView.Dock = DockStyle.Fill;
            this.formDataBarangView.Show();
        }

        private void DataKasirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.formKasirView = new FormMasterDataViewer();
            this.formKasirView.SetJenisForm("kasir");
            this.formKasirView.MdiParent = this;
            this.formKasirView.MdiParent.LayoutMdi(MdiLayout.TileHorizontal);
            this.formKasirView.Dock = DockStyle.Fill;
            this.formKasirView.Show();
        }

        private void DataPenerbitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.formPenerbitView = new FormMasterDataViewer();
            this.formPenerbitView.SetJenisForm("penerbit");
            this.formPenerbitView.MdiParent = this;
            this.formPenerbitView.MdiParent.LayoutMdi(MdiLayout.TileHorizontal);
            this.formPenerbitView.Dock = DockStyle.Fill;
            this.formPenerbitView.Show();

        }

        private void DataSupplierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.formSupplierView = new FormMasterDataViewer();
            this.formSupplierView.SetJenisForm("supplier");
            this.formSupplierView.MdiParent = this;
            this.formSupplierView.MdiParent.LayoutMdi(MdiLayout.TileHorizontal);
            this.formSupplierView.Dock = DockStyle.Fill;
            this.formSupplierView.Show();

        }

        private void DataPelangganToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.formPelangganView = new FormMasterDataViewer();
            this.formPelangganView.SetJenisForm("pelanggan");
            this.formPelangganView.MdiParent = this;
            this.formPelangganView.MdiParent.LayoutMdi(MdiLayout.TileHorizontal);
            this.formPelangganView.Dock = DockStyle.Fill;
            this.formPelangganView.Show();

        }

        private void AddDataTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Nama");
            dt.Columns.Add("Alamat");

            for (int i = 0; i < 30; i++)
            {
                DataRow drow = dt.NewRow();
                drow["Nama"] = $"Andi ke-{i}";
                drow["Alamat"] = $"Mangunan ke-{i}";
                dt.Rows.Add(drow);
            }
            //this.formMasterView.SetDataGridSource(dt);
        }
    }
}
