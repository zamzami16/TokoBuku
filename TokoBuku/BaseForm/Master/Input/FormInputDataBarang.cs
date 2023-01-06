using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using TokoBuku.BaseForm.TipeData.DataBase;
using TokoBuku.DbUtility;

namespace TokoBuku.BaseForm.Master.Input
{
    public partial class FormInputDataBarang : Form
    {
        ///Atribut Form
        public string NamaForm { get; set; }
        public string TitleForm { get; set; }

        #region Property access
        public string NamaBarang { get; set; }
        public double HargaBeli { get; set; }
        public double Harga { get; set; }
        public double Diskon { get; set; }
        public string Rak { get; set; }
        public int Stock { get; set; }
        public string Kategori { get; set; }
        public string Penerbit { get; set; }
        public string Penulis { get; set; }
        public string ISBN { get; set; }
        public string BarCode { get; set; }
        public string Status { get; set; }
        public string Keterangan { get; set; }
        public string KategoriText { get; set; }
        public string PenerbitText { get; set; }
        public string RakText { get; set; }
        public string KodeBarang { get; set; }
        #endregion

        public TBarang DbBarang { get; set; }

        public FormInputDataBarang()
        {
            InitializeComponent();
            this.NamaForm = "TAMBAH DATA BARANG";
            this.TitleForm = "TAMBAH DATA BARANG";
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSaveData_Click(object sender, EventArgs e)
        {
            // set variable for each control
            double hargaJual, diskon, hargaBeli;

            if (string.IsNullOrWhiteSpace(textBoxNamaBarang.Text))
            {
                ShowErrorPrompt("NAMA BARANG TIDAK BOLEH KOSONG");
            }
            else if (string.IsNullOrEmpty(textBoxHargaBeli.Text) || !double.TryParse(textBoxHargaBeli.Text, out hargaBeli))
            {
                ShowErrorPrompt("HARGA BELI TIDAK BOLEH KOSONG ATAU BERUPA HURUF");
            }
            // for harga barang
            else if (string.IsNullOrWhiteSpace(textBoXHarga.Text) || !double.TryParse(textBoXHarga.Text, out hargaJual))
            {
                ShowErrorPrompt("HARGA JUAL TIDAK BOLEH KOSONG ATAU BERUPA HURUF");
            }
            else if (!double.TryParse(textBoxDiskon.Text, out diskon))
            {
                ShowErrorPrompt("DISKON TIDAK BOLEH BERUPA HURUF.");
            }
            else if (string.IsNullOrWhiteSpace(comboBoxRak.Text))
            {
                ShowErrorPrompt("DATA RAK TIDAK BOLEH KOSONG");
            }
            else if (numericStock.Value <= 0)
            {
                ShowErrorPrompt("JUMLAH STOCK MINIMAL 1.");
            }
            else if (string.IsNullOrWhiteSpace(comboBoxKategori.Text))
            {
                ShowErrorPrompt("KATEGORI TIDAK BOLEH KOSONG");
            }
            else if (string.IsNullOrWhiteSpace(comboBoxPenerbit.Text))
            {
                ShowErrorPrompt("DATA PENERBIT TIDAK BOLEH KOSONG");
            }
            else
            {
                if (hargaBeli > ((1.0 - (diskon / 100.0)) * hargaJual))
                {
                    if (MessageBox.Show("Harga setelah diskon lebih sedikit dari harga jual. Tetap Lanjutkan?", "Lanjut?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, defaultButton: MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        this.DbBarang.IdKategori = Convert.ToInt32(this.comboBoxKategori.SelectedValue.ToString());
                        this.DbBarang.IdPenerbit = Convert.ToInt32(this.comboBoxPenerbit.SelectedValue.ToString());
                        this.DbBarang.IdRak = Convert.ToInt32(this.comboBoxRak.SelectedValue.ToString());
                        this.DbBarang.Kode = textBoxKode.Text;
                        this.DbBarang.NamaBarang = textBoxNamaBarang.Text;
                        this.DbBarang.Stock = Convert.ToDouble(numericStock.Value.ToString());
                        this.DbBarang.HargaJual = hargaJual;
                        this.DbBarang.HargaBeli = hargaBeli;
                        this.DbBarang.ISBN = textBoxISBN.Text;
                        this.DbBarang.Penulis = textBoxPenulis.Text;
                        this.DbBarang.Diskon = diskon;
                        this.DbBarang.Status = TStatus.Aktif;
                        this.DbBarang.BarCode = this.textBoxBarCode.Text;
                        this.DbBarang.Keterangan = this.richTextBoxKeterangan.Text;

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                else
                {
                    this.DbBarang.IdKategori = Convert.ToInt32(this.comboBoxKategori.SelectedValue.ToString());
                    this.DbBarang.IdPenerbit = Convert.ToInt32(this.comboBoxPenerbit.SelectedValue.ToString());
                    this.DbBarang.IdRak = Convert.ToInt32(this.comboBoxRak.SelectedValue.ToString());
                    this.DbBarang.Kode = textBoxKode.Text;
                    this.DbBarang.NamaBarang = textBoxNamaBarang.Text;
                    this.DbBarang.Stock = Convert.ToDouble(numericStock.Value.ToString());
                    this.DbBarang.HargaJual = hargaJual;
                    this.DbBarang.HargaBeli = hargaBeli;
                    this.DbBarang.ISBN = textBoxISBN.Text;
                    this.DbBarang.Penulis = textBoxPenulis.Text;
                    this.DbBarang.Diskon = diskon;
                    this.DbBarang.Status = TStatus.Aktif;
                    this.DbBarang.BarCode = this.textBoxBarCode.Text;
                    this.DbBarang.Keterangan = this.richTextBoxKeterangan.Text;

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        


        private void ShowErrorPrompt(string message)
        {
            MessageBox.Show(message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void FormDataBarang_Load(object sender, EventArgs e)
        {
            ///
            this.ActiveControl = this.textBoxNamaBarang;
            /// Add Member to Rak
            this.RefreshComboRak();

            /// Add Member to Kategori
            this.RefreshComboKategori();

            /// Add member to Penerbit
            this.RefreshComboPenerbit();

            /// Generate Barcode
            this.GenerateBarCode();

            this.DbBarang = new TBarang();
        }

        private void buttonTambahRak_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("FITUR INI MASIH TAHAP PENGEMBANGAN.\nSILAKAN UNTUK TAMBAH MANUAL DARI MENU INPUT DATA", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            using (var form = FormInput.Rak())
            {
                form.ShowDialog();
                if (form.DialogResult == DialogResult.OK)
                {
                    var nama = form.ValueName;
                    var keterangan = form.ValueKeterangan;

                    try
                    {
                        var id = TokoBuku.DbUtility.DbSaveData.Rak(nama: nama, keterangan: keterangan, status: "AKTIF");
                        this.RefreshComboRak();
                        this.comboBoxRak.SelectedValue = id;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //throw;
                    }
                }
                else
                {
                    this.RefreshComboRak();
                }
            }
        }

        private void buttonTambahKategori_Click(object sender, EventArgs e)
        {
            using (var form = FormInput.Kategori())
            {
                form.ShowDialog();
                if (form.DialogResult == DialogResult.OK)
                {
                    var nama = form.ValueName;
                    var keterangan = form.ValueKeterangan;

                    try
                    {
                        var id = TokoBuku.DbUtility.DbSaveData.Kategori(nama: nama, keterangan: keterangan, status: "AKTIF");
                        this.RefreshComboKategori();
                        this.comboBoxKategori.SelectedValue = id;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //throw;
                    }
                }
                else
                {
                    this.RefreshComboKategori();
                }
            }

        }

        private void buttonTambahPenerbit_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("FITUR INI MASIH TAHAP PENGEMBANGAN.\nSILAKAN UNTUK TAMBAH MANUAL DARI MENU INPUT DATA", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            using (var form = FormInput.Penerbit())
            {
                form.ShowDialog();
                if (form.DialogResult == DialogResult.OK)
                {
                    var nama = form.ValueName;
                    var keterangan = form.ValueKeterangan;

                    try
                    {
                        var id = TokoBuku.DbUtility.DbSaveData.Penerbit(nama: nama, keterangan: keterangan, status: "AKTIF");
                        this.RefreshComboPenerbit();
                        this.comboBoxPenerbit.SelectedValue = id;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //throw;
                    }
                }
                else
                {
                    this.RefreshComboPenerbit();
                }
            }

        }

        public void SetToEditForm()
        {
            this.Text = this.NamaForm;
            this.labelTitle.Text = this.TitleForm;
            this.textBoxNamaBarang.Text = this.NamaBarang;
            this.textBoxKode.Text = this.KodeBarang;
            this.comboBoxKategori.Text = this.Kategori;
            this.comboBoxPenerbit.Text = this.Penerbit;
            this.comboBoxRak.Text = this.Rak;
            this.numericStock.Value = this.Stock;
            this.textBoXHarga.Text = this.Harga.ToString();
            this.textBoxHargaBeli.Text = this.HargaBeli.ToString();
            this.textBoxISBN.Text = this.ISBN;
            this.textBoxPenulis.Text = this.Penulis;
            this.textBoxDiskon.Text = this.Diskon.ToString();
            this.textBoxBarCode.Text = this.BarCode;
            this.richTextBoxKeterangan.Text = this.Keterangan;
            this.textBoxKode.Enabled = false;
            this.numericStock.Enabled = false;
            this.textBoxHargaBeli.Enabled = false;
            this.buttonGenerateKode.Enabled = false;
        }

        private void buttonGenerateKode_Click(object sender, EventArgs e)
        {
            this.textBoxKode.Text = "--OTOMATIS--";
        }

        private void buttonGenerateBarCode_Click(object sender, EventArgs e)
        {
            this.GenerateBarCode();
        }

        private void GenerateBarCode()
        {
            var dt = TokoBuku.DbUtility.Etc.GenerateBarCode();
            Random rnd = new Random();
            while (true)
            {
                string calon_barcode = rnd.Next().ToString();
                bool contains = dt.AsEnumerable().Any(row => calon_barcode == row.Field<String>("BARCODE"));
                if (!contains)
                {
                    this.textBoxBarCode.Text = calon_barcode;
                    break;
                }
            }
        }

        #region Active Control
        private void textBoxNamaBarang_Leave(object sender, EventArgs e)
        {
            //this.ActiveControl = this.textBoxKode;
        }
        private void textBoxKode_Leave(object sender, EventArgs e)
        {
            //this.ActiveControl = this.textBoXHarga;
        }

        #endregion

        #region Refresh Combo box
        /// Rak
        private void RefreshComboRak()
        {
            comboBoxRak.DataSource = DbLoadData.Rak(); ;
            this.comboBoxRak.DisplayMember = "NAMA";
            this.comboBoxRak.ValueMember = "ID";
        }
        private void RefreshComboKategori()
        {
            var xx = DbLoadData.Kategori();
            this.comboBoxKategori.DataSource = DbLoadData.Kategori();
            this.comboBoxKategori.DisplayMember = "NAMA";
            this.comboBoxKategori.ValueMember = "ID";
        }
        private void RefreshComboPenerbit()
        {
            this.comboBoxPenerbit.DataSource = DbLoadData.Penerbit();
            this.comboBoxPenerbit.DisplayMember = "NAMA_PENERBIT";
            this.comboBoxPenerbit.ValueMember = "ID";
        }
        #endregion
    }
}
