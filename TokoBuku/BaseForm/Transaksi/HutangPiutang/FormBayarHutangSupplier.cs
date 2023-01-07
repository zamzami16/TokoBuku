using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using TokoBuku.BaseForm.TipeData.DataBase;
using TokoBuku.DbUtility;

namespace TokoBuku.BaseForm.Transaksi.HutangPiutang
{
    public partial class FormBayarHutangSupplier : Form
    {
        public int IdSupplier { get; set; }
        public string NamaSupplier { get; set; }
        public double TotalHutang { get; set; }
        private DataTable dataHutang;
        private DataTable DataKas;

        public FormBayarHutangSupplier() { InitializeComponent(); }

        private void buttonBatal_Click(object sender, EventArgs e) { this.Close(); }

        private void FormBayarHutang_Load(object sender, EventArgs e)
        {
            /// assign value
            this.textBoxNamaSupplier.Text = this.NamaSupplier;

            this.RefreshDataHutang();

            /// Combobox No Transaksi
            this.comboBoxNoNota.DataSource = this.GetKodeTransaksi();
            this.comboBoxNoNota.ValueMember = "id_pembelian";
            this.comboBoxNoNota.DisplayMember = "nota_pembelian";
            this.comboBoxNoNota.SelectedIndex = 0;
            this.comboBoxNoNota.DropDownStyle = ComboBoxStyle.DropDownList;

            /// combo box kas
            this.RefreshDataKas();
        }

        private void RefreshDataHutang()
        {
            this.dataHutang = TokoBuku.DbUtility.Transactions.HutangPiutang.BayarHutangKeSupplier
                .DataHutangKeSupplier(this.IdSupplier); // id_supplier
            this.DgvListHutang.DataSource = this.dataHutang;
            this.DgvListHutang.Columns["id"].Visible = false;
            this.DgvListHutang.Columns["id_supplier"].Visible = false;
            this.DgvListHutang.Columns["id_pembelian"].Visible = false;

            for (int i = 0; i < this.DgvListHutang.ColumnCount; i++)
            {
                this.DgvListHutang.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            this.DgvListHutang.Columns["nama_supplier"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.DgvListHutang.Columns["nama_supplier"].MinimumWidth = 75;
            this.DgvListHutang.Columns["nama_supplier"].HeaderText = "Supplier";

            this.DgvListHutang.Columns["total_hutang"].DefaultCellStyle.Format = "C";
            this.DgvListHutang.Columns["sudah_dibayar"].DefaultCellStyle.Format = "C";
            this.DgvListHutang.Columns["belum_bayar"].DefaultCellStyle.Format = "C";

            this.DgvListHutang.Columns["total_hutang"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.DgvListHutang.Columns["sudah_dibayar"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.DgvListHutang.Columns["belum_bayar"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void buttonBayar_Click(object sender, EventArgs e)
        {
            
            double total_bayar;
            if (!double.TryParse(this.textBoxNominalBayar.Text, out total_bayar))
            {
                MessageBox.Show("Cek total pembayaran dulu.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.ActiveControl = this.textBoxTotal;
            }
            else if (Convert.ToInt32(this.comboBoxNoNota.SelectedValue.ToString()) == -1 && this.DgvListHutang.RowCount > 1)
            {/// perform untuk pembayaran semua. Syarat harus lunas semuanya, jika tidak, lanjutkan ke bembayaran tiap item
                if ((total_bayar - Convert.ToDouble(this.textBoxTotal.Text.Replace("Rp", ""))) >= 0)
                {/// uang pembayaran lunas
                    try
                    {
                        foreach (DataGridViewRow row in this.DgvListHutang.Rows)
                        {

                            TBayarHutang bayarHutang = new TBayarHutang();
                            bayarHutang.IdHutang = Convert.ToInt32(row.Cells["id_hutang"].Value.ToString());
                            bayarHutang.Pembayaran = Convert.ToDouble(this.DgvListHutang.Rows[0].Cells["belum_bayar"].Value.ToString());
                            bayarHutang.TglBayar = dateTimePicker1.Value;
                            bayarHutang.IdKas = Convert.ToInt32(this.comboBoxJenisKas.SelectedValue.ToString());
                            bayarHutang.isDP = TIsDP.bukan;
                            DbUtility.Transactions.HutangPiutang.BayarHutangKeSupplier
                                .BayarHutang(bayarHutang, TLunas.Lunas);
                        }
                        MessageBox.Show("Pembayaran Hutang Berhasil.", "Succcess.");
                        this.RefreshDataHutang();
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Pembayaran hutang Error.\nErrCode: " + ex.Message, "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        throw ex;
                    }
                }
                else
                {/// uang pembayaran kurang
                    MessageBox.Show("Pembayaran semua hanya untuk pembayaran lunas. Jika mencicil, silakan lewat pembayaran per item.", "Warning.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.ActiveControl = this.comboBoxNoNota;
                }
            }
            else
            { 
                TLunas lunas = TLunas.Belum;TBayarHutang bayarHutang = new TBayarHutang();
                bayarHutang.IdHutang = Convert.ToInt32(this.DgvListHutang.Rows[0].Cells["id"].Value.ToString());
                bayarHutang.Pembayaran = total_bayar;
                bayarHutang.TglBayar = dateTimePicker1.Value;
                bayarHutang.IdKas = Convert.ToInt32(this.comboBoxJenisKas.SelectedValue.ToString());
                bayarHutang.isDP = TIsDP.bukan;

                if (bayarHutang.Pembayaran - Convert.ToDouble(this.textBoxTotal.Text.Replace("Rp", "")) >= 0)
                {
                    lunas = TLunas.Lunas;
                }
                if (bayarHutang.Pembayaran > 0)
                {
                    try
                    {
                        DbUtility.Transactions.HutangPiutang.BayarHutangKeSupplier
                            .BayarHutang(bayarHutang, lunas: lunas);
                        MessageBox.Show("Pembayaran berhasil");
                        this.RefreshDataHutang();
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        throw ex;
                    }
                }
                else { MessageBox.Show("Cek nominal pembayaran dulu.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); }

            }
        }

        private DataTable GetKodeTransaksi()
        {
            DataTable data = new DataTable();
            data.Columns.Add("id_pembelian", typeof(int));
            data.Columns.Add("nota_pembelian", typeof(string));
            DataRow drow = data.NewRow();
            drow["id_pembelian"] = -1;
            drow["nota_pembelian"] = "Semua";
            data.Rows.Add(drow);
            DataTable data1 = this.dataHutang.Copy();
            data1.Columns.RemoveAt(0);
            data1.Columns.RemoveAt(0);
            data1.Columns.RemoveAt(0);
            data1.Columns.RemoveAt(2);
            data1.Columns.RemoveAt(2);
            data1.Columns.RemoveAt(2);
            data1.Columns.RemoveAt(2);
            data.Merge(data1);
            return data;
        }

        private void comboBoxNoTransaksi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxNoNota.SelectedIndex != 0)
            {
                int ids = Convert.ToInt32(this.comboBoxNoNota.SelectedValue.ToString());
                DataView dv = new DataView(this.dataHutang);
                dv.RowFilter = $"[ID_PEMBELIAN]={ids}";
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
                    .Sum(t => Convert.ToDouble(t.Cells["belum_bayar"].Value));
            this.textBoxTotal.Text = total_.ToString("c");
            this.textBoxNominalBayar.Text = Convert.ToDouble(this.textBoxTotal.Text.Replace("Rp", "")).ToString();
            this.UpdateKembalian();
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
            this.UpdateKembalian();
        }

        private void UpdateKembalian()
        {
            double pembayaran_;
            var i_ = double.TryParse(this.textBoxNominalBayar.Text, out pembayaran_);
            double total_ = double.Parse(this.textBoxTotal.Text.Replace("Rp", ""));
            double kembalian_ = pembayaran_ - total_;
        }
    }
}
