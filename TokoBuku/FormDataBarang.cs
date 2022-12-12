using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TokoBuku
{
    public partial class FormDataBarang : Form
    {
        bool SuccesSaved = false;
        public FormDataBarang()
        {
            InitializeComponent();
            SuccesSaved = false;
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {
            //toolTip1.Show("Tambah Item", buttonTambahRak);
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
                this.SuccesSaved = true;


                MessageBox.Show($"DATA TERSIMPAN:\nNAMA BARANG: {textBoxNamaBarang.Text}\n" +
                    $"HARGA: {textBoXHarga.Text} Rupiah.\n" +
                    $"DISKON: {textBoxDiskon.Text} %\n" +
                    $"RAK: {comboBoxRak.Text} \n" +
                    $"STOCK: {numericStock.Value} \n");
                this.Close();
            }



            //DialogResult res = MessageBox.Show("DATA BERHASIL DISIMPAN.\nAPAKAH ANDA INGIN MENAMBAH DATA YANG LAIN?", "success", MessageBoxButtons.YesNo, MessageBoxIcon.None);
            //if (res == DialogResult.No)
            //{
            //    this.Close();
            //}
        }

        private void buttonSaveData_ControlAdded(object sender, ControlEventArgs e)
        {
            buttonSaveData.Focus();
        }

        private void ShowErrorPrompt(string message)
        {
            MessageBox.Show(message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void FormDataBarang_Load(object sender, EventArgs e)
        {

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

        public bool GetSuccess()
        {
            return this.SuccesSaved;
        }
    }
}
