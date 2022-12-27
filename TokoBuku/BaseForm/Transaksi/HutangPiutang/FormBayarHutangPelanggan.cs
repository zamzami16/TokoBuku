using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TokoBuku.DbUtility;

namespace TokoBuku.BaseForm.Transaksi.HutangPiutang
{
    public partial class FormBayarHutangPelanggan : Form
    {
        public int IdPelanggan { get; set; }
        public string NamaPelanggan { get; set; }
        public double TotalHutang { get; set; }
        private DataTable dataHutang;
        private DataTable DataKas;

        public FormBayarHutangPelanggan()
        {
            InitializeComponent();
        }


        private void buttonBatal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormBayarHutang_Load(object sender, EventArgs e)
        {
            this.dataHutang = TokoBuku.DbUtility.Transactions.HutangPiutang.BayarHutangPelanggan.DataHutangPelanggan(7); // id_pelanggan = 7
            this.DgvListHutang.DataSource = this.dataHutang;

            this.DgvListHutang.Columns[0].Visible = false;
            this.DgvListHutang.Columns[6].Visible = false;
            this.DgvListHutang.Columns[7].Visible = false;
            for (int i = 1; i < 5; i++)
            {
                this.DgvListHutang.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            this.DgvListHutang.Columns[1].HeaderText = "Kasir";
            this.DgvListHutang.Columns[2].HeaderText = "Tenggat Bayar";
            this.DgvListHutang.Columns[3].HeaderText = "DP";
            this.DgvListHutang.Columns[4].HeaderText = "Total";
            this.DgvListHutang.Columns[5].HeaderText = "Keterangan Pembelian";

            this.DgvListHutang.Columns[3].DefaultCellStyle.Format = "C";
            this.DgvListHutang.Columns[4].DefaultCellStyle.Format = "C";
            this.DgvListHutang.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.DgvListHutang.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.DgvListHutang.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


            /// Combobox No Transaksi
            this.comboBoxNoTransaksi.DataSource = this.GetKodeTransaksi();
            this.comboBoxNoTransaksi.ValueMember = "id_penjualan";
            this.comboBoxNoTransaksi.DisplayMember = "kode_transaksi";
            this.comboBoxNoTransaksi.SelectedIndex = 0;
            this.comboBoxNoTransaksi.DropDownStyle = ComboBoxStyle.DropDownList;

            /// combo box kas
            this.RefreshDataKas();
        }

        private void buttonBayar_Click(object sender, EventArgs e)
        {
            /// TODO: cek catatan
            double total_bayar;
            if (!double.TryParse(this.textBoxTotal.Text, out total_bayar))
            {
                MessageBox.Show("Cek total pembayaran dulu.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.ActiveControl = this.textBoxTotal;
            }
            else if(Convert.ToDouble(this.textBoxKembalian.Text.Replace("Rp","")) < 0)
            {
                MessageBox.Show("Kembalian tidak boleh negatif. Cek total pembayaran dulu.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.ActiveControl = this.textBoxTotal;
            }
            else if (Convert.ToInt32(this.comboBoxNoTransaksi.SelectedValue.ToString()) == -1 )
            { /// perform untuk pembayaran semua
                
            }
            else
            { /// perform untuk masing2 pembayaran
                double pembayaran = total_bayar - Convert.ToDouble(this.textBoxKembalian.Text.Replace("Rp", ""));
                int id_pelanggan = this.IdPelanggan;
                int id_penjualan = Convert.ToInt32(this.comboBoxNoTransaksi.SelectedValue.ToString());
                try
                {
                    TokoBuku.DbUtility.Transactions.HutangPiutang
                        .BayarHutangPelanggan.BayarHutang(id_pelanggan: id_pelanggan,
                        id_penjualan: id_penjualan, tanggal_bayar: DateTime.Now, pembayaran_awal: pembayaran);
                    MessageBox.Show("Pembayaran berhasil");
                    this.Close();
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                
            }
        }

        private DataTable GetKodeTransaksi()
        {
            DataTable data= new DataTable();
            data.Columns.Add("id_penjualan", typeof(int));
            data.Columns.Add("kode_transaksi", typeof(string));
            DataRow drow = data.NewRow();
            drow["id_penjualan"] = -1;
            drow["kode_transaksi"] = "Semua";
            data.Rows.Add(drow);
            DataTable data1 = this.dataHutang.Copy();
            data1.Columns.RemoveAt(0);
            data1.Columns.RemoveAt(0);
            data1.Columns.RemoveAt(0);
            data1.Columns.RemoveAt(0);
            data1.Columns.RemoveAt(0);
            data1.Columns.RemoveAt(0);
            data.Merge(data1);
            return data;
        }

        private void comboBoxNoTransaksi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxNoTransaksi.SelectedIndex != 0)
            {
                int ids = Convert.ToInt32(this.comboBoxNoTransaksi.SelectedValue.ToString());
                DataView dv = new DataView(this.dataHutang);
                dv.RowFilter = $"[ID_PENJUALAN]={ids}";
                this.DgvListHutang.DataSource = dv.ToTable();
            }
            else
            {
                this.DgvListHutang.DataSource = this.dataHutang;
            }
        }

        private void DgvListHutang_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            double total_ = DgvListHutang.Rows.Cast<DataGridViewRow>()
                    .Sum(t => Convert.ToDouble(t.Cells["total"].Value));
            this.textBoxTotal.Text = total_.ToString("c");
            //this.textBoxNominalBayar.Text = Convert.ToDouble(this.textBoxTotal.Text.Replace("Rp", "")).ToString();
        }
        private void RefreshDataKas()
        {
            this.DataKas = DbSearchLoadData.Kas();
            this.comboBoxJenisKas.DataSource = this.DataKas;
            this.comboBoxJenisKas.DisplayMember = "NAMA";
            this.comboBoxJenisKas.ValueMember = "ID";
            this.comboBoxJenisKas.SelectedIndex = 0;
        }

        private void textBoxNominalBayar_TextChanged(object sender, EventArgs e)
        {
            /// TODO: tambahkan update kembalian
        }
    }
}
