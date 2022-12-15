using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TokoBuku.DbUtility;

namespace TokoBuku.BaseForm.Master.Input
{
    public partial class FormInputDataBarang : Form
    {
        public bool SuccesSaved { get; set; }
        ///Atribut Form
        ///
        public string NamaForm { get; set; }
        public string TitleForm { get; set; }


        #region Property access
        public string NamaBarang { get; set; }
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
        #endregion



        public FormInputDataBarang()
        {
            InitializeComponent();
            this.SuccesSaved = false;
            this.NamaBarang = "TAMBAH DATA BARANG";
            this.TitleForm = "TAMBAH DATA BARANG";
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void buttonSaveData_Click(object sender, EventArgs e)
        {
            // set variable for each control
            double harga, diskon;


            if (string.IsNullOrWhiteSpace(textBoxNamaBarang.Text))
            {
                ShowErrorPrompt("NAMA BARANG TIDAK BOLEH KOSONG");
            }
            // for harga barang
            else if (string.IsNullOrWhiteSpace(textBoXHarga.Text) || !double.TryParse(textBoXHarga.Text, out harga))
            {
                ShowErrorPrompt("HARGA TIDAK BOLEH KOSONG ATAU BERUPA HURUF");
            }
            else if (string.IsNullOrWhiteSpace(textBoxDiskon.Text) || !double.TryParse(textBoxDiskon.Text, out diskon))
            {
                ShowErrorPrompt("DISKON TIDAK BOLEH KOSONG ATAU BERUPA HURUF");
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
            else if (string.IsNullOrWhiteSpace(textBoxBarCode.Text))
            {
                ShowErrorPrompt("DATA BARCODE TIDAK BOLEH KOSONG");
            }
            else if (string.IsNullOrWhiteSpace(comboBoxStatus.Text))
            {
                comboBoxStatus.Text = "AKTIF";
            }
            else
            {
                this.NamaBarang = textBoxNamaBarang.Text;
                this.Harga = harga;
                this.Diskon = diskon;
                this.Rak = this.comboBoxRak.SelectedValue.ToString();
                this.Stock = Convert.ToInt32(numericStock.Value.ToString());
                this.Kategori = this.comboBoxKategori.SelectedValue.ToString();
                this.Penerbit = this.comboBoxPenerbit.SelectedValue.ToString();
                this.Penulis = this.textBoxPenulis.Text;
                this.ISBN = this.textBoxISBN.Text;
                this.BarCode = this.textBoxBarCode.Text;
                this.Keterangan = this.richTextBoxKeterangan.Text;
                this.Status = "AKTIF";
                this.KategoriText = this.comboBoxKategori.Text;
                this.RakText = this.comboBoxRak.Text;
                this.PenerbitText = this.comboBoxPenerbit.Text;

                this.DialogResult = DialogResult.OK;
                this.SuccesSaved = true;
                this.Close();
            }
        }


        private void ShowErrorPrompt(string message)
        {
            MessageBox.Show(message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void FormDataBarang_Load(object sender, EventArgs e)
        {
            /// Add Member to Rak
            /// 
            var rakTable = new DataTable();
            rakTable = DbLoadData.Rak(ConnectDB.Connetc());
            this.comboBoxRak.DataSource = rakTable;
            this.comboBoxRak.DisplayMember = "NAMA";
            this.comboBoxRak.ValueMember = "ID";

            /// Add Member to Kategori
            /// 
            var KategoriTable = DbLoadData.Kategori(ConnectDB.Connetc());
            this.comboBoxKategori.DataSource = KategoriTable;
            this.comboBoxKategori.DisplayMember = "NAMA";
            this.comboBoxKategori.ValueMember = "ID";

            /// Add member to Penerbit
            /// 
            var PenerbitTable = DbLoadData.Penerbit(ConnectDB.Connetc());
            this.comboBoxPenerbit.DataSource = PenerbitTable;
            this.comboBoxPenerbit.DisplayMember = "NAMA_PENERBIT";
            this.comboBoxPenerbit.ValueMember = "ID";
        }

        private void buttonTambahRak_Click(object sender, EventArgs e)
        {
            MessageBox.Show("FITUR INI MASIH TAHAP PENGEMBANGAN.\nSILAKAN UNTUK TAMBAH MANUAL DARI MENU INPUT DATA", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonTambahKategori_Click(object sender, EventArgs e)
        {
            MessageBox.Show("FITUR INI MASIH TAHAP PENGEMBANGAN.\nSILAKAN UNTUK TAMBAH MANUAL DARI MENU INPUT DATA", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void buttonTambahPenerbit_Click(object sender, EventArgs e)
        {
            MessageBox.Show("FITUR INI MASIH TAHAP PENGEMBANGAN.\nSILAKAN UNTUK TAMBAH MANUAL DARI MENU INPUT DATA", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void richTextBoxKeterangan_Move(object sender, EventArgs e)
        {
            //this.ActiveControl = this.buttonSaveData;
            this.buttonSaveData.Focus();
        }

        public void SetToEditForm()
        {
            this.Text = this.NamaForm;
            this.labelTitle.Text = this.TitleForm;
            this.textBoxNamaBarang.Text = this.NamaBarang;
            this.comboBoxKategori.Text = this.Kategori;
            this.comboBoxPenerbit.Text = this.Penerbit;
            this.comboBoxRak.Text = this.Rak;
            this.numericStock.Value = this.Stock;
            this.textBoXHarga.Text = this.Harga.ToString();
            this.textBoxISBN.Text = this.ISBN;
            this.textBoxPenulis.Text = this.Penulis;
            this.textBoxDiskon.Text = this.Diskon.ToString();
            this.textBoxBarCode.Text = this.BarCode;
        }
    }
}
