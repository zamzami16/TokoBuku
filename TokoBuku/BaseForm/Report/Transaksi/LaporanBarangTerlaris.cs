using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Windows.Forms;

namespace TokoBuku.BaseForm.Report.Transaksi
{
    public partial class LaporanBarangTerlaris : Form
    {
        public LaporanBarangTerlaris()
        {
            InitializeComponent();
        }

        private void LaporanBarangTerlaris_Load(object sender, EventArgs e)
        {
            this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            this.reportViewer1.ZoomMode = ZoomMode.Percent;
            this.reportViewer1.ZoomPercent = 100;

            this.SetThisMonth();
            this.UpdateData();
            this.reportViewer1.RefreshReport();
        }

        private void SetThisMonth()
        {
            var today = DateTime.Today;
            var first = new DateTime(today.Year, today.Month, 1);
            var month = first.AddMonths(1);
            var last = month.AddDays(-1);

            this.dtpDari.Value = first;
            this.dtpSampai.Value = last;
        }
        private void UpdateData()
        {
            int top_ = (int)this.nud.Value;
            DateTime dateDari = this.dtpDari.Value;
            DateTime dateSampai = this.dtpSampai.Value;
            DataTable data = new DataTable();
            data = DbUtility.Report.Transaksi.BarangTerlaris.Get(top_, dateDari, dateSampai);
            ReportDataSource dataSource = new ReportDataSource()
            {
                Name = "DataSetBarangTerlaris",
                Value = data
            };
            ReportParameter[] parameters = new ReportParameter[2];
            parameters[0] = new ReportParameter("dateDari", dateDari.ToString());
            parameters[1] = new ReportParameter("dateSampai", dateSampai.ToString());
            this.reportViewer1.LocalReport.SetParameters(parameters);
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(dataSource);
            this.reportViewer1.RefreshReport();
        }

        private void btnTerapkan_Click(object sender, EventArgs e)
        {
            this.UpdateData();
        }

        private void cbThisMonth_CheckedChanged(object sender, EventArgs e)
        {
            this.dtpDari.Enabled = this.dtpSampai.Enabled = !this.cbThisMonth.Checked;
            if (this.cbThisMonth.Checked)
            {
                this.SetThisMonth();
            }
        }
    }
}
