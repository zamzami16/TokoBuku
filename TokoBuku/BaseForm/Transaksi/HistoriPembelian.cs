using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TokoBuku.BaseForm.Report.Transaksi;
using TokoBuku.BaseForm.TipeData.DataBase;

namespace TokoBuku.BaseForm.Transaksi
{
    public partial class HistoriPembelian : Form
    {
        private DataTable data = new DataTable();
        private DataTable TempData = new DataTable();
        public HistoriPembelian()
        {
            InitializeComponent();
        }

        private void HistoriPembelian_Load(object sender, EventArgs e)
        {
            this.RefreshDataTable();
        }

        private void RefreshDataTable()
        {
            this.data = TokoBuku.DbUtility.Transactions.Pembelian.HistoriPembelian();
            this.dgv.DataSource = this.data;
            this.dgv.Columns[0].Visible = false;
            this.dgv.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dgv.Columns[1].MinimumWidth = 125;
            for (int i = 2; i < this.dgv.ColumnCount; i++)
            {
                this.dgv.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            this.dgv.Columns["total"].DefaultCellStyle.Format = "C";
            this.dgv.Columns["total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            this.UpdateComboSupplier();
            this.UpdateMinMaxDate();
            this.UpdateTipePembayaran();
            this.UpdateJenisKas();
            this.TempData = this.data;
        }

        private void UpdateJenisKas()
        {
            DataView _temp_ = new DataView(this.data);
            DataTable _temp = _temp_.ToTable(true, "kas");
            this.comboBoxKas.Items.Add("Semua");
            foreach (DataRow _dr in _temp.Rows)
            {
                string measurement = _dr.Field<String>("kas");
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

        private void UpdateComboSupplier()
        {
            DataView _temp_ = new DataView(this.data);
            DataTable _temp = _temp_.ToTable(true, "suplier");
            DataTable supplier = this.data.Clone();
            DataRow row = supplier.NewRow();
            row["suplier"] = "Semua";
            supplier.Rows.Add(row);
            supplier.Merge(_temp);
            _temp.Dispose();
            this.comboBoxSupplier.DataSource = supplier;
            this.comboBoxSupplier.ValueMember = "suplier";
            this.comboBoxSupplier.DisplayMember = "suplier";
        }

        private void UpdateMinMaxDate()
        {
            var first = this.data.AsEnumerable()
               .Select(cols => cols.Field<DateTime>("tanggal_beli"))
               .OrderBy(p => p.Ticks)
               .FirstOrDefault();

            var last = this.data.AsEnumerable()
                          .Select(cols => cols.Field<DateTime>("tanggal_beli"))
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
            this.TempData = this.data.Copy();
            var sup = this.comboBoxSupplier.Text.Replace("'", "''");
            var dDari = this.dateTimePickerDari.Value;
            var dSampai = this.dateTimePickerSampai.Value;
            var tBayar = this.comboBoxTipeBayar.Text.Replace("'", "''");
            var kas = this.comboBoxKas.Text.Replace("'", "''");
            string exp_ = "";
            if (dSampai < dDari)
            {
                MessageBox.Show("Rentang tanggal tidak valid.", "Warning.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.RefreshDataTable();
            }
            else
            {
                if (sup.ToLower() != "semua")
                {
                    exp_ += $"suplier = '{sup}' ";
                }
                if (tBayar.ToLower() != "semua")
                {
                    exp_ += (exp_.Length > 0) ? $"and pembayaran = '{tBayar}' " : $"pembayaran = '{tBayar}' ";
                }
                if (kas.ToLower() != "semua")
                {
                    exp_ += (exp_.Length > 0) ? $"and kas = '{kas}' " : $"kas = '{kas}' ";
                }
                exp_ += (exp_.Length > 0) ? "and " : "";
                exp_ += $"tanggal_beli >= #{dDari.ToString("yyyy-MM-dd")}# and tanggal_beli <= #{dSampai.ToString("yyyy-MM-dd")}#";
                DataView dv = new DataView(TempData);
                dv.RowFilter = exp_;
                TempData = dv.ToTable();
            }
            this.dgv.DataSource = TempData;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var form = new LaporanDataPembelian())
            {
                form.data = this.TempData;
                form.ShowDialog();
            }
        }
    }
}
