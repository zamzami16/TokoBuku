using System;
using System.Data;
using System.Windows.Forms;

namespace TokoBuku.BaseForm.Report.Transaksi
{
    public partial class LaporanDataPembelian : Form
    {
        public DataTable data { get; set; }
        public LaporanDataPembelian()
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
                Name = "DataSetReportPembelian",
                Value = this.data
            };
            this.reportDataPelanggan.LocalReport.DataSources.Clear();
            this.reportDataPelanggan.LocalReport.DataSources.Add(reportDataSource);
            this.reportDataPelanggan.RefreshReport();
            // TODO: Pembelian. Jadikan 1 saja dengan report viewer
            // TODO: Pembelian. [Tgl | No Transaksi | Pelanggan | Pembayaran | Total | Kasir]
            // TODO: Pembelian. Rev: [Header] => Nama PT. [Body] => Judul + Periode tanggal
        }
    }
}
