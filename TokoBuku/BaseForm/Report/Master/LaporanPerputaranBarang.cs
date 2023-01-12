using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Windows.Forms;

namespace TokoBuku.BaseForm.Report.Master
{
    public partial class LaporanPerputaranBarang : Form
    {
        private DataTable data = new DataTable();
        private DataTable tempData = new DataTable();
        public LaporanPerputaranBarang()
        {
            InitializeComponent();
        }

        private void HistoriPembelian_Load(object sender, EventArgs e)
        {
            this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            this.reportViewer1.ZoomMode = ZoomMode.Percent;
            this.reportViewer1.ZoomPercent = 100;

            this.InitLaporanPerputaranBarang();
            this.reportViewer1.RefreshReport();
        }

        private void InitLaporanPerputaranBarang()
        {
            var today = DateTime.Today;
            var first = new DateTime(today.Year, today.Month, 1);
            var month = first.AddMonths(1);
            var last = month.AddDays(-1);

            this.dateTimePickerDari.Value = first;
            this.dateTimePickerSampai.Value = last;
            this.UpdatePerputaranBarang();
        }

        private void UpdatePerputaranBarang()
        {
            DateTime first = this.dateTimePickerDari.Value;
            DateTime last = this.dateTimePickerSampai.Value;
            if (first > last)
            {
                MessageBox.Show("Rentang tanggal tidak valid.", "Error", MessageBoxButtons.OK);
            }
            else
            {
                this.tempData = DbUtility.Report.Transaksi.PerputaranBarang.GetPerputaranBarang(first, last);
                ReportDataSource dataSource = new ReportDataSource()
                {
                    Name = "DataSetPerputaranBarang",
                    Value = this.tempData
                };
                ReportParameter[] parameters = new ReportParameter[2];
                parameters[0] = new ReportParameter("dateDari", first.ToString());
                parameters[1] = new ReportParameter("dateSampai", last.ToString());
                this.reportViewer1.LocalReport.SetParameters(parameters);
                this.reportViewer1.LocalReport.DataSources.Clear();
                this.reportViewer1.LocalReport.DataSources.Add(dataSource);
                this.reportViewer1.RefreshReport();
            }
        }

        /* private void UpdateMinMaxDate()
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
         }*/
        private void buttonTerapkan_Click(object sender, EventArgs e)
        {
            this.UpdatePerputaranBarang();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.dateTimePickerDari.Enabled = this.dateTimePickerSampai.Enabled = !this.checkBox1.Checked;
            if (this.checkBox1.Checked)
            {
                var today = DateTime.Today;
                var first = new DateTime(today.Year, today.Month, 1);
                var month = first.AddMonths(1);
                var last = month.AddDays(-1);

                this.dateTimePickerDari.Value = first;
                this.dateTimePickerSampai.Value = last;
            }
        }
    }
}
