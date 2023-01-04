using System;
using System.Data;
using System.Windows.Forms;
using TokoBuku.BaseForm.Master;
using TokoBuku.BaseForm.Transaksi;

namespace TokoBuku
{
    public partial class TokoBukuWindows : Form
    {
        private Penjualan formPenjualanView;
        private Pembelian formPembelianView;
        private FormMasterViewKategori formKategoriView;
        private FormMasterViewBarang formBarangView;
        private FormMasterViewPenerbit formPenerbitView;
        private FormMasterViewRak formRakView;
        private FormMasterViewSupplier formSupplierView;
        private FormMasterViewPelanggan formPelangganView;
        private FormMasterViewKasir formKasirView;
        private FormMasterViewKasMaster formKasView;
        private FormUbahHarga formUbahHarga;
        private int IdKasir { get; set; }
        private string NamaKasir { set; get; }
        private bool IsAdmin { get; set; }


        public TokoBukuWindows() { InitializeComponent(); }

        private void TokoBukuWindows_Load(object sender, EventArgs e)
        {
            if (this.IsAdmin)
            {
                this.DataKasirToolStripMenuItem.Enabled = true;
            }
            else
            {
                this.DataKasirToolStripMenuItem.Enabled = false;
            }
            this.formPenjualanView = new Penjualan(this.IdKasir, this.NamaKasir);
            this.formPenjualanView.MdiParent = this;
            this.formPenjualanView.MdiParent.LayoutMdi(MdiLayout.TileHorizontal);
            this.formPenjualanView.Dock = DockStyle.Fill;
            this.formPenjualanView.Show();
        }

        private void DataBarangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //AddDataTable();
            this.formBarangView = new FormMasterViewBarang();
            this.formBarangView.MdiParent = this;
            this.formBarangView.MdiParent.LayoutMdi(MdiLayout.TileHorizontal);
            this.formBarangView.Dock = DockStyle.Fill;
            this.formBarangView.Show();
        }

        private void DataRakToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.formRakView = new FormMasterViewRak();
            this.formRakView.MdiParent = this;
            this.formRakView.MdiParent.LayoutMdi(MdiLayout.TileHorizontal);
            this.formRakView.Dock = DockStyle.Fill;
            this.formRakView.Show();
        }

        private void DataKategoriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.formKategoriView = new FormMasterViewKategori();
            this.formKategoriView.MdiParent = this;
            this.formKategoriView.MdiParent.LayoutMdi(MdiLayout.TileHorizontal);
            this.formKategoriView.Dock = DockStyle.Fill;
            this.formKategoriView.Show();
        }

        private void KasMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.formKasView = new FormMasterViewKasMaster();
            this.formKasView.MdiParent = this;
            this.formKasView.MdiParent.LayoutMdi(MdiLayout.TileHorizontal);
            this.formKasView.Dock = DockStyle.Fill;
            this.formKasView.Show();
        }

        /// <summary>
        /// TODO: Tambah fitur laporan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lAPORANToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("MENU LAPORAN MASIH DALAM PENGEMBANGAN", "Information.", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DataKasirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.formKasirView = new FormMasterViewKasir();
            this.formKasirView.MdiParent = this;
            this.formKasirView.MdiParent.LayoutMdi(MdiLayout.TileHorizontal);
            this.formKasirView.Dock = DockStyle.Fill;
            this.formKasirView.Show();
        }

        private void DataPenerbitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.formPenerbitView = new FormMasterViewPenerbit();
            this.formPenerbitView.MdiParent = this;
            this.formPenerbitView.MdiParent.LayoutMdi(MdiLayout.TileHorizontal);
            this.formPenerbitView.Dock = DockStyle.Fill;
            this.formPenerbitView.Show();

        }

        private void DataSupplierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.formSupplierView = new FormMasterViewSupplier();
            this.formSupplierView.MdiParent = this;
            this.formSupplierView.MdiParent.LayoutMdi(MdiLayout.TileHorizontal);
            this.formSupplierView.Dock = DockStyle.Fill;
            this.formSupplierView.Show();

        }

        private void DataPelangganToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.formPelangganView = new FormMasterViewPelanggan();
            this.formPelangganView.MdiParent = this;
            this.formPelangganView.MdiParent.LayoutMdi(MdiLayout.TileHorizontal);
            this.formPelangganView.Dock = DockStyle.Fill;
            this.formPelangganView.Show();

        }

        /// <summary>
        /// Menu Transaksi Penjualan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PenjualanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.formPenjualanView = new Penjualan(this.IdKasir, this.NamaKasir);
            this.formPenjualanView.MdiParent = this;
            this.formPenjualanView.MdiParent.LayoutMdi(MdiLayout.TileHorizontal);
            this.formPenjualanView.Dock = DockStyle.Fill;
            this.formPenjualanView.Show();
        }

        private void PembelianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.formPembelianView = new Pembelian();
            this.formPembelianView.MdiParent = this;
            this.formPembelianView.MdiParent.LayoutMdi(MdiLayout.TileHorizontal);
            this.formPembelianView.Dock = DockStyle.Fill;
            this.formPembelianView.Show();
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

        private void uBAHHARGAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.formUbahHarga = new FormUbahHarga();
            this.formUbahHarga.MdiParent = this;
            this.formUbahHarga.MdiParent.LayoutMdi(MdiLayout.TileHorizontal);
            this.formUbahHarga.Dock = DockStyle.Fill;
            this.formUbahHarga.Show();
        }

        private void rESETDATABASEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var x_ = MessageBox.Show("Semua data di Database (kecuali data kasir) akan dihapus. Anda yakin?", "Warning.", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (x_ == DialogResult.Yes) { TokoBuku.DbUtility.Database.ResetDatabase(); }
        }

        public void SetKasirTerpilih(int idKasir, string namaKasir)
        {
            this.IdKasir = idKasir;
            this.NamaKasir = namaKasir;
        }

        public void SetAdmin(bool isAdmin)
        {
            this.IsAdmin = isAdmin;
        }

        private void pembelianToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // TODO: Cetak nota penjualan
        }

        private void pembelianToolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }
    }
}
