using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using TokoBuku.BaseForm.Master.Input;
using TokoBuku.BaseForm.TipeData.DataBase;
using TokoBuku.DbUtility;

namespace TokoBuku.BaseForm.Transaksi.SearchForm
{
    public partial class FormSearchSupplierPelanggan : Form
    {
        public string SearchText { get; set; }
        public string SearchedText { get; set; }
        public int SearchIndex { get; set; }
        public string FormName { get; set; }

        public TPelanggan Pelanggan { get; set; }
        public TSupplier Supplier { get; set; }

        private DataTable data;

        public FormSearchSupplierPelanggan()
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
                this.Supplier = new TSupplier();
                this.Text = "Cari Pelanggan";
                this.labelTitle.Text = "Cari Pelanggan";
                this.textSearch.Text = this.SearchText;
                try
                {
                    this.data = DbSearchLoadData.Pelanggan();
                    var da = new DataView(this.data);
                    da.RowFilter = "[NAMA] LIKE '%" + this.SearchText.Trim().Replace("'", "''") + "%'";
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
            else if (this.FormName == "supplier")
            {
                this.Supplier = new TSupplier();
                this.Text = "Cari Supplier";
                this.labelTitle.Text = "Cari Supplier";
                this.textSearch.Text = this.SearchText;
                try
                {
                    this.data = DbSearchLoadData.Supplier();
                    var da = new DataView(this.data);
                    da.RowFilter = "[NAMA] LIKE '%" + this.SearchText.Trim().Replace("'", "''") + "%'";
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
                if (this.FormName == "pelanggan")
                {
                    this.Pelanggan.Id = Convert.ToInt32(this.dataGridView1.Rows[idx].Cells[0].Value.ToString());
                    this.Pelanggan.Nama = this.dataGridView1.Rows[idx].Cells[1].Value.ToString();
                }
                else if (this.FormName == "supplier")
                {
                    this.Supplier.Id = Convert.ToInt32(this.dataGridView1.Rows[idx].Cells[0].Value.ToString());
                    this.Supplier.Nama = this.dataGridView1.Rows[idx].Cells[1].Value.ToString();
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
                    da.RowFilter = "[NAMA] LIKE '%" + this.textSearch.Text.Trim().Replace("'", "''") + "%'";
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
            else if (this.FormName == "supplier")
            {
                try
                {
                    this.data = DbSearchLoadData.Supplier();
                    var da = new DataView(this.data);
                    da.RowFilter = "[NAMA] LIKE '%" + this.textSearch.Text.Trim().Replace("'", "''") + "%'";
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

        }

        private void ButtAdd_Click(object sender, EventArgs e)
        {
            if (this.FormName == "pelanggan")
            {
                this.AddPelanggan();
            }
            else if (this.FormName == "supplier")
            {
                this.AddSupplier();
            }
        }

        private void AddSupplier()
        {
            using (var form = FormInput.Supplier())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    var supplier = form.Supplier;
                    int ids;
                    try
                    {
                        ids = DbUtility.Master.Supplier.SaveSupplier(supplier);
                        supplier.Id = ids;
                        this.Supplier = supplier;
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        throw;
                    }
                }
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
