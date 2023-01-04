using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TokoBuku.BaseForm.Report.Transaksi;
using TokoBuku.BaseForm.TipeData.DataBase;

namespace TokoBuku.BaseForm.Transaksi
{
    public partial class HistoriPenjualan : Form
    {
        private DataTable data = new DataTable();
        private DataTable tempData = new DataTable();
        public DataTable dataTable { get { return tempData; } }
        public HistoriPenjualan()
        {
            InitializeComponent();
        }
        /// TODO: tambahkan filter
        private void HistoriPembelian_Load(object sender, EventArgs e)
        {
            this.RefreshDataPenjualan();
        }

        private void RefreshDataPenjualan()
        {
            this.data = TokoBuku.DbUtility.Transactions.Penjualan.GetHistoriPenjualan();
            this.dgv.DataSource = this.data;
            this.dgv.Columns[0].Visible = false;
            for (int i = 1; i < this.dgv.ColumnCount; i++)
            {
                this.dgv.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            this.dgv.Columns["keterangan"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dgv.Columns[1].HeaderText = "No Transaksi";
            this.dgv.Columns[2].HeaderText = "kasir";
            this.dgv.Columns[3].HeaderText = "pelanggan";
            this.dgv.Columns[4].HeaderText = "total";
            this.dgv.Columns[5].HeaderText = "tanggal";
            this.dgv.Columns[6].HeaderText = "pembayaran";
            this.dgv.Columns[7].HeaderText = "kas";

            this.dgv.Columns[4].DefaultCellStyle.Format = "c";
            this.dgv.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            this.tempData = this.data;

            this.UpdateMinMaxDate();
            this.UpdateTipePembayaran();
            this.UpdateJenisKas();
            this.UpdateComboKasir();
        }

        private void UpdateComboKasir()
        {
            DataView _temp_ = new DataView(this.data);
            DataTable _temp = _temp_.ToTable(true, "nama_kasir");
            DataTable supplier = this.data.Clone();
            DataRow row = supplier.NewRow();
            row["nama_kasir"] = "Semua";
            supplier.Rows.Add(row);
            supplier.Merge(_temp);
            _temp.Dispose();
            this.comboBoxKasir.DataSource = supplier;
            this.comboBoxKasir.ValueMember = "nama_kasir";
            this.comboBoxKasir.DisplayMember = "nama_kasir";
        }

        private void UpdateJenisKas()
        {
            DataView _temp_ = new DataView(this.data);
            DataTable _temp = _temp_.ToTable(true, "nama_kas");
            this.comboBoxKas.Items.Add("Semua");
            foreach (DataRow _dr in _temp.Rows)
            {
                string measurement = _dr.Field<String>("nama_kas");
                if (measurement != null && measurement.Trim() != "")
                    this.comboBoxKas.Items.Add(measurement);
            }
            this.comboBoxKas.SelectedIndex = 0;
            _temp_.Dispose();
            _temp.Dispose();
        }

        private void UpdateTipePembayaran()
        {
            this.comboBoxTipeBayar.Items.Add("Semua");
            foreach (var item in Enum.GetValues(typeof(TJenisPembayaran)))
            {
                this.comboBoxTipeBayar.Items.Add(item);
            }
            this.comboBoxTipeBayar.SelectedIndex = 0;
        }

        private void UpdateMinMaxDate()
        {
            var first = this.data.AsEnumerable()
               .Select(cols => cols.Field<DateTime>("tanggal"))
               .OrderBy(p => p.Ticks)
               .FirstOrDefault();

            var last = this.data.AsEnumerable()
                          .Select(cols => cols.Field<DateTime>("tanggal"))
                          .OrderByDescending(p => p.Ticks)
                          .FirstOrDefault();
            this.dateTimePickerDari.Value = first;
            this.dateTimePickerSampai.Value = last;
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

        private void buttonTerapkan_Click(object sender, EventArgs e)
        {
            this.tempData = this.data.Copy();
            var kasir = this.comboBoxKasir.Text.Replace("'", "''");
            var dDari = this.dateTimePickerDari.Value;
            var dSampai = this.dateTimePickerSampai.Value;
            var tBayar = this.comboBoxTipeBayar.Text.Replace("'", "''");
            var kas = this.comboBoxKas.Text.Replace("'", "''");
            string exp_ = "";
            if (dSampai < dDari)
            {
                MessageBox.Show("Rentang tanggal tidak valid.", "Warning.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.RefreshDataPenjualan();
            }
            else
            {
                if (kasir.ToLower() != "semua")
                {
                    exp_ += $"nama_kasir = '{kasir}' ";
                }
                if (tBayar.ToLower() != "semua")
                {
                    exp_ += (exp_.Length > 0) ? $"and pembayaran = '{tBayar}' " : $"pembayaran = '{tBayar}' ";
                }
                if (kas.ToLower() != "semua")
                {
                    exp_ += (exp_.Length > 0) ? $"and nama_kas = '{kas}' " : $"nama_kas = '{kas}' ";
                }
                exp_ += (exp_.Length > 0) ? "and " : "";
                exp_ += $"tanggal >= #{dDari.ToString("yyyy-MM-dd")}# and tanggal <= #{dSampai.ToString("yyyy-MM-dd")}#";
                DataView dv = new DataView(this.tempData);
                dv.RowFilter = exp_;
                this.tempData = dv.ToTable();
            }
            this.dgv.DataSource = this.tempData;
        }

        private void buttonBuatLaporan_Click(object sender, EventArgs e)
        {
            using (var form = new LaporanDataPenjualan())
            {
                form.data = this.dataTable;
                form.ShowDialog();
            }
        }
    }
}
