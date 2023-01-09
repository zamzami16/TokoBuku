using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TokoBuku.BaseForm.Report.Transaksi
{
    public partial class LaporanLabaRugi : Form
    {
        private DataTable DataLabaRugi { get; set; }
        private DataTable FilteredDataLabaRugi { get; set; }
        public LaporanLabaRugi()
        {
            InitializeComponent();
            this.DataLabaRugi = TokoBuku.DbUtility.Report.Transaksi.GetLabaRugi.Get();
        }

        private void LaporanLabaRugi_Load(object sender, EventArgs e)
        {
            this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            this.reportViewer1.ZoomMode = ZoomMode.Percent;
            this.reportViewer1.ZoomPercent = 100;

            var first = this.DataLabaRugi.AsEnumerable()
               .Select(cols => cols.Field<DateTime>("tanggal"))
               .OrderBy(p => p.Ticks)
               .FirstOrDefault();

            var last = this.DataLabaRugi.AsEnumerable()
                          .Select(cols => cols.Field<DateTime>("tanggal"))
                          .OrderByDescending(p => p.Ticks)
                          .FirstOrDefault();

            this.dtpMulai.Value = first;
            this.dtpSampai.Value = last;
            this.ActiveControl = this.buttonTerapkan;
            this.FilteredDataLabaRugi = this.DataLabaRugi;
            this.UpdateDataSource(first, last);
        }

        private void UpdateDataSource(DateTime mulai, DateTime sampai)
        {
            ReportDataSource dt = new ReportDataSource() { Value = this.FilteredDataLabaRugi, Name = "DataSetLabaRugi" };
            ReportParameter[] parameter = new ReportParameter[2];
            parameter[0] = new ReportParameter("ReportParameterDateMulai", mulai.ToString());
            parameter[1] = new ReportParameter("ReportParameterDateSampai", sampai.ToString());
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(dt);
            this.reportViewer1.LocalReport.SetParameters(parameter);
            this.reportViewer1.RefreshReport();
        }

        private void buttonTerapkan_Click(object sender, EventArgs e)
        {
            this.UpdateData();
        }
        private void UpdateData()
        {
            DateTime mulai = dtpMulai.Value.Date;
            DateTime sampai = dtpSampai.Value.Date;

            if (sampai < mulai) { MessageBox.Show("Rentang tanggal tidak valid."); }
            else
            {
                DataView dv = new DataView(this.DataLabaRugi);
                string _exp = $"tanggal >= #{mulai.ToString("yyyy-MM-dd")}# and " +
                    $"tanggal <= #{sampai.ToString("yyyy-MM-dd")}#";
                dv.RowFilter = _exp;
                this.FilteredDataLabaRugi = dv.ToTable();
                this.UpdateDataSource(mulai, sampai);
            }
        }

        // TODO: Total Potongan dan Diskon disendirikan
        // TODO: Gaperlu dimerah

    }
}
