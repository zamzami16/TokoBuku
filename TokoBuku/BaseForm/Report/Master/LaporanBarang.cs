using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Windows.Forms;

namespace TokoBuku.BaseForm.Report.Master
{
    public partial class LaporanBarang : Form
    {
        private DataTable DataStockBarang { get; set; }
        public LaporanBarang()
        {
            InitializeComponent();
            this.RefreshDataStockBarang();
        }

        private void LaporanBarang_Load(object sender, EventArgs e)
        {
            this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            this.reportViewer1.ZoomMode = ZoomMode.Percent;
            this.reportViewer1.ZoomPercent = 100;

            ReportDataSource dataSource = new ReportDataSource()
            {
                Name = "DataSetLaporanBarang",
                Value = this.DataStockBarang
            };
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(dataSource);
            this.reportViewer1.RefreshReport();
        }

        private void RefreshDataStockBarang()
        {
            this.DataStockBarang = DbUtility.Report.Master.LaporanBarang.Get();
        }
    }
}
