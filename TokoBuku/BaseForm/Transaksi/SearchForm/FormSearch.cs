using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using TokoBuku.BaseForm.Master.Input;
using TokoBuku.DbUtility;

namespace TokoBuku.BaseForm.Transaksi.SearchForm
{
    public partial class FormSearch : Form
    {
        public string SearchText { get; set; }
        public string SearchedText { get; set; }
        public string SearchedKode { get; set; }
        public int SearchIndex { get; set; }
        public string FormName { get; set; }
        private DataTable data;

        public FormSearch()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormSearch_Load(object sender, EventArgs e)
        {
            if (this.FormName == "pelanggan")
            {
                this.Text = "Cari Pelanggan";
                this.labelTitle.Text = "Cari Pelanggan";
                this.textSearch.Text = this.SearchText;
                try
                {
                    this.data = DbSearchLoadData.Pelanggan();
                    var da = new DataView(this.data);
                    da.RowFilter = "[NAMA] LIKE '%" + this.SearchText + "%'";
                    this.dataGridView1.DataSource = da.ToTable();
                    this.dataGridView1.Columns[0].Visible = false;
                    this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //throw ex;
                }
            }
            else if (this.FormName == "kode")
            {
                this.Text = "Cari Kode Barang";
                this.labelTitle.Text = "Cari Kode Barang";
                this.textSearch.Text = this.SearchText;
                try
                {
                    this.data = DbSearchLoadData.Barang();
                    var da = new DataView(this.data);
                    da.RowFilter = "[KODE] LIKE '%" + this.SearchText + "%'";
                    this.dataGridView1.DataSource = da.ToTable();
                    this.dataGridView1.Columns[0].Visible = false;
                    this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    this.dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    this.dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    this.dataGridView1.Columns[1].FillWeight = 100;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //throw ex;
                }
            }
            else if (this.FormName == "barang")
            {
                this.Text = "Cari Barang";
                this.labelTitle.Text = "Cari Barang";
                this.textSearch.Text = this.SearchText;
                try
                {
                    this.data = DbSearchLoadData.Barang();
                    var da = new DataView(this.data);
                    da.RowFilter = "[NAMA_BARANG] LIKE '%" + this.SearchText + "%'";
                    this.dataGridView1.DataSource = da.ToTable();
                    this.dataGridView1.Columns[0].Visible = false;
                    this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    this.dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    this.dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    this.dataGridView1.Columns[1].FillWeight = 100;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //throw ex;
                }
            }
            else if (this.FormName == "supplier")
            {
                this.Text = "Cari Supplier";
                this.labelTitle.Text = "Cari Supplier";
                this.textSearch.Text = this.SearchText;
                try
                {
                    this.data = DbSearchLoadData.Supplier();
                    var da = new DataView(this.data);
                    da.RowFilter = "[NAMA] LIKE '%" + this.SearchText + "%'";
                    this.dataGridView1.DataSource = da.ToTable();
                    this.dataGridView1.Columns[0].Visible = false;
                    this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //throw;
                }
            }
        }

        private void buttonPilih_Click(object sender, EventArgs e)
        {
            var idx = this.dataGridView1.SelectedCells[0].RowIndex;
            if (idx != -1)
            {
                this.SearchIndex = Convert.ToInt32(this.dataGridView1.Rows[idx].Cells[0].Value.ToString());
                this.SearchedText = this.dataGridView1.Rows[idx].Cells[1].Value.ToString();
                if (this.FormName == "barang" || this.FormName == "kode")
                {
                    this.SearchedKode = this.dataGridView1.Rows[idx].Cells[2].Value.ToString();
                }
                this.DialogResult = DialogResult.OK;
                //this.Close();
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var idx = this.dataGridView1.SelectedCells[0].RowIndex;
                if (idx != -1)
                {
                    this.SearchIndex = Convert.ToInt32(this.dataGridView1.Rows[idx].Cells[0].Value.ToString());
                    this.SearchedText = this.dataGridView1.Rows[idx].Cells[1].Value.ToString();
                    if (this.FormName == "barang" || this.FormName == "kode")
                    {
                        this.SearchedKode = this.dataGridView1.Rows[idx].Cells[2].Value.ToString();
                    }
                    this.DialogResult = DialogResult.OK;
                    //this.Close();
                }
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (this.FormName == "pelanggan")
            {
                try
                {
                    this.data = DbSearchLoadData.Pelanggan();
                    var da = new DataView(this.data);
                    da.RowFilter = "[NAMA] LIKE '%" + this.textSearch.Text.Trim() + "%'";
                    this.dataGridView1.DataSource = da.ToTable();
                    this.dataGridView1.Columns[0].Visible = false;
                    this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //throw ex;
                }
            }
            else if (this.FormName == "kode")
            {
                try
                {
                    this.data = DbSearchLoadData.Barang();
                    var da = new DataView(this.data);
                    da.RowFilter = "[KODE] LIKE '%" + this.textSearch.Text.Trim() + "%'";
                    this.dataGridView1.DataSource = da.ToTable();
                    this.dataGridView1.Columns[0].Visible = false;
                    //this.dataGridView1.Columns[1].Visible = false;
                    this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //throw ex;
                }
            }
            else if (this.FormName == "barang")
            {
                try
                {
                    this.data = DbSearchLoadData.Barang();
                    var da = new DataView(this.data);
                    da.RowFilter = "[NAMA_BARANG] LIKE '%" + this.textSearch.Text.Trim() + "%'";
                    this.dataGridView1.DataSource = da.ToTable();
                    this.dataGridView1.Columns[0].Visible = false;
                    //this.dataGridView1.Columns[2].Visible = false;
                    this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ButtAdd_Click(object sender, EventArgs e)
        {
            if (this.FormName == "pelanggan")
            {
                this.AddPelanggan();
            }
            else if (this.FormName == "kode" || this.FormName == "barang")
            {
                this.AddBarang();
            }
        }

        private void AddPelanggan()
        {
            using (var form = FormInput.Pelanggan())
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    var nama = form.inputNama;
                    var alamat = form.inputALamat;
                    var no_hp = form.inputNoHP.Replace("-", "").Replace("(", "").Replace(")", "").Replace("+", "").Replace(" ", "");
                    var email = form.inputEmail;
                    var keterangan = form.inputKeterangan;
                    var status = form.inputStatus;
                    int ids;
                    try
                    {
                        ids = DbSaveData.Pelanggan(nama: nama, alamat: alamat, email: email,
                        no_hp: no_hp, keterangan: keterangan, status: status);

                        this.data = DbSearchLoadData.Pelanggan();
                        this.dataGridView1.DataSource = this.data;

                        MessageBox.Show("Data Berhasil disimpan.", "Success.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }

        private void AddBarang()
        {
            using (var form = new FormInputDataBarang())
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    var kategori = Convert.ToInt32(form.Kategori);
                    var penerbit = Convert.ToInt32(form.Penerbit);
                    var rak = Convert.ToInt32(form.Rak);
                    var kode = form.KodeBarang;
                    var namaBarang = form.NamaBarang;
                    var stock = form.Stock;
                    double hargaBeli = form.HargaBeli;
                    double harga = form.Harga;
                    var isbn = form.ISBN;
                    var penulis = form.Penulis;
                    double diskon = form.Diskon;
                    var status = form.Status;
                    var barCode = form.BarCode;
                    var keterangan = form.Keterangan;

                    try
                    {
                        int ids = DbSaveData.Barang(inIdKategori: kategori, inIdPenerbit: penerbit,
                            inIdRak: rak, inKode: kode, inNama: namaBarang, inStock: stock, inHargaBeli: hargaBeli,
                            inHarga: harga, inIsbn: isbn, inPenulis: penulis, inDiskon: diskon,
                            inStatus: status, inBarCode: barCode, inKeterngan: keterangan);

                        this.data = DbSearchLoadData.Barang();
                        this.dataGridView1.DataSource = this.data;

                        MessageBox.Show("Data Berhasil disimpan.", "Success.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void textSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.buttonSearch_Click(sender, e);
            }
        }

        /// Function key
        private void FormSearch_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    this.buttonSearch_Click(sender, e);
                    break;
                case Keys.F6:
                    this.buttonPilih_Click(sender, e);
                    break;
                case Keys.F7:
                    this.buttonCancel_Click(sender, e);
                    break;
                case Keys.F8:
                    this.ButtAdd_Click(sender, e);
                    break;
            }
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();

            var centerFormat = new StringFormat()
            {
                // right alignment might actually make more sense for numbers
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }
    }
}
