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
    public partial class Penjualan : Form
    {
        public Penjualan()
        {
            InitializeComponent();
        }

        private void textBoxKodeItem_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void comboJenisBayar_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboJenisBayar.Text == "KREDIT")
            {
                this.dateTimePickerJatuhTempo.Enabled = true;
                this.comboBoxJenisKas.Enabled = false;
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
            this.Close();
        }

        private void buttonPelanggan_Click(object sender, EventArgs e)
        {
            FormSearch formSearch = new FormSearch();
            formSearch.ShowDialog();
        }

        private void Penjualan_Load(object sender, EventArgs e)
        {
            this.ActiveControl = this.textBoxKodeItem;
            this.dateTimePickerJatuhTempo.Value = DateTime.Now.AddDays(7);
            this.dateTimePickerJatuhTempo.Enabled = false;
            this.comboJenisBayar.SelectedIndex = 0;
        }


        private void buttonBawahProcess_Click(object sender, EventArgs e)
        {

        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            double harga = 0;
            if (!string.IsNullOrWhiteSpace(textBox2.Text) && double.TryParse(textBox2.Text, out harga))
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

        private void comboSatuan_Leave(object sender, EventArgs e)
        {
            this.ActiveControl = this.buttonAdd;
        }
    }
}
