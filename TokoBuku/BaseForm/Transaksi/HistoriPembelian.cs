using Microsoft.Reporting.WinForms;
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
        private DataTable data { get; set; }
        private DataTable TempData = new DataTable();
        public HistoriPembelian()
        {
            InitializeComponent();
            this.data = DbUtility.Transactions.Pembelian.HistoriPembelian();
            this.TempData = this.data;
        }

        private void HistoriPembelian_Load(object sender, EventArgs e)
        {
            this.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
            this.reportViewer1.ZoomPercent = 100;

            this.RefreshDataTable();
            this.reportViewer1.RefreshReport();
        }

        private void RefreshDataTable()
        {
            this.UpdateComboSupplier();
            this.UpdateMinMaxDate();
            this.UpdateTipePembayaran();
            this.UpdateJenisKas();
            this.UpdateDataReport();
        }

        private void UpdateDataReport()
        {
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource = new Microsoft.Reporting.WinForms.ReportDataSource()
            {
                Name = "DataSetReportPembelian",
                Value = this.TempData
            };

            ReportParameter[] parameters = new ReportParameter[2];
            parameters[0] = new ReportParameter("DateMulai", this.dateTimePickerDari.Value.ToString());
            parameters[1] = new ReportParameter("DateSampai", this.dateTimePickerSampai.Value.ToString());
            this.reportViewer1.LocalReport.SetParameters(parameters);
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource);
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
                this.UpdateDataReport();
            }
        }
    }
}
