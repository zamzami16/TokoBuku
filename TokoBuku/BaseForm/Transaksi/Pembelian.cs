using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TokoBuku.BaseForm.TipeData.DataBase;
using TokoBuku.BaseForm.Transaksi.SearchForm;
using TokoBuku.DbUtility;
using TokoBuku.DbUtility.Transactions;

namespace TokoBuku.BaseForm.Transaksi
{
    public partial class Pembelian : Form
    {
        private DataTable ListBarangDibeli = new DataTable();
        private string KodeTerpilih;
        private int IdBarangTerpilih = -1;
        private int IdSupplierTerpilih = -1;
        private string IdKasTerpilih;
        private DataTable DataKas;
        private double TempHargaJual;
        //private string

        public Pembelian()
        {
            InitializeComponent();
        }

        private void textBoxKodeItem_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboJenisBayar_SelectedValueChanged(object sender, EventArgs e)
        {
            var x_ = (TJenisPembayaran)Enum.Parse(typeof(TJenisPembayaran), this.comboJenisBayar.Text);
            if (x_ == TJenisPembayaran.Kredit)
            {
                this.dateTimePickerJatuhTempo.Enabled = true;
                this.comboBoxJenisKas.Enabled = false;
                this.labelPembayaran.Text = "Pembayaran Awal";
                this.labelPembayaran.Enabled = true;
                this.textBoxPembayaranAwal.Enabled = true;
                this.dateTimePickerTglPesanan.Value = DateTime.Now;
                this.dateTimePickerJatuhTempo.Value = DateTime.Now.AddDays(7);
                this.textBoxTotalPembayaran.Enabled = false;
            }
            else if (x_ == TJenisPembayaran.Cash)
            {
                this.labelPembayaran.Enabled = false;
                this.textBoxPembayaranAwal.Text = "0";
                this.textBoxPembayaranAwal.Enabled = false;
                this.comboBoxJenisKas.Enabled = true;
                this.dateTimePickerTglPesanan.Value = DateTime.Now;
                this.dateTimePickerJatuhTempo.Value = DateTime.Now.AddDays(7);
                this.dateTimePickerJatuhTempo.Enabled = false;
            }
        }

        private void Penjualan_Load(object sender, EventArgs e)
        {
            this.ActiveControl = this.textNoNota;
            this.dateTimePickerJatuhTempo.Value = DateTime.Now.AddDays(7);
            this.dateTimePickerJatuhTempo.Enabled = false;
            this.comboSatuan.SelectedIndex = 0;

            /// init datagridview
            this.dataGridView1.Columns["barang"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            ComboBox _satuan = new ComboBox();
            _satuan.Items.Add("Pcs");
            _satuan.Items.Add("Packs");
            ((DataGridViewComboBoxColumn)dataGridView1.Columns["satuan"]).DataSource = _satuan.Items;

            this.dataGridView1.Columns["subtotal_harga"].DefaultCellStyle.Format = "C";
            this.dataGridView1.Columns["harga_Satuan"].DefaultCellStyle.Format = "C";

            this.dataGridView1.Columns["subtotal_harga"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            this.dataGridView1.Columns["harga_Satuan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;

            this.RefreshDataKas();

            this.textBoxPembayaranAwal.Text = 0.ToString("N2");
            this.textBoxTotalPembayaran.Text = 0.ToString("N2");
            this.textsubTotalHargaBeli.Text = 0.ToString("N2");

            foreach (var item in Enum.GetValues(typeof(TJenisPembayaran)))
            {
                this.comboJenisBayar.Items.Add(item);
            }
            this.comboJenisBayar.SelectedIndex = 0;
            this.buttUbahHargaJual.Enabled = false;
        }

        private void RefreshDataKas()
        {
            this.DataKas = DbSearchLoadData.Kas();
            this.comboBoxJenisKas.DataSource = this.DataKas;
            this.comboBoxJenisKas.DisplayMember = "NAMA";
            this.comboBoxJenisKas.ValueMember = "ID";
            this.comboBoxJenisKas.SelectedIndex = 0;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            double harga = 0;
            if (!string.IsNullOrWhiteSpace(textBoxPembayaranAwal.Text) && double.TryParse(textBoxPembayaranAwal.Text, out harga))
            {
                if (harga > 0)
                {
                    comboBoxJenisKas.Enabled = true;
                }
            }
            else
            {
                comboBoxJenisKas.Enabled = false;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void dataGridView1_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells["jumlah"].Value = 1;
            e.Row.Cells["satuan"].Value = "Pcs";
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int row_idx = e.RowIndex;
            int col_idx = e.ColumnIndex;
            string txt = "";
            if (dataGridView1[col_idx, row_idx].Value != null)
            {
                txt = dataGridView1[col_idx, row_idx].Value.ToString();
            }
            MessageBox.Show(txt);
            dataGridView1[col_idx, row_idx].Value = "makan";
        }

        private void textBoxKodeItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.FilterDataBarang_();
            }
        }

        private void FilterDataBarang_()
        {
            using (var form = new FormSearchBarang())
            {
                form.FormName = "kode";
                form.SearchText = this.textBoxKodeItem.Text;
                form.TipeForm = "pembelian";
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    this.KodeTerpilih = form.SearchBarang.Kode;
                    this.IdBarangTerpilih = form.SearchBarang.Id;
                    this.textBoxKodeItem.Text = form.SearchBarang.Kode;
                    this.textBoxNamaItem.Text = form.SearchBarang.Nama;
                    this.textBoxHargaJual.Text = form.SearchBarang.HargaJual.ToString();
                    this.textHargaBeliSatuan.Text = form.SearchBarang.HargaBeli.ToString();
                    this.TempHargaJual = Convert.ToDouble(form.SearchBarang.HargaJual.ToString());
                    this.ActiveControl = this.textBoxQty;
                }
            }
        }

        private void FilterDataBarang()
        {
            using (var form = new FormSearchBarang())
            {
                form.FormName = "barang";
                form.SearchText = this.textBoxNamaItem.Text;
                form.TipeForm = "pembelian";
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    this.KodeTerpilih = form.SearchBarang.Kode;
                    this.IdBarangTerpilih = form.SearchBarang.Id;
                    this.textBoxKodeItem.Text = form.SearchBarang.Kode;
                    this.textBoxNamaItem.Text = form.SearchBarang.Nama;
                    this.textBoxHargaJual.Text = form.SearchBarang.HargaJual.ToString();
                    this.textHargaBeliSatuan.Text = form.SearchBarang.HargaBeli.ToString();
                    this.TempHargaJual = Convert.ToDouble(form.SearchBarang.HargaJual.ToString());
                    this.ActiveControl = this.textBoxQty;
                }
            }
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();

            var centerFormat = new StringFormat()
            {
                // right alignment might actually make more sense for numbers
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }

        private void ButSearchKode_Click(object sender, EventArgs e)
        {
            this.FilterDataBarang_();
            this.buttUbahHargaJual.Enabled = true;
        }

        private void textBoxNamaItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.FilterDataBarang();
            }
        }

        private void FilterDataSupplier()
        {
            using (var form = new FormSearchSupplierPelanggan())
            {
                form.FormName = "supplier";
                form.SearchText = this.textBoxSupplier.Text;
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    this.IdSupplierTerpilih = form.Supplier.Id;
                    this.textBoxSupplier.Text = form.Supplier.Nama;
                }
            }
        }

        private void textBoxSupplier_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.FilterDataSupplier();
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            double hargaBeli;
            double satuan;
            double subhargabeli;
            if (this.IdBarangTerpilih == -1)
            {
                MessageBox.Show("Pilih data barang terlebih dahulu.", "Warning.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.ActiveControl = this.textBoxKodeItem;
            }
            else if (!double.TryParse(this.textHargaBeliSatuan.Text, out hargaBeli) || hargaBeli < 0)
            {
                MessageBox.Show("Masukkan harga satuan terlebih dahulu.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.ActiveControl = this.textHargaBeliSatuan;
            }
            else if (!double.TryParse(this.textBoxQty.Text, out satuan) || satuan <= 0)
            {
                MessageBox.Show("Masukkan jumlah satuan terlebih dahulu.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.ActiveControl = this.textBoxQty;
            }
            else if (!double.TryParse(this.textsubTotalHargaBeli.Text, out subhargabeli) || subhargabeli <= 0)
            {
                MessageBox.Show("Cek Subtotal Harga Beli terlebih dahulu.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.ActiveControl = this.textsubTotalHargaBeli;
            }
            else
            {
                int row_idx = this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[row_idx].Cells["id"].Value = this.IdBarangTerpilih.ToString();
                this.dataGridView1.Rows[row_idx].Cells["kode"].Value = this.KodeTerpilih.ToString();
                this.dataGridView1.Rows[row_idx].Cells["barang"].Value = this.textBoxNamaItem.Text;
                this.dataGridView1.Rows[row_idx].Cells["jumlah"].Value = satuan;
                this.dataGridView1.Rows[row_idx].Cells["satuan"].Value = this.comboSatuan.Text;
                this.dataGridView1.Rows[row_idx].Cells["harga_Satuan"].Value = hargaBeli;
                this.dataGridView1.Rows[row_idx].Cells["subtotal_harga"].Value = subhargabeli;
                if (this.comboSatuan.Text.ToLower() == "packs")
                {
                    satuan *= 10;
                    this.dataGridView1.Rows[row_idx].Cells["subtotal_harga"].Value = subhargabeli * 10;
                }
                this.ResetFormInput();
                this.UpdateTotalBeli();
            }
        }

        private void ResetFormInput()
        {
            this.textBoxKodeItem.Text = string.Empty;
            this.textBoxNamaItem.Text = string.Empty;
            this.textBoxQty.Text = "1";
            this.comboSatuan.SelectedIndex = 0;
            this.textHargaBeliSatuan.Text = "0";
            this.textsubTotalHargaBeli.Text = "0";
            this.textBoxHargaJual.Text = "0";
            this.buttUbahHargaJual.Enabled = false;
            this.IdBarangTerpilih = -1;
            this.ActiveControl = this.textBoxKodeItem;
        }

        private void ResetFormAll()
        {
            this.ResetFormInput();
            this.textNoNota.Text = string.Empty;
            this.textBoxSupplier.Text = string.Empty;
            this.IdSupplierTerpilih = -1;
            this.comboBoxJenisKas.SelectedIndex = 0;
            this.dataGridView1.Rows.Clear();
            this.comboJenisBayar.SelectedIndex = 0;
            this.labelSubTotal.Text = "Rp.0";
            this.textsubTotalHargaBeli.Text = "0";
            this.textBoxTotalPembayaran.Text = "0";
            this.richTextBox1.Text = string.Empty;
            this.dateTimePickerTglPesanan.Value = DateTime.Now;
            this.dateTimePickerJatuhTempo.Value = DateTime.Now;
            this.comboJenisBayar.SelectedIndex = 0;
        }

        private void UpdateTotalBeli()
        {
            if (this.dataGridView1.Rows.Count > 0)
            {
                double total_ = dataGridView1.Rows.Cast<DataGridViewRow>()
                    .Sum(t => Convert.ToDouble(t.Cells["subtotal_harga"].Value));
                this.labelSubTotal.Text = total_.ToString("C");
                this.textBoxTotalPembayaran.Text = total_.ToString();
            }
        }

        private void Pembelian_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.F4:
                    this.buttonSupplier_Click(sender, e);
                    break;
                case Keys.F5:
                    this.ButSearchKode_Click(sender, e);
                    break;
                case Keys.F6:
                    this.buttonAdd_Click(sender, e);
                    break;
            }
        }

        private void buttonSupplier_Click(object sender, EventArgs e)
        {
            this.FilterDataSupplier();
        }

        private void Pembelian_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Shift && e.KeyCode == Keys.F5)
            {
                this.ButSearchBarang_Click(sender, e);
            }
        }

        private void ButSearchBarang_Click(object sender, EventArgs e)
        {
            this.FilterDataBarang();
            this.buttUbahHargaJual.Enabled = true;
        }

        private void textHargaBeliSatuan_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double sub_ = double.Parse(this.textBoxQty.Text.Trim()) * double.Parse(this.textHargaBeliSatuan.Text.Trim());
                this.textsubTotalHargaBeli.Text = sub_.ToString();
            }
            catch (Exception) { }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.labelKasir.Text))
            {
                MessageBox.Show("Pilih kasir terlebih dahulu", "Warning.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.ActiveControl = this.labelKasir;
            }
            else if (string.IsNullOrEmpty(this.textBoxSupplier.Text) || this.IdSupplierTerpilih < 1)
            {
                MessageBox.Show("Supplier tidak boleh kosong.\nPilih Supplier terlebih dahulu.", "Warning.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.ActiveControl = this.textBoxSupplier;
            }
            else if (string.IsNullOrWhiteSpace(this.textNoNota.Text))
            {
                MessageBox.Show("Masukkan nomor nota terlebih dahulu.", "Warning.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.ActiveControl = this.textNoNota;
            }
            else
            {
                if ((TJenisPembayaran)Enum.Parse(typeof(TJenisPembayaran), this.comboJenisBayar.SelectedItem.ToString()) == TJenisPembayaran.Cash)
                {
                    this.ProsedurPembelianCash();
                }
                else if ((TJenisPembayaran)Enum.Parse(typeof(TJenisPembayaran), this.comboJenisBayar.SelectedItem.ToString()) == TJenisPembayaran.Kredit)
                {
                    this.ProsedurPembelianKredit();
                }
            }
        }

        private void ProsedurPembelianCash()
        {
            double totalPembayaran = 0;
            if (!double.TryParse(this.textBoxTotalPembayaran.Text, out totalPembayaran) || totalPembayaran <= 0)
            {
                MessageBox.Show("Check total pembayaran terlebih dahulu", "Warning.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.ActiveControl = this.textBoxTotalPembayaran;
            }
            else if (!this.CekSaldoKasCukupPembayaran(Convert.ToInt32(this.IdKasTerpilih), totalPembayaran))
            {
                MessageBox.Show("Saldo kas tidak cukup. Tolong gunakan kas yang lain atau update saldo kas terlebih dahulu.", "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.ActiveControl = this.comboBoxJenisKas;
            }
            else
            {
                TPembelian pembelian = new TPembelian(TJenisPembayaran.Cash);
                pembelian.IdSupplier = this.IdSupplierTerpilih;
                pembelian.NoNota = this.textNoNota.Text;
                pembelian.TanggalBeli = this.dateTimePickerTglPesanan.Value;
                pembelian.Total = totalPembayaran;
                pembelian.IdKas = Convert.ToInt32(this.IdKasTerpilih);
                try
                {
                    DbUtility.Transactions.Pembelian
                        .SavePembelianCash(pembelian, detailPembelian: this.ConvertDgvToDetailPembelian(this.dataGridView1));
                    this.ResetFormAll();
                    MessageBox.Show("Data Berhasil disimpan.", "Success.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw;
                }
            }
        }

        private List<TDetailPembelian> ConvertDgvToDetailPembelian(DataGridView dgv)
        {
            List<TDetailPembelian> detailPembelian = new List<TDetailPembelian>();
            foreach (DataGridViewRow row in dgv.Rows)
            {
                TDetailPembelian detail = new TDetailPembelian();
                detail.IdBarang = Convert.ToInt32(row.Cells["id"].Value.ToString());
                detail.Jumlah = Convert.ToDouble(row.Cells["jumlah"].Value.ToString());
                detail.Harga = Convert.ToDouble(row.Cells["harga_Satuan"].Value.ToString());
                detail.Satuan = row.Cells["satuan"].Value.ToString();
                detailPembelian.Add(detail);
            }
            return detailPembelian;
        }

        private DataTable ConvertDGVtoDT(DataGridView dgv)
        {
            DataTable dt = new DataTable();
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                dt.Columns.Add(col.Name);
            }

            foreach (DataGridViewRow row in dgv.Rows)
            {
                DataRow dRow = dt.NewRow();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    dRow[cell.ColumnIndex] = cell.Value;
                }
                dt.Rows.Add(dRow);
            }
            return dt;
        }

        private void ProsedurPembelianKredit()
        {
            double totalPembayaran;
            double pembayaranAwal;
            if (!double.TryParse(this.textBoxTotalPembayaran.Text, out totalPembayaran) || totalPembayaran <= 0)
            {
                MessageBox.Show("Check total pembayaran terlebih dahulu", "Warning.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.ActiveControl = this.textBoxTotalPembayaran;
            }
            else if (string.IsNullOrWhiteSpace(this.textBoxPembayaranAwal.Text))
            {
                this.textBoxPembayaranAwal.Text = "0";
            }
            else if (!double.TryParse(this.textBoxPembayaranAwal.Text, out pembayaranAwal) || pembayaranAwal < 0)
            {
                MessageBox.Show("Check pembayaran awal terlebih dahulu", "Warning.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.ActiveControl = this.textBoxPembayaranAwal;
            }
            else if (this.dateTimePickerJatuhTempo.Value < this.dateTimePickerTglPesanan.Value)
            {
                MessageBox.Show("Tanggal jatuh tempo harus lebih banyak dari tanggal pesan. \nCheck tanggal jatuh tempo terlebih dahulu", "Warning.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.ActiveControl = this.dateTimePickerJatuhTempo;
            }
            else if (!this.CekSaldoKasCukupPembayaran(Convert.ToInt32(this.IdKasTerpilih), pembayaranAwal))
            {
                MessageBox.Show("Saldo kas tidak cukup. Tolong gunakan kas yang lain atau update saldo kas terlebih dahulu.", "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.ActiveControl = this.comboBoxJenisKas;
            }
            else
            {
                TPembelian pembelian = new TPembelian();
                pembelian.IdSupplier = this.IdSupplierTerpilih;
                pembelian.TanggalBeli = this.dateTimePickerTglPesanan.Value;
                pembelian.NoNota = this.textNoNota.Text;
                pembelian.Total = totalPembayaran;
                pembelian.JenisPembayaran = TJenisPembayaran.Kredit;
                if (this.comboBoxJenisKas.Enabled == true)
                {
                    pembelian.IdKas = Convert.ToInt32(this.IdKasTerpilih);
                }

                THutang hutang = new THutang();
                hutang.IdSupplier = pembelian.IdSupplier;
                hutang.TanggalTenggatBayar = this.dateTimePickerJatuhTempo.Value;
                hutang.Total = pembelian.Total - pembayaranAwal;
                hutang.Lunas = TLunas.Belum;
                TBayarHutang bayarHutang = new TBayarHutang();
                bayarHutang.Pembayaran = pembayaranAwal;
                bayarHutang.TglBayar = pembelian.TanggalBeli;
                bayarHutang.IdKas = pembelian.IdKas;
                bayarHutang.isDP = TIsDP.ya;
                try
                {
                    TokoBuku.DbUtility.Transactions.Pembelian
                        .SavePembelianKredit(pembelian, detailPembelian: this.ConvertDgvToDetailPembelian(this.dataGridView1), hutang, bayarHutang);
                    this.ResetFormAll();
                    MessageBox.Show("Data Berhasil disimpan.", "Success.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error @Save Pembelian Kredit: {ex.Message}", "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw;
                }
            }
        }

        private void comboBoxJenisKas_SelectedValueChanged(object sender, EventArgs e)
        {
            this.IdKasTerpilih = this.comboBoxJenisKas.SelectedValue.ToString();
        }

        private void textBoxQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double sub_ = double.Parse(this.textBoxQty.Text.Trim()) * double.Parse(this.textHargaBeliSatuan.Text.Trim());
                this.textsubTotalHargaBeli.Text = sub_.ToString();
            }
            catch (Exception)
            {
                /// 
            }
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

        private void buttonReset_Click(object sender, EventArgs e)
        {
            this.ResetFormAll();
        }

        private void buttonHistoriPembelian_Click(object sender, EventArgs e)
        {
            using (HistoriPembelian histori = new HistoriPembelian())
            {
                histori.ShowDialog();
            }
        }

        private void buttUbahHargaJual_Click(object sender, EventArgs e)
        {
            double hargaBaru;
            string TempMsg = "";
            if (!double.TryParse(this.textBoxHargaJual.Text, out hargaBaru))
            {
                MessageBox.Show("Check harga jual yang baru.", "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.ActiveControl = this.textBoxHargaJual;
            }
            else if (hargaBaru < TempHargaJual)
            {
                TempMsg = "Harga Baru lebih Sedikit dari harga lama.\n";
                GantiHargaJual(this.IdBarangTerpilih, hargaBaru, TempMsg);
            }
            else
            {
                GantiHargaJual(this.IdBarangTerpilih, hargaBaru, TempMsg);
            }
        }

        private void GantiHargaJual(int id_barang, double hargaBaru, string TempMsg)
        {
            if (MessageBox.Show(TempMsg + $"Ganti Harga lama : {TempHargaJual} ke Harga Baru: {hargaBaru}?", "Ganti Harga Jual", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    DbUtility.Transactions.Pembelian.UbahHargaJualBarang(id_barang, hargaBaru);
                    MessageBox.Show("Harga jual berhasil di update.");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private bool CekSaldoKasCukupPembayaran(int idKas, double pembayaran)
        {
            bool cukup;
            cukup = (UpdateKas.GetKasSekarang(idKas) - pembayaran >= 0);
            return cukup;
        }
    }
}
