using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace TokoBuku.BaseForm.Report.HutangPiutang
{
    public partial class LaporanHutang : Form
    {
        private DataTable DataHutang { get; set; }
        private DataTable FilteredDataHutang { get; set; }
        private DateTime DefaultDateMulai { get; set; }
        private DateTime DefaultDateSampai { get; set; }

        public LaporanHutang()
        {
            InitializeComponent();
            this.DataHutang = DbUtility.Report.HutangPiutang.GetHutangPiutang.GetHutang();
        }

        private void LaporanLabaRugi_Load(object sender, EventArgs e)
        {
            this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            this.reportViewer1.ZoomMode = ZoomMode.Percent;
            this.reportViewer1.ZoomPercent = 100;

            this.FilteredDataHutang = this.DataHutang;

            var first = this.FilteredDataHutang.AsEnumerable()
               .Select(cols => cols.Field<DateTime>("tenggat"))
               .OrderBy(p => p.Ticks)
               .FirstOrDefault();

            var last = this.FilteredDataHutang.AsEnumerable()
                          .Select(cols => cols.Field<DateTime>("tenggat"))
                          .OrderByDescending(p => p.Ticks)
                          .FirstOrDefault();

            this.dtpMulai.Value = first;
            this.dtpSampai.Value = last;
            this.DefaultDateMulai = this.dtpMulai.Value;
            this.DefaultDateSampai = this.dtpSampai.Value;

            this.UpdateData();
            this.ActiveControl = this.buttonTerapkan;
            this.UpdateDataSource(first, last);
            this.dtpMulai.Enabled = this.dtpSampai.Enabled = !this.checkBoxSemauTanggal.Checked;
        }

        private void UpdateDataSource(DateTime mulai, DateTime sampai)
        {
            ReportDataSource dt = new ReportDataSource() { Value = this.FilteredDataHutang, Name = "DataSetRiwayatHutang" };
            ReportParameter[] parameter = new ReportParameter[2];
            parameter[0] = new ReportParameter("ReportParameterDateMulai", mulai.ToString());
            parameter[1] = new ReportParameter("ReportParameterDateSampai", sampai.ToString());
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(dt);
            //this.reportViewer1.LocalReport.SetParameters(parameter);
            this.reportViewer1.RefreshReport();
        }

        private void buttonTerapkan_Click(object sender, EventArgs e)
        {
            if (this.checkBoxSemauTanggal.Checked && !this.checkBoxBelumLunasSaja.Checked)
            {
                this.TampilkanSemua();
                return;
            }
            else
            this.UpdateData();
        }

        private void TampilkanSemua()
        {
            this.FilteredDataHutang = this.DataHutang;
            this.UpdateDataSource(this.DefaultDateMulai, this.DefaultDateSampai);
        }
        private void UpdateData()
        {
            DateTime mulai = dtpMulai.Value.Date;
            DateTime sampai = dtpSampai.Value.Date;

            if (sampai < mulai) { MessageBox.Show("Rentang tanggal tidak valid.\nData ditampilkan semua."); this.TampilkanSemua(); }
            else
            {
                string _exp = "";
                DataView dv = new DataView(this.DataHutang);
                if (!this.checkBoxSemauTanggal.Checked)
                {
                    _exp += $"tenggat >= #{mulai:yyyy-MM-dd}# and tenggat <= #{sampai:yyyy-MM-dd}#";
                }
                if (this.checkBoxBelumLunasSaja.Checked)
                {
                    _exp += (_exp.Length > 0) ? " and sudah_lunas='Belum'" : "sudah_lunas='Belum'";
                }
                dv.RowFilter = _exp;
                this.FilteredDataHutang = dv.ToTable();
                this.UpdateDataSource(mulai, sampai);
            }
        }

        private void checkBoxSemauTanggal_CheckedChanged(object sender, EventArgs e)
        {
            this.dtpMulai.Enabled = this.dtpSampai.Enabled = !this.checkBoxSemauTanggal.Checked;
        }
    }
}
