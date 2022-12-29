using System;
using System.Data;
using System.Linq;
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

        public FormBayarHutangPelanggan() { InitializeComponent(); }

        private void buttonBatal_Click(object sender, EventArgs e) { this.Close(); }

        private void FormBayarHutang_Load(object sender, EventArgs e)
        {
            /// Load data hutang pelanggan
            this.RefreshDataHutangPelanggan();

            /// Nama Pelanggan
            this.textBoxNamaPelanggan.Text = this.NamaPelanggan;

            /// Combobox No Transaksi
            this.comboBoxNoTransaksi.DataSource = this.GetKodeTransaksi();
            this.comboBoxNoTransaksi.ValueMember = "id_penjualan";
            this.comboBoxNoTransaksi.DisplayMember = "kode_transaksi";
            this.comboBoxNoTransaksi.SelectedIndex = 0;
            this.comboBoxNoTransaksi.DropDownStyle = ComboBoxStyle.DropDownList;

            /// combo box kas
            this.RefreshDataKas();

            /// Nominal bayar
            this.textBoxNominalBayar.Text = 0.ToString("N2");
        }

        private void RefreshDataHutangPelanggan()
        {
            this.dataHutang = TokoBuku.DbUtility.Transactions.HutangPiutang.BayarHutangPelanggan
                .DataHutangPelanggan(this.IdPelanggan); // id_pelanggan = 7
            this.DgvListHutang.DataSource = this.dataHutang;

            this.DgvListHutang.Columns[0].Visible = false; // id piutang
            this.DgvListHutang.Columns[1].Visible = false; // id pelanggan
            this.DgvListHutang.Columns[2].Visible = false; // id penjualan
            for (int i = 1; i < this.DgvListHutang.ColumnCount; i++) { this.DgvListHutang.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells; }

            this.DgvListHutang.Columns[5].DefaultCellStyle.Format = "C";
            this.DgvListHutang.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.DgvListHutang.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.DgvListHutang.Columns[6].MinimumWidth = 100;
        }

        private void buttonBayar_Click(object sender, EventArgs e)
        {
            /// TODO: cek catatan
            double total_bayar;
            if (!double.TryParse(this.textBoxNominalBayar.Text, out total_bayar))
            {
                MessageBox.Show("Cek total pembayaran dulu.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.ActiveControl = this.textBoxNominalBayar;
            }
            else if (Convert.ToInt32(this.comboBoxNoTransaksi.SelectedValue.ToString()) == -1 && this.DgvListHutang.RowCount > 1)
            {/// perform untuk pembayaran semua. Syarat harus lunas semuanya, jika tidak, lanjutkan ke bembayaran tiap item
                if ((total_bayar - Convert.ToDouble(this.textBoxTotal.Text.Replace("Rp", ""))) >= 0)
                {/// uang pembayaran lunas
                    try
                    {
                        foreach (DataGridViewRow row in this.DgvListHutang.Rows)
                        {
                            /// TODO: Lakukan pembayaran hutang untuk masing-masih row
                            bool lunas = true;
                            int id_hutang = Convert.ToInt32(row.Cells["id_piutang"].Value.ToString());
                            double pembayaran = Convert.ToDouble(row.Cells["total"].Value.ToString());
                            DateTime tgl_bayar = dateTimePicker1.Value;
                            string id_kas = this.comboBoxJenisKas.SelectedValue.ToString();

                            DbUtility.Transactions.HutangPiutang.BayarHutangPelanggan
                                .BayarHutang(id_piutang: id_hutang, tanggal_bayar: tgl_bayar, pembayaran: pembayaran, id_kas: id_kas, sudah_lunas: lunas);
                        }
                        MessageBox.Show("Pembayaran Hutang Berhasil.", "SUcccess.");
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
                    this.ActiveControl = this.comboBoxNoTransaksi;
                }
            }
            else
            { /// TODO: perform untuk masing2 pembayaran
                bool lunas = false;
                double pembayaran;
                int id_hutang = Convert.ToInt32(this.DgvListHutang.Rows[0].Cells["id_piutang"].Value.ToString());
                var i_ = double.TryParse(this.textBoxNominalBayar.Text, out pembayaran);
                DateTime tgl_bayar = dateTimePicker1.Value;
                string id_kas = this.comboBoxJenisKas.SelectedValue.ToString();
                if (pembayaran - Convert.ToDouble(this.textBoxTotal.Text.Replace("Rp", "")) >= 0) { lunas = true; }
                if (pembayaran > 0)
                {
                    try
                    {
                        DbUtility.Transactions.HutangPiutang.BayarHutangPelanggan
                            .BayarHutang(id_piutang: id_hutang, tanggal_bayar: tgl_bayar, pembayaran: pembayaran, id_kas: id_kas, sudah_lunas: lunas);
                        MessageBox.Show("Pembayaran berhasil");
                        this.RefreshDataHutangPelanggan();
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
            data.Columns.Add("id_penjualan", typeof(int));
            data.Columns.Add("kode_transaksi", typeof(string));
            DataRow drow = data.NewRow();
            drow["id_penjualan"] = -1;
            drow["kode_transaksi"] = "Semua";
            data.Rows.Add(drow);
            DataTable data1 = this.dataHutang.Copy();
            data1.Columns.RemoveAt(0);
            data1.Columns.RemoveAt(0);
            data1.Columns.RemoveAt(2);
            data1.Columns.RemoveAt(2);
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
            double bayar_ = Convert.ToDouble(this.textBoxNominalBayar.Text);
            double total_;
            var x_ = double.TryParse(this.textBoxTotal.Text.Replace("Rp", ""), out total_);
            this.textBoxKembalian.Text = (bayar_ - total_).ToString("C");
        }

        #region HandleCurrency
        private void txtRealBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == 45)
            {
                /// char 45 = "-"
                TextBox t = (TextBox)sender;
                int cursorPosition = t.Text.Length - t.SelectionStart;      // Text in the box and Cursor position

                if (e.KeyChar == 45)
                {
                    if (t.Text[0] == 45)
                    {
                        t.Text = t.Text.Remove(0);
                    }
                    else
                    {
                        t.Text = "-" + t.Text;
                    }
                }
                else
                    if (t.Text.Length < 20)
                    t.Text = (decimal.Parse(t.Text.Insert(t.SelectionStart, e.KeyChar.ToString()).Replace(",", "").Replace(".", "")) / 100).ToString("N2");
                //t.Text = (decimal.Parse(t.Text.Insert(t.SelectionStart, e.KeyChar.ToString()).Replace(",", "").Replace(".", "")) / 100).ToString("N2");

                t.SelectionStart = (t.Text.Length - cursorPosition < 0 ? 0 : t.Text.Length - cursorPosition);
            }
            e.Handled = true;
        }
        private void txtRealBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)     // Deals with BackSpace e Delete keys
            {
                TextBox t = (TextBox)sender;
                int cursorPosition = t.Text.Length - t.SelectionStart;

                string Left = t.Text.Substring(0, t.Text.Length - cursorPosition).Replace(".", "").Replace(",", "");
                string Right = t.Text.Substring(t.Text.Length - cursorPosition).Replace(".", "").Replace(",", "");

                if (Left.Length > 0)
                {
                    Left = Left.Remove(Left.Length - 1);                            // Take out the rightmost digit
                    t.Text = (decimal.Parse(Left + Right) / 100).ToString("N2");
                    //t.Text = (decimal.Parse(Left + Right) / 100).ToString("N2");
                    t.SelectionStart = (t.Text.Length - cursorPosition < 0 ? 0 : t.Text.Length - cursorPosition);
                }
                e.Handled = true;
            }

            if (e.KeyCode == Keys.End)                                  // Treats End key
            {
                TextBox t = (TextBox)sender;
                t.SelectionStart = t.Text.Length;                       // Moves the cursor o the rightmost position
                e.Handled = true;
            }

            if (e.KeyCode == Keys.Home)                                 // Trata tecla Home
            {
                TextBox t = (TextBox)sender;
                //t.Text = 0.ToString("N2");                              // Set field value to zero 
                t.Text = 0.ToString("N2");
                t.SelectionStart = t.Text.Length;                       // Moves the cursor o the rightmost position
                e.Handled = true;
            }
        }
        private void txtRealBox_Enter(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;                                // Desliga seleção de texto
            t.SelectionStart = t.Text.Length;
        }
        #endregion

    }
}
