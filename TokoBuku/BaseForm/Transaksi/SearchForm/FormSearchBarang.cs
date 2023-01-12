using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using TokoBuku.BaseForm.Master.Input;
using TokoBuku.BaseForm.TipeData.Search;
using TokoBuku.DbUtility;

namespace TokoBuku.BaseForm.Transaksi.SearchForm
{
    public partial class FormSearchBarang : Form
    {
        public string SearchText { get; set; }
        public string FormName { get; set; }

        public string TipeForm { get; set; }
        public TSearchBarang SearchBarang { get; set; }
        private DataTable data;

        public FormSearchBarang()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormSearch_Load(object sender, EventArgs e)
        {
            this.textSearch.Text = this.SearchText;
            this.SearchBarang = new TSearchBarang();
            // Load and filter data
            this.FilterDataBarang();
        }

        private void FilterDataBarang()
        {
            try
            {
                // Load data barang
                this.data = DbSearchLoadData.Barang();
                // create filter
                this.SearchText = this.textSearch.Text.ToString();
                var filterer = this.SearchText.Trim().Replace("\'", "");
                var da = new DataView(this.data);
                da.RowFilter = "[KODE] LIKE '%" + filterer + "%'" + "OR [NAMA_BARANG] LIKE '%" + filterer + "%'";
                this.dataGridView1.DataSource = da.ToTable();
                this.dataGridView1.Columns[0].Visible = false;
                this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                this.dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                this.dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                //
                this.dataGridView1.Columns["harga_jual"].DefaultCellStyle.Format = "C";
                this.dataGridView1.Columns["harga_beli"].DefaultCellStyle.Format = "C";
                this.dataGridView1.Columns["harga_beli"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dataGridView1.Columns["harga_jual"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                if (TipeForm == "penjualan")
                {
                    this.dataGridView1.Columns["harga_beli"].Visible = false;
                }
                else if (TipeForm == "pembelian")
                {
                    this.dataGridView1.Columns["harga_jual"].Visible = false;
                }
                this.dataGridView1.Columns[1].FillWeight = 100;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //throw ex;
            }
        }

        private void buttonPilih_Click(object sender, EventArgs e)
        {
            var idx = this.dataGridView1.SelectedCells[0].RowIndex;
            if (idx != -1)
            {
                this.SearchBarang.Id = Convert.ToInt32(this.dataGridView1.Rows[idx].Cells["id_barang"].Value.ToString());
                this.SearchBarang.Nama = this.dataGridView1.Rows[idx].Cells["nama_barang"].Value.ToString();
                this.SearchBarang.Kode = this.dataGridView1.Rows[idx].Cells["kode"].Value.ToString();
                this.SearchBarang.HargaBeli = Convert.ToDouble(this.dataGridView1.Rows[idx].Cells["harga_beli"].Value.ToString());
                this.SearchBarang.HargaJual = Convert.ToDouble(this.dataGridView1.Rows[idx].Cells["harga_jual"].Value.ToString());

                this.DialogResult = DialogResult.OK;
                //this.Close();
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.buttonPilih_Click(sender, e);
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e) { this.FilterDataBarang(); }

        private void ButtAdd_Click(object sender, EventArgs e) { this.AddBarang(); }

        private void AddBarang()
        {
            using (var form = new FormInputDataBarang())
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    var barang = form.DbBarang;

                    try
                    {
                        int ids = DbUtility.Master.Barang.SaveBarang(barang);
                        this.RefreshDataBarang();

                        MessageBox.Show("Data Berhasil disimpan.", "Success.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void RefreshDataBarang()
        {
            this.data = DbSearchLoadData.Barang();
            this.dataGridView1.DataSource = this.data;
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

        public void HideAddBarang()
        {
            this.ButtAdd.Enabled = false;
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
