using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TokoBuku.BaseForm.Transaksi.SearchForm;
using TokoBuku.DbUtility;
using TokoBuku.DbUtility.Transactions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using DataGridViewNumericUpDownElements;

namespace TokoBuku.BaseForm.Transaksi
{
    public partial class Penjualan : Form
    {
        private DataTable DataPenjualan;
        private DataTable DataBarang;
        private DataTable DataKas;
        private DataTable DataPelanggan;
        private DataTable ListBarang = new DataTable();
        private string PelangganNamaTerpilih;
        private int PelangganIdTerpilih;
        private string KodeTerpilih;
        private int BarangIdTerpilih;
        private string BarangTerpilih;
        private double TotalHarga;
        private double TotalKembalian;

        public Penjualan()
        {
            InitializeComponent();
            
        }

        private void comboJenisBayar_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboJenisBayar.Text == "KREDIT")
            {
                this.dateTimePickerJatuhTempo.Enabled = true;
                this.comboBoxJenisKas.Enabled = false;
                //this.labelKembalian.Enabled = false;
                //this.labelkembali.Enabled = false;
                this.labelDp.Text = "Pembayaran Awal :";
                this.labelkembali.Text = "Jumlah Kekurangan";
            }
            else
            {
                this.dateTimePickerJatuhTempo.Enabled = false;
                this.comboBoxJenisKas.Enabled = true;
                this.labelKembalian.Enabled = true;
                this.labelkembali.Enabled = true;
                this.labelDp.Text = "Cash :";
                this.labelkembali.Text = "Jumlah Kembalian";
            }
            

            /*if (comboJenisBayar.Text != "CASH")
            {
                using (var form = new FormKredit())
                {
                    var result = form.ShowDialog();
                    if (result == DialogResult.Yes)
                    {
                        var DP = form.PembayaranAwal;
                        var TglPesan = form.TglPesan;
                        var TglBayar = form.TenggatPembayaran;
                        var namaPelanggan = form.NamaPelanggan;

                        if (DP <= 0)
                        {
                            //this.panelJenisKas.Visible = false;
                        }
                    }
                }
                
            }*/
        }

        private void buttonBawahCancel_Click(object sender, EventArgs e)
        {
            this.ResetForm();
        }

        private void buttonPelanggan_Click(object sender, EventArgs e)
        {
            this.FilterDataPelanggan();
        }

        private void Penjualan_Load(object sender, EventArgs e)
        {
            /// handle function key
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyEvent);


            this.dateTimePickerJatuhTempo.Value = DateTime.Now.AddDays(7);
            this.dateTimePickerJatuhTempo.Enabled = false;
            this.comboJenisBayar.SelectedIndex = 0;
            this.labelNoTransaksi.Text = this.GenerateNoTransaksi();
            this.ActiveControl = this.textKode;

            /// Init Data Header Data List Barang
            this.ListBarang.Columns.Add("Id", typeof(string));      // 0
            this.ListBarang.Columns.Add("Kode", typeof(string));    // 1
            this.ListBarang.Columns.Add("Nama", typeof(string));    // 2
            this.ListBarang.Columns.Add("Jumlah", typeof(double));  // 3
            this.ListBarang.Columns.Add("Satuan", typeof(string));  // 4
            this.ListBarang.Columns.Add("Harga", typeof(double));   // 5
            this.ListBarang.Columns.Add("Diskon", typeof(double));  // 6
            this.ListBarang.Columns.Add("Total", typeof(double));   // 7

            this.ListBarang.Columns[7].Expression = "Jumlah * Harga * (1.00 - Diskon / 100.00)";

            this.dataGridView1.DataSource = this.ListBarang;
            this.dataGridView1.Columns[0].Visible = false;
            //this.dataGridView1.Columns.Add("Button", "Delete");

            /// set datagridView formatting
            /// 
            this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //this.dataGridView1.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //this.dataGridView1.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

            this.dataGridView1.Columns[2].FillWeight = 25;
            this.dataGridView1.Columns["jumlah"].ReadOnly = false;
            //this.dataGridView1.Columns[7].FillWeight = 20;
            //this.dataGridView1.Columns[7].

            this.comboSatuan.SelectedIndex = 0;
            this.PelangganIdTerpilih = TokoBuku.DbUtility.Transactions.Penjualan.GetIdPelangganUmum();

            /// Init Data Pelanggan
            this.RefreshDataPelanggan();

            /// Init Data Nama Barang
            this.RefreshDataBarang();

            /// Init Data Jenis Kas
            this.RefreshDataKas();
            /// init Data Kode Barang
            this.RefreshKodeBarang();



        }

        private void buttonBawahProcess_Click(object sender, EventArgs e)
        {
            /// TODO: Ganti algoritma penyimpanan data ke database.
            /// Pisahkan input penjualan dan detail penjualan. masukkan piutang
            if (dataGridView1.Rows.Count < 1)
            {
                MessageBox.Show("Pilih barang terlebih dahulu.", "Gagal.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.ActiveControl = this.textKode;
            }
            else if (string.IsNullOrWhiteSpace(this.comboBoxJenisKas.Text))
            {
                MessageBox.Show("Pilih jenis kas dulu.", "Warning.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.ActiveControl = this.comboBoxJenisKas;
            }
            else
            {
                if (this.comboJenisBayar.Text == "CASH")
                {
                    if (this.TotalKembalian > 0)
                    {
                        bool Success = false;
                        foreach (DataGridViewRow row in this.dataGridView1.Rows)
                        {
                            int id_barang = Convert.ToInt32(row.Cells[0].Value.ToString());
                            double quantity = Convert.ToDouble(row.Cells[3].Value.ToString());
                            if (row.Cells[4].Value.ToString().ToLower() == "packs")
                            {
                                quantity *= 10;
                            }
                            try
                            {
                                TokoBuku.DbUtility.Transactions.Penjualan.SaveCash(
                                    kode_transaksi: this.labelNoTransaksi.Text,
                                    id_kasir: 1,
                                    id_barang: id_barang,
                                    quantity: quantity,
                                    tanggal: this.dateTimePickerTglPesanan.Value,
                                    waktu: DateTime.Now,
                                    id_kas: Convert.ToInt32(this.comboBoxJenisKas.SelectedValue.ToString())
                                    );
                                Success = true;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Success = false;
                            }
                        }
                        if (Success)
                        {

                            MessageBox.Show("Data Berhasil disimpan.", "Success.");
                            this.ResetForm();
                            this.GenerateNoTransaksi();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Masukkan Uang Pembayaran terlebih dahulu.", "warning.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.ActiveControl = this.textBoxPembayaranAwal;
                    }
                }

                /// Kredit
                else if(this.comboJenisBayar.Text == "KREDIT")
                {
                    if (this.TotalKembalian > 0)
                    {
                        var results = MessageBox.Show("Jumlah uang pembayaran bisa digunakan untuk pembayaran CASH.\nApakah anda mau menggunakan metode pembayaran CASH?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (results == DialogResult.Yes)
                        {
                            this.comboJenisBayar.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        bool Success = false;
                        foreach (DataGridViewRow row in this.dataGridView1.Rows)
                        {
                            int id_barang = Convert.ToInt32(row.Cells[0].Value.ToString());
                            double quantity = Convert.ToDouble(row.Cells[3].Value.ToString());
                            if (row.Cells[4].Value.ToString().ToLower() == "packs")
                            {
                                quantity *= 10;
                            }
                            string id_kas = null;
                            if (this.comboBoxJenisKas.Enabled == true)
                            {
                                id_kas = this.comboBoxJenisKas.SelectedValue.ToString();
                            }
                            try
                            {

                                int id_penjualan = TokoBuku.DbUtility
                                    .Transactions.Penjualan.SaveKredit(
                                    kode_transaksi: this.labelNoTransaksi.Text,
                                    id_kasir: 1,
                                    id_barang: id_barang,
                                    quantity: quantity,
                                    tanggal: this.dateTimePickerTglPesanan.Value,
                                    waktu: DateTime.Now,
                                    tgl_tenggat_bayar: this.dateTimePickerJatuhTempo.Value,
                                    pembayaran_awal: Convert.ToDouble(this.textBoxPembayaranAwal.Text),
                                    id_kas: id_kas
                                    );

                                try
                                {
                                    TokoBuku.DbUtility.Transactions.Penjualan.SavePiutang(id_penjualan: id_penjualan, id_pelanggan: this.PelangganIdTerpilih);
                                    Success = true;
                                }
                                catch (Exception ex)
                                {
                                    TokoBuku.DbUtility.Transactions.Penjualan.DeleteSaveKredit(id_penjualan: id_penjualan);
                                    Success = false;
                                    throw;
                                }

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                //throw;
                                Success= false;
                            }
                        }
                        if (Success)
                        {
                            this.ResetForm();
                            MessageBox.Show("Data Berhasil disimpan.", "Success.");
                            this.GenerateNoTransaksi();
                        }
                    }
                }
            }
        }

        private void ResetForm()
        {
            this.ListBarang.Clear();
            this.dataGridView1.DataSource = this.ListBarang;
            this.LatbelHargaTotal.Text = "0";
            this.labelSubTotal.Text = "0";
            this.textBoxPembayaranAwal.Text = "0";
            this.labelKembalian.Text = "0";
            this.textBox1.Text = "0";
            this.comboBoxJenisKas.SelectedIndex = 0;
            this.comboJenisBayar.SelectedIndex = 0;
            this.textNamaPelangganAtas.Text = "UMUM";
            this.PelangganIdTerpilih = TokoBuku.DbUtility.Transactions.Penjualan.GetIdPelangganUmum();
        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            float harga = 0;
            comboBoxJenisKas.Enabled = true;
            if (!string.IsNullOrWhiteSpace(textBoxPembayaranAwal.Text) && float.TryParse(textBoxPembayaranAwal.Text, out harga))
            {
                if (harga > 0)
                {
                    double diskon = 0;
                    comboBoxJenisKas.Enabled = true;
                    double SubTotalPrice = this.ListBarang.AsEnumerable().Sum(row => row.Field<double>("Total"));
                    var r_ = double.TryParse(textBox1.Text, out diskon);
                    if (r_)
                    {
                        var totalHarga = (1.00 - diskon / 100) * SubTotalPrice;
                        this.labelKembalian.Text = (harga - totalHarga).ToString("C");
                        this.TotalHarga = totalHarga;
                        this.TotalKembalian = harga - totalHarga;
                    }
                    else
                    {
                        this.labelKembalian.Text = (harga - SubTotalPrice).ToString("C");
                        this.TotalHarga = SubTotalPrice;
                        this.TotalKembalian = harga - this.TotalHarga;
                    }
                }
            }
            /*if (comboJenisBayar.Text == "KREDIT")
            {
                if (!string.IsNullOrWhiteSpace(textBoxPembayaranAwal.Text) && float.TryParse(textBoxPembayaranAwal.Text, out harga))
                {
                    if (harga > 0)
                    {
                        comboBoxJenisKas.Enabled = true;
                        this.labelKembalian.Text = 0.ToString("C");
                    }
                }
                else
                {
                    comboBoxJenisKas.Enabled = false;
                    this.labelKembalian.Text = 0.ToString("C");
                }
            }
            else
            {
                comboBoxJenisKas.Enabled = true;
                if (!string.IsNullOrWhiteSpace(textBoxPembayaranAwal.Text) && float.TryParse(textBoxPembayaranAwal.Text, out harga))
                {
                    if (harga > 0)
                    {
                        double diskon = 0;
                        double SubTotalPrice = this.ListBarang.AsEnumerable().Sum(row => row.Field<double>("Total"));
                        var r_ = double.TryParse(textBox1.Text, out diskon);
                        if (r_)
                        {
                            var totalHarga = (1.00 - diskon / 100) * SubTotalPrice;
                            this.labelKembalian.Text = (harga - totalHarga).ToString("C");
                            this.TotalHarga = totalHarga;
                            this.TotalKembalian = harga - totalHarga;
                        }
                        else
                        {
                            this.labelKembalian.Text = (harga - SubTotalPrice).ToString("C");
                            this.TotalHarga = SubTotalPrice;
                            this.TotalKembalian = harga - this.TotalHarga;
                        }
                    }
                }
            }*/

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textNamaBarang.Text))
            {
                MessageBox.Show("Pilih Kode atau Nama Barang dulu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.ActiveControl = this.textKode;
            }
            else
            {
                double qty = Convert.ToDouble(textBoxQty.Text);
                var satuan = comboSatuan.Text;
                var dt = GetDataPartial.Barang(this.BarangIdTerpilih, qty, satuan);
                if (this.dataGridView1.Columns.Count > 8)
                {
                    this.dataGridView1.Columns.RemoveAt(8);
                }
                DataGridViewButtonColumn deleteButt = new DataGridViewButtonColumn();
                deleteButt.Name = "Delete";
                deleteButt.HeaderText = "Delete";
                deleteButt.Text = "Delete";
                deleteButt.UseColumnTextForButtonValue = true;
                this.dataGridView1.Columns.Insert(8, deleteButt);

                this.ListBarang.Merge(dt);

                /// format to currency view
                this.dataGridView1.Columns[5].DefaultCellStyle.Format = "C";
                this.dataGridView1.Columns[6].DefaultCellStyle.Format = "0.00'%'";
                this.dataGridView1.Columns[7].DefaultCellStyle.Format = "C";
                this.dataGridView1.Refresh();

                this.textNamaBarang.Text = string.Empty;
                this.textKode.Text = string.Empty;
                this.textBoxQty.Text = "1";
                this.comboSatuan.SelectedIndex = 0;
                double SubTotalPrice = this.ListBarang.AsEnumerable().Sum(row => row.Field<double>("Total"));
                this.labelSubTotal.Text = SubTotalPrice.ToString("C");
                this.ActiveControl = this.textKode;
            }
        }

        private string GenerateNoTransaksi()
        {
            var tgl = DateTime.Now.ToString("yyyyMMdd") + "-";
            var no_db = TokoBuku.DbUtility.Transactions.Penjualan.GenerateNoTransaksiPenjualan();
            this.labelNoTransaksi.Text = tgl + no_db.ToString();
            return tgl + no_db.ToString();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridView1.Rows.Count > 0)
            {
                if (e.ColumnIndex == dataGridView1.Columns["Delete"].Index)
                {
                    //Do something with your button.
                    int row_idx = e.RowIndex;
                    if (row_idx > -1)
                    {

                        this.ListBarang.Rows.Remove(ListBarang.Rows[row_idx]);
                        double SubTotalPrice = this.ListBarang.AsEnumerable().Sum(row => row.Field<double>("Total"));
                        this.labelSubTotal.Text = SubTotalPrice.ToString("C");
                    }
                }
            }
        }

        #region RefreshDataBarang
        private void RefreshDataPelanggan()
        {

            this.DataPelanggan = DbSearchLoadData.Pelanggan();
            this.textNamaPelangganAtas.AutoCompleteSource = AutoCompleteSource.CustomSource;
            this.textNamaPelangganAtas.AutoCompleteMode= AutoCompleteMode.SuggestAppend;
        }

        private void RefreshDataBarang()
        {
            this.DataBarang = DbSearchLoadData.Barang();
        }

        private void RefreshDataKas()
        {
            this.DataKas = DbSearchLoadData.Kas();
            this.comboBoxJenisKas.DataSource = this.DataKas;
            this.comboBoxJenisKas.DisplayMember = "NAMA";
            this.comboBoxJenisKas.ValueMember = "ID";
            this.comboBoxJenisKas.SelectedIndex= 0;
        }

        private void RefreshKodeBarang()
        {
            this.DataBarang = DbSearchLoadData.Barang();
        }
        #endregion
        
   
        #region ACtiveControl
        private void comboSatuan_Leave(object sender, EventArgs e)
        {
            this.ActiveControl = this.buttonAdd;
        }

        private void textBoxKeterangan_Leave(object sender, EventArgs e)
        {
            this.ActiveControl = this.comboBoxJenisKas;
        }

        private void comboBoxJenisKas_Leave(object sender, EventArgs e)
        {
            this.ActiveControl = buttonBawahProcess;
        }


        #endregion


        private void textNamaPelangganAtas_KeyDown(object sender, KeyEventArgs e)
        {

            if (this.textNamaPelangganAtas.Text != "Umum")
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.FilterDataPelanggan();
                }
            }
        }

        private void FilterDataPelanggan()
        {
            using (var form = new FormSearch())
            {
                form.FormName = "pelanggan";
                form.SearchText = this.textNamaPelangganAtas.Text;
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    this.PelangganIdTerpilih = form.SearchIndex;
                    this.PelangganNamaTerpilih = form.SearchedText;
                    this.textNamaPelangganAtas.Text = form.SearchedText;
                }
            }
        }

        private void textKode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.FilterDataKode();
            }
            else if (e.KeyCode == Keys.Down)
            {
                this.ActiveControl = this.dataGridView1;
            }
        }

        private void butCariKode_Click(object sender, EventArgs e)
        {
            this.FilterDataKode();
        }
        private void FilterDataKode()
        {
            using (var form = new FormSearch())
            {
                form.FormName = "kode";
                form.SearchText = this.textKode.Text;
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    this.KodeTerpilih = form.SearchedKode;
                    this.BarangIdTerpilih = form.SearchIndex;
                    this.textKode.Text = form.SearchedKode;
                    this.textNamaBarang.Text = form.SearchedText;
                    this.ActiveControl = this.textBoxQty;
                }
            }
        }

        private void textNamaBarang_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.FilterDataBarang();
            }
            else if (e.KeyCode == Keys.Down)
            {
                this.ActiveControl = this.dataGridView1;
            }
        }

        private void butCariBarang_Click(object sender, EventArgs e)
        {
            this.FilterDataBarang();
        }
        private void FilterDataBarang()
        {
            using (var form = new FormSearch())
            {
                form.FormName = "barang";
                form.SearchText = this.textNamaBarang.Text;
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    this.KodeTerpilih = form.SearchedKode;
                    this.BarangIdTerpilih = form.SearchIndex;
                    this.textKode.Text = form.SearchedKode;
                    this.textNamaBarang.Text = form.SearchedText;
                    this.ActiveControl = this.textBoxQty;
                }
            }
        }

        private void labelSubTotal_TextChanged(object sender, EventArgs e)
        {
            this.LatbelHargaTotal.Text = this.labelSubTotal.Text;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                double SubTotalPrice = this.ListBarang.AsEnumerable().Sum(row => row.Field<double>("Total"));
                double diskon;
                if (textBox1.Text.Length > 0)
                {
                    var r_ = double.TryParse(textBox1.Text, out diskon);
                    var totalHarga = (1.00 - diskon / 100) * SubTotalPrice;
                    this.LatbelHargaTotal.Text = totalHarga.ToString("C");
                }
            }
        }

        private void butCariKode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                this.ActiveControl = this.comboJenisBayar;
            }
        }

        private void KeyEvent(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F7:
                    this.buttonBawahProcess_Click(sender, e);
                    break;
                case Keys.F6:
                    this.buttonAdd_Click(sender, e);
                    break;
                case Keys.F5:
                    this.buttonPelanggan_Click(sender, e);
                    break;
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
    }
}
