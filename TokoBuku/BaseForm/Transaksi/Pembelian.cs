using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TokoBuku.BaseForm.Transaksi.SearchForm;

namespace TokoBuku.BaseForm.Transaksi
{
    public partial class Pembelian : Form
    {
        private DataTable ListBarangDibeli = new DataTable();

        public Pembelian()
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
            this.comboSatuan.SelectedIndex = 0;

            /// init datagridview
            this.dataGridView1.Columns["barang"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            ComboBox _satuan = new ComboBox();
            _satuan.Items.Add("Pcs");
            _satuan.Items.Add("Packs");
            ((DataGridViewComboBoxColumn)dataGridView1.Columns["satuan"]).DataSource = _satuan.Items;
            //DataGridViewNumericUpDownElements _jumlah = new DataGridViewNumericUpDownElements();
            /*this.ListBarangDibeli.Columns.Add("Kode", typeof(string));
            this.ListBarangDibeli.Columns.Add("Nama Barang", typeof(string));
            this.ListBarangDibeli.Columns.Add("Jumlah", typeof(double));
            this.ListBarangDibeli.Columns.Add("Satuan", typeof(string));*/

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


        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void dataGridView1_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells["jumlah"].Value = 1;
            e.Row.Cells["satuan"].Value = "Pcs";
        }

        private string CariKodeBarang()
        {

            return "";
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        { 
            int row_idx = e.RowIndex;
            int col_idx = e.ColumnIndex;
            string txt = "";
            bool isnul = string.IsNullOrWhiteSpace(dataGridView1[col_idx, row_idx].Value.ToString());
            if (!isnul)
            {
                txt = dataGridView1[col_idx, row_idx].Value.ToString();
            }
            MessageBox.Show(txt);
            dataGridView1[col_idx, row_idx].Value = "makan";
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            
        }
    }
}
