using System;
using System.Data;
using System.Windows.Forms;

namespace TokoBuku.BaseForm.Report.Transaksi
{
    public partial class LaporanDataPenjualan : Form
    {
        public DataTable data { get; set; }
        public LaporanDataPenjualan()
        {
            InitializeComponent();
        }

        private void LaporanDataPenjualan_Load(object sender, EventArgs e)
        {
            this.reportDataPelanggan.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            this.reportDataPelanggan.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
            this.reportDataPelanggan.ZoomPercent = 100;

            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource = new Microsoft.Reporting.WinForms.ReportDataSource()
            {
                Name = "DataSetReport",
                Value = this.data
            };
            this.reportDataPelanggan.LocalReport.DataSources.Clear();
            this.reportDataPelanggan.LocalReport.DataSources.Add(reportDataSource);
            this.reportDataPelanggan.RefreshReport();
        }
    }
}
