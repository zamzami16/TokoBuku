using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TokoBuku.BaseForm.TipeData.DataBase;
using TokoBuku.BaseForm.Transaksi.SearchForm;
using TokoBuku.DbUtility;
using TokoBuku.DbUtility.Master;
using TokoBuku.DbUtility.Transactions;
using TokoBuku.Login;
using TextBox = System.Windows.Forms.TextBox;

namespace TokoBuku.BaseForm.Transaksi
{
    public partial class Penjualan : Form
    {
        private DataTable DataBarang;
        private DataTable DataKas;
        private DataTable DataPelanggan;
        private DataTable ListBarang = new DataTable();
        private string PelangganNamaTerpilih;
        private int PelangganIdTerpilih;
        private string KodeTerpilih;
        private int BarangIdTerpilih;
        private int IdKasir { get; set; }
        private string NamaKasir { set; get; }

        public Penjualan(int idKasir, string namaKasir)
        {
            InitializeComponent();
            this.IdKasir = idKasir;
            this.NamaKasir= namaKasir;
        }

        private void comboJenisBayar_SelectedValueChanged(object sender, EventArgs e)
        {
            var x_ = (TJenisPembayaran)Enum.Parse(typeof(TJenisPembayaran), this.comboJenisBayar.Text);
            if (x_ == TJenisPembayaran.Kredit)
            {
                this.dateTimePickerJatuhTempo.Enabled = true;
                this.comboBoxJenisKas.Enabled = false;
                //this.labelKembalian.Enabled = false;
                //this.labelkembali.Enabled = false;
                this.labelDp.Text = "Pembayaran Awal :";
                this.labelkembali.Text = "Jumlah Kekurangan";
            }
            else if (x_ == TJenisPembayaran.Cash)
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

            this.textBoxPotongan.Text = 0.ToString("N2");
            this.textBoxPembayaranAwal.Text = 0.ToString("N2");

            this.comboJenisBayar.DataSource = Enum.GetValues(typeof(TJenisPembayaran));
            this.comboJenisBayar.SelectedIndex = 0;

            // Set label kasir
            this.labelKasir.Text = this.NamaKasir;
        }

        private void buttonBawahProcess_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                MessageBox.Show("Pilih barang terlebih dahulu.", "Gagal.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.ActiveControl = this.textKode;
            }
            /*else if (string.IsNullOrWhiteSpace(this.comboBoxJenisKas.Text))
            {
                MessageBox.Show("Pilih jenis kas dulu.", "Warning.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.ActiveControl = this.comboBoxJenisKas;
            }*/
            else
            {
                var x_ = (TJenisPembayaran)Enum.Parse(typeof(TJenisPembayaran), this.comboJenisBayar.Text);
                if (x_ == TJenisPembayaran.Cash)
                {
                    this.ProsedurPenjualanCash();
                }
                /// Kredit
                else if (x_ == TJenisPembayaran.Kredit)
                {
                    double totalPembayaranAwal = 0;
                    if (!double.TryParse(this.textBoxPembayaranAwal.Text, out totalPembayaranAwal) && totalPembayaranAwal < 0)
                    {
                        totalPembayaranAwal = 0;
                        this.textBoxPembayaranAwal.Text = "0";
                    }
                    else if (this.GetKembalian() >= 0 && totalPembayaranAwal > 0)
                    {
                        var results = MessageBox.Show("Jumlah uang pembayaran bisa digunakan untuk pembayaran CASH.\nApakah anda mau menggunakan metode pembayaran CASH?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (results == DialogResult.Yes)
                        {
                            this.comboJenisBayar.SelectedIndex = 0;
                            /// TODO: -No Priority-lanjutkan untuk pembayaran cash
                        }
                    }
                    else if (this.textNamaPelangganAtas.Text.ToLower() == "umum" || this.PelangganIdTerpilih == TokoBuku.DbUtility.Transactions.Penjualan.GetIdPelangganUmum())
                    {
                        this.textNamaPelangganAtas.Text = string.Empty;
                        MessageBox.Show("Pelanggan tidak boleh Umum.\nPilih nama pelanggan terlebih dahulu.", "Warning.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.ActiveControl = this.textNamaPelangganAtas;
                        this.buttonPelanggan_Click(null, null);
                    }
                    else
                    {
                        this.ProsedurPenjualanKredit();
                    }
                }
            }
        }

        private void ProsedurPenjualanKredit()
        {
            string id_kas = 0.ToString();
            if (this.comboBoxJenisKas.Enabled == true)
            {
                id_kas = this.comboBoxJenisKas.SelectedValue.ToString();
            }
            try
            {

                TPenjualan penjualan = new TPenjualan();
                penjualan.KodeTransaksi = this.labelNoTransaksi.Text;
                penjualan.IdKasir = this.IdKasir; /// TODO: Ganti dengan system login
                penjualan.IdPelanggan = this.PelangganIdTerpilih;
                penjualan.Total = this.GetTotalHarga();
                penjualan.UangPembayaran = Convert.ToDouble(this.textBoxPembayaranAwal.Text);
                penjualan.UangKembalian = 0;
                penjualan.Potongan = this.GetPotongan();
                penjualan.Tanggal = this.dateTimePickerTglPesanan.Value;
                penjualan.Waktu = DateTime.Now;
                penjualan.StatusPembayaran = TJenisPembayaran.Kredit;
                if (penjualan.UangPembayaran > 0)
                {
                    penjualan.IdKas = Convert.ToInt32(this.comboBoxJenisKas.SelectedValue.ToString());
                }
                penjualan.Keterangan = this.textBoxKeterangan.Text;

                TPiutang piutang = new TPiutang()
                {
                    Total = this.GetTotalHarga() - penjualan.UangPembayaran,
                    IdPelanggan = penjualan.IdPelanggan,
                    TanggalTenggatBayar = dateTimePickerJatuhTempo.Value,
                    Lunas = TLunas.Belum
                };
                TokoBuku.DbUtility
                    .Transactions.Penjualan.SaveKredit(penjualan, this.ConvertDGVToDetailPenjualan(this.dataGridView1), piutang: piutang);
                MessageBox.Show("Data Berhasil disimpan.", "Success.");
                this.ResetForm();
                this.GenerateNoTransaksi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void ProsedurPenjualanCash()
        {
            double temp_TotalBayar = 0;
            if (this.GetKembalian() >= 0 && double.TryParse(this.textBoxPembayaranAwal.Text, out temp_TotalBayar) && temp_TotalBayar > 0)
            {
                try
                {
                    TPenjualan penjualan = new TPenjualan();
                    penjualan.KodeTransaksi = this.labelNoTransaksi.Text;
                    penjualan.IdKasir = this.IdKasir; /// TODO: Ganti dengan system login
                    penjualan.IdPelanggan = this.PelangganIdTerpilih;
                    penjualan.Total = this.GetTotalHarga();
                    penjualan.UangPembayaran = temp_TotalBayar;
                    penjualan.UangKembalian = this.GetKembalian();
                    penjualan.Potongan = this.GetPotongan();
                    penjualan.Tanggal = this.dateTimePickerTglPesanan.Value;
                    penjualan.Waktu = DateTime.Now;
                    penjualan.StatusPembayaran = TJenisPembayaran.Cash;
                    penjualan.IdKas = Convert.ToInt32(this.comboBoxJenisKas.SelectedValue.ToString());
                    penjualan.Keterangan = this.textBoxKeterangan.Text;

                    DbUtility.Transactions.Penjualan.SaveCash(penjualan, detailPenjualan: ConvertDGVToDetailPenjualan(this.dataGridView1));
                    MessageBox.Show("Data Berhasil disimpan.", "Success.");
                    this.ResetForm();
                    this.GenerateNoTransaksi();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw;
                }
            }
            else
            {
                MessageBox.Show("Masukkan Uang Pembayaran terlebih dahulu.", "warning.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.ActiveControl = this.textBoxPembayaranAwal;
            }
        }

        private List<TDetailPenjualan> ConvertDGVToDetailPenjualan(DataGridView dgv)
        {
            List<TDetailPenjualan> detailPenjualan = new List<TDetailPenjualan>();
            foreach (DataGridViewRow row in dgv.Rows)
            {
                TDetailPenjualan detail = new TDetailPenjualan();
                detail.IdBarang = Convert.ToInt32(row.Cells["Id"].Value.ToString());
                detail.Jumlah = Convert.ToDouble(row.Cells["Jumlah"].Value.ToString());
                detail.HargaJual = Convert.ToDouble(row.Cells["Harga"].Value.ToString());
                detail.Satuan = row.Cells["Satuan"].Value.ToString();
                detailPenjualan.Add(detail);
            }
            return detailPenjualan;
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
        private void ResetForm()
        {
            this.ListBarang.Clear();
            this.dataGridView1.DataSource = this.ListBarang;
            this.LatbelHargaTotal.Text = "0";
            this.labelSubTotal.Text = "0";
            this.textBoxPembayaranAwal.Text = "0";
            this.labelKembalian.Text = "0";
            this.textBoxPotongan.Text = "0";
            this.comboBoxJenisKas.SelectedIndex = 0;
            this.comboJenisBayar.SelectedIndex = 0;
            this.textNamaPelangganAtas.Text = "UMUM";
            this.PelangganIdTerpilih = TokoBuku.DbUtility.Transactions.Penjualan.GetIdPelangganUmum();
            this.textBoxKeterangan.Text = string.Empty;
        }


        private void textBoxPembayaranAwal_TextChanged(object sender, EventArgs e)
        {
            float _pembayaranAwal = 0;
            comboBoxJenisKas.Enabled = true;
            if (!string.IsNullOrWhiteSpace(textBoxPembayaranAwal.Text) && float.TryParse(textBoxPembayaranAwal.Text, out _pembayaranAwal))
            {
                if (_pembayaranAwal > 0)
                {
                    this.UpdateLabelKembalian();
                }
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            double stock_db;
            if (string.IsNullOrWhiteSpace(textNamaBarang.Text))
            {
                MessageBox.Show("Pilih Kode atau Nama Barang dulu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.ActiveControl = this.textKode;
            }
            else if (!IsValidStock(out stock_db))
            {
                MessageBox.Show($"Stock Barang tidak cukup!\nStock barang tinngal {stock_db} Pcs. (1 Packs = 10 Pcs)", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.ActiveControl = this.textBoxQty;
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

                // Update label subtotal harga
                this.UpdateLabelSubTotal();

                // Reset input add barang
                this.textNamaBarang.Text = string.Empty;
                this.textKode.Text = string.Empty;
                this.textBoxQty.Text = "1";
                this.comboSatuan.SelectedIndex = 0;
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

        #region RefreshDataBarangPelangganKas
        private void RefreshDataPelanggan()
        {/// data pelanggan di buat ke umum
            this.PelangganIdTerpilih = DbUtility.Transactions.Penjualan.GetIdPelangganUmum();
            this.textNamaPelangganAtas.Text = "UMUM";
        }

        private void RefreshDataBarang()
        {
            this.DataBarang = DbSearchLoadData.Barang();
        }

        private void RefreshDataKas()
        {
            this.DataKas = DbSearchLoadData.Kas();
            if (this.DataKas.Rows.Count < 1)
            {
                DataRow drow = this.DataKas.NewRow();
                drow["ID"] = -1;
                drow["NAMA"] = "Kosong";
                this.DataKas.Rows.Add(drow);
            }
            this.comboBoxJenisKas.DataSource = this.DataKas;
            this.comboBoxJenisKas.DisplayMember = "NAMA";
            this.comboBoxJenisKas.ValueMember = "ID";
            this.comboBoxJenisKas.SelectedIndex = 0; 

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
                DataTable data = TokoBuku.DbUtility.Transactions.Penjualan.GetKodeBarang(this.textKode.Text);
                if (data.Rows.Count > 0)
                {
                    int id = data.Rows[0].Field<int>("ID");
                    string kode = data.Rows[0].Field<string>(1);
                    string nama = data.Rows[0].Field<string>(2);
                    this.BarangIdTerpilih = id;
                    this.textKode.Text = kode;
                    this.textNamaBarang.Text = nama;
                    this.ActiveControl = this.textBoxQty;
                }
                else { this.FilterDataKode(); }
            }
        }

        private void butCariKode_Click(object sender, EventArgs e)
        {
            this.FilterDataKode();
        }
        private void FilterDataKode()
        {
            /*using (var form = new FormSearch())
            {
                form.HideAddBarang();
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
            }*/

            using (var form = new FormSearchBarang())
            {
                form.FormName = "kode";
                form.SearchText = this.textKode.Text;
                form.TipeForm = "penjualan";
                form.HideAddBarang();
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    this.KodeTerpilih = form.SearchBarang.Kode;
                    this.BarangIdTerpilih = form.SearchBarang.Id;
                    this.textKode.Text = form.SearchBarang.Kode;
                    this.textNamaBarang.Text = form.SearchBarang.Nama;
                    this.ActiveControl = this.textBoxQty;
                }
            }
        }

        private void textNamaBarang_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable data = TokoBuku.DbUtility.Transactions.Penjualan.GetNamaBarang(this.textNamaBarang.Text);
                if (data.Rows.Count > 0)
                {
                    int id = data.Rows[0].Field<int>("ID");
                    string kode = data.Rows[0].Field<string>(1);
                    string nama = data.Rows[0].Field<string>(2);
                    this.BarangIdTerpilih = id;
                    this.textKode.Text = kode;
                    this.textNamaBarang.Text = nama;
                    this.ActiveControl = this.textBoxQty;
                }
                else { this.FilterDataBarang(); }
            }
        }

        private void butCariBarang_Click(object sender, EventArgs e)
        {
            this.FilterDataBarang();
        }
        private void FilterDataBarang()
        {
            using (var form = new FormSearchBarang())
            {
                form.FormName = "barang";
                form.SearchText = this.textNamaBarang.Text;
                form.TipeForm = "penjualan";
                form.HideAddBarang();
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    this.KodeTerpilih = form.SearchBarang.Kode;
                    this.BarangIdTerpilih = form.SearchBarang.Id;
                    this.textKode.Text = form.SearchBarang.Kode;
                    this.textNamaBarang.Text = form.SearchBarang.Nama;
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
                this.UpdateLabelTotalHarga();
            }
            else
            {
                this.txtRealBox_KeyDown(sender, e);
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

        
        /// <summary>
        /// Update label total harga
        /// </summary>
        private void UpdateLabelTotalHarga()
        {
            this.LatbelHargaTotal.Text = this.GetTotalHarga().ToString("C");
        }

        /// <summary>
        /// Get Total Harga barang. (Subtotal - Potongan)
        /// </summary>
        /// <returns>Total Harga</returns>
        private double GetTotalHarga()
        {
            double totalHarga = GetSubTotal() - GetPotongan();
            return totalHarga;
        }

        /// <summary>
        /// Get potongan harga
        /// </summary>
        /// <returns>Potongan harga / Diskon</returns>
        private double GetPotongan()
        {
            double subTotal = GetSubTotal();
            double diskon = 0;
            if (subTotal > 0 && textBoxPotongan.Text.Length > 0)
            {
                if (!double.TryParse(textBoxPotongan.Text, out diskon)) { diskon = 0; }
            }
            return diskon;
        }

        /// <summary>
        /// Get subtotal harga
        /// </summary>
        /// <returns>subtotal harga</returns>
        private double GetSubTotal()
        {
            double SubTotalPrice = this.ListBarang.AsEnumerable().Sum(row => row.Field<double>("Total"));
            return SubTotalPrice;
        }

        /// <summary>
        /// Update label subtotal
        /// </summary>
        private void UpdateLabelSubTotal()
        {
            this.labelSubTotal.Text= this.GetSubTotal().ToString("C");
        }

        private void UpdateLabelKembalian()
        {
            this.labelKembalian.Text = this.GetKembalian().ToString("C");
        }

        private double GetKembalian()
        {
            double _totalPembayaran;
            double.TryParse(this.textBoxPembayaranAwal.Text, out _totalPembayaran);
            return _totalPembayaran - this.GetTotalHarga();
        }

        /// <summary>
        /// Get stock barang masih apa tidak
        /// </summary>
        /// <param name="stock_db"></param>
        /// <returns>bool="Stock valid or not"</returns>
        private bool IsValidStock(out double stock_db)
        {
            /// cek stock di DB
            stock_db = TokoBuku.DbUtility.Transactions.Penjualan.GetStockBarang(id_barang: this.BarangIdTerpilih);
            double stock_textBox;
            if (!double.TryParse(this.textBoxQty.Text, out stock_textBox))
            {
                this.textBoxQty.Text = "1";
                return false;
            }
            else if (this.comboSatuan.Text.ToLower() == "packs")
            {
                stock_textBox *= 10;
            }
            if ((stock_db - stock_textBox) >= 0)
            {
                return true;
            }
            return false;
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

        private void buttonHistoriPenjualan_Click(object sender, EventArgs e)
        {
            using (HistoriPenjualan histori = new HistoriPenjualan())
            {
                histori.ShowDialog();
            }
        }

        private void textBoxQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.ActiveControl = this.buttonAdd;
            }
        }

        private void textBoxPotongan_TextChanged(object sender, EventArgs e)
        {
            this.UpdateLabelTotalHarga();
            this.UpdateLabelKembalian();
        }

        private void buttonAdmin_Click(object sender, EventArgs e)
        {
            using (var formLogin = new FormLogin())
            {
                if (formLogin.ShowDialog() == DialogResult.OK)
                {
                    if (formLogin.NamaKasir.ToLower() == "admin")
                    {
                        ((TokoBukuWindows)this.MdiParent).SetKasirTerpilih(Convert.ToInt32(formLogin.IdKasir), formLogin.NamaKasir);
                        ((TokoBukuWindows)this.MdiParent).SetAdmin(true);
                        this.UpdateKasir(Convert.ToInt32(formLogin.IdKasir), formLogin.NamaKasir);
                    }
                }
            }
        }

        private void UpdateKasir(int idKasir, string namaKasir)
        {
            this.IdKasir= idKasir;
            this.NamaKasir= namaKasir;
            this.labelKasir.Text = namaKasir;   
        }
    }
}
