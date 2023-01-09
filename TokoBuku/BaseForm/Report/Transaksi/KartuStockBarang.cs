using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace TokoBuku.BaseForm.Report.Transaksi
{
    public partial class KartuStockBarang : Form
    {
        private DataTable DataTable { get; set; }
        private DataTable TempData { get; set; }
        public KartuStockBarang()
        {
            InitializeComponent();
        }

        private void KartuStockBarang_Load(object sender, EventArgs e)
        {
            this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            this.reportViewer1.ZoomMode = ZoomMode.Percent;
            this.reportViewer1.ZoomPercent= 100;
            this.ActiveControl = this.btnTerapkan;
            this.GetData();
            this.UpdateMinMaxDate();
            this.UpdateDataSource();
            this.reportViewer1.RefreshReport();
        }

        private void GetData()
        {
            DataTable _masuk = DbUtility.Report.Transaksi.GetKartuStock.GetMasuk();
            DataTable _keluar = DbUtility.Report.Transaksi.GetKartuStock.GetKeluar();
            _masuk.Merge(_keluar);
            DataView dv = new DataView(_masuk);
            dv.Sort = "TANGGAL ASC";
            this.DataTable = dv.ToTable();
            this.TempData = this.DataTable;
            _masuk.Dispose();
            _keluar.Dispose();
        }

        private void UpdateDataSource()
        {
            ReportParameter[] reportParameters = new ReportParameter[2];
            reportParameters[0] = new ReportParameter("DateMulai", this.dtpMulai.Value.ToString());
            reportParameters[1] = new ReportParameter("DateSampai", this.dtpSampai.Value.ToString());
            this.reportViewer1.LocalReport.SetParameters(reportParameters); 
            ReportDataSource dataSource = new ReportDataSource()
            {
                Name= "KartuStock",
                Value=this.TempData
            };
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(dataSource);
            this.reportViewer1.RefreshReport();
        }

        private void RefreshData()
        {
            DateTime dateMulai = dtpMulai.Value;
            DateTime dateSampai = dtpSampai.Value;
            if (dateMulai > dateSampai)
            {
                MessageBox.Show("Tanggal mulai tidak boleh lebih besar dari tanggal sampai.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.ActiveControl = this.dtpMulai;
            }
            else if (this.dtpMulai.Enabled)
            {
                string _exp = $"tanggal >= #{dateMulai.ToString("yyyy-MM-dd")}# and tanggal <= #{dateSampai.ToString("yyyy-MM-dd")}#";
                DataView dv = new DataView(this.TempData);
                dv.RowFilter = _exp;
                dv.Sort = "TANGGAL ASC";
                this.TempData = dv.ToTable();
            }
            else { this.TempData = this.DataTable; }
            this.UpdateDataSource();
        }

        private void chBTgl_CheckedChanged(object sender, EventArgs e)
        {
            this.dtpMulai.Enabled = this.dtpSampai.Enabled = !this.chBTgl.Checked;
        }
        private void UpdateMinMaxDate()
        {
            var first = this.DataTable.AsEnumerable()
               .Select(cols => cols.Field<DateTime>("tanggal"))
               .OrderBy(p => p.Ticks)
               .FirstOrDefault();

            var last = this.DataTable.AsEnumerable()
                          .Select(cols => cols.Field<DateTime>("tanggal"))
                          .OrderByDescending(p => p.Ticks)
                          .FirstOrDefault();
            this.dtpMulai.Value = first;
            this.dtpSampai.Value = last;
        }

        private void btnTerapkan_Click(object sender, EventArgs e)
        {
            this.RefreshData();
        }
    }
}
