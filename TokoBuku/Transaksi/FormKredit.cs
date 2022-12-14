using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TokoBuku.Transaksi.SearchForm;

namespace TokoBuku.Transaksi
{
    public partial class FormKredit : Form
    {
        /// <summary>
        ///  Form Menu Pembayaran Kredit:
        ///  Output: 
        ///     1. Pembayaran Awal / DP -> 
        ///     2. Tanggal Pesan
        ///     3. Tenggat Pembayaran
        ///     4. Nama Pelanggan
        ///     5. IdPelanggan
        /// </summary>

        public double PembayaranAwal { get; set; }
        public DateTime TglPesan { get; set; }
        public DateTime TenggatPembayaran { get; set; }
        public string NamaPelanggan { get; set; }
        public int IdPelanggan { get; set; }

        public FormKredit()
        {
            InitializeComponent();
        }

        private void buttonSimpan_Click(object sender, EventArgs e)
        {
            double dp = 0;
            DateTime datePesan, dateTenggatbayar;
            /*datePesan = dateTimePickerPemesanan.Value.Date;
            ShowErrMsg(datePesan.ToShortDateString());*/
            if (string.IsNullOrWhiteSpace(textBoxDP.Text) || !double.TryParse(textBoxDP.Text, out dp))
            {
                ShowErrMsg("PEMBAYARAN AWAL TIDAK BOLEH KOSONG ATAU BERUPA HURUF.");
            }
            else if (dateTimePickerPembayaran.Value.Date == dateTimePickerPemesanan.Value.Date)
            {
                ShowErrMsg("TANGGAL TENGGAT PEMBAYARAN TIDAK BOLEH SAMA DENGAN TANGGAL PEMESANAN.");
            }
            else if (string.IsNullOrWhiteSpace(textBoxPelanggan.Text))
            {
                ShowErrMsg("NAMA PELANGGAN TIDAK BOLEH KOSONG.");
            }
            else
            {
                this.PembayaranAwal = dp;
                this.TglPesan = dateTimePickerPemesanan.Value.Date;
                this.TenggatPembayaran = dateTimePickerPembayaran.Value.Date;
                this.NamaPelanggan = textBoxPelanggan.Text;
                this.buttonSimpan.DialogResult = DialogResult.Yes;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            //this.Close();
        }

        private void ShowErrMsg(string msg)
        {
            MessageBox.Show(msg, "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void buttonSearchPelanggan_Click(object sender, EventArgs e)
        {
            FormSearch formSearch = new FormSearch();
            formSearch.ShowDialog();
        }

        private void FormKredit_Load(object sender, EventArgs e)
        {
            this.dateTimePickerPembayaran.Value = DateTime.Now.Date.AddDays(14);

        }
    }
}
