using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
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

        public FormBayarHutangSupplier()
        {
            InitializeComponent();
        }


        private void buttonBatal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormBayarHutang_Load(object sender, EventArgs e)
        {
            /// assign value
            this.textBoxNamaSupplier.Text = this.NamaSupplier;

            this.RefreshDataHutang();

            /// Combobox No Transaksi
            this.comboBoxNoNota.DataSource = this.GetKodeTransaksi();
            this.comboBoxNoNota.ValueMember = "id_pembelian";
            this.comboBoxNoNota.DisplayMember = "no_nota";
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
            this.DgvListHutang.Columns[0].Visible = false;
            this.DgvListHutang.Columns[1].Visible = false;
            this.DgvListHutang.Columns[3].Visible = false;

            for (int i = 4; i < this.DgvListHutang.ColumnCount; i++)
            {
                this.DgvListHutang.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            this.DgvListHutang.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void buttonBayar_Click(object sender, EventArgs e)
        {
            /// TODO: cek catatan. perform jika cicil bagaimana
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
                            int id_hutang = Convert.ToInt32(row.Cells["id_hutang"].Value.ToString());
                            double pembayaran = Convert.ToDouble(row.Cells["nominal_hutang"].Value.ToString());
                            DateTime tgl_bayar = dateTimePicker1.Value;
                            string id_kas = this.comboBoxJenisKas.SelectedValue.ToString();

                            DbUtility.Transactions.HutangPiutang.BayarHutangKeSupplier
                                .BayarHutang(id_hutang: id_hutang, pembayaran: pembayaran, tgl_bayar: tgl_bayar, id_kas: id_kas, lunas: true);
                        }
                        MessageBox.Show("Pembayaran Hutang Berhasil.", "SUcccess.");
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
            { /// TODO: perform untuk masing2 pembayaran
                bool lunas = false;
                double pembayaran;
                int id_hutang = Convert.ToInt32(this.DgvListHutang.Rows[0].Cells["id_hutang"].Value.ToString());
                var i_ = double.TryParse(this.textBoxNominalBayar.Text, out pembayaran);
                DateTime tgl_bayar = dateTimePicker1.Value;
                string id_kas = this.comboBoxJenisKas.SelectedValue.ToString();
                if (pembayaran - Convert.ToDouble(this.textBoxTotal.Text.Replace("Rp", "")) >= 0)
                {
                    lunas = true;
                }
                if (pembayaran > 0)
                {
                    try
                    {
                        DbUtility.Transactions.HutangPiutang.BayarHutangKeSupplier
                            .BayarHutang(id_hutang: id_hutang, pembayaran: pembayaran, tgl_bayar: tgl_bayar, id_kas: id_kas, lunas: lunas);
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
            data.Columns.Add("no_nota", typeof(string));
            DataRow drow = data.NewRow();
            drow["id_pembelian"] = -1;
            drow["no_nota"] = "Semua";
            data.Rows.Add(drow);
            DataTable data1 = this.dataHutang.Copy();
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
                    .Sum(t => Convert.ToDouble(t.Cells["nominal_hutang"].Value));
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
