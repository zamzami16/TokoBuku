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

namespace TokoBuku.BaseForm.Transaksi.Resource
{
    public partial class PenjualanBk : Form
    {
        private DataTable DataPenjualan;
        private DataTable DataBarang;
        private DataTable DataKas;
        private DataTable DataPelanggan;
        private DataTable ListBarang = new DataTable();
        public PenjualanBk()
        {
            InitializeComponent();
            
        }

        private void textBoxKodeItem_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboJenisBayar_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboJenisBayar.Text == "KREDIT")
            {
                this.dateTimePickerJatuhTempo.Enabled = true;
                this.comboBoxJenisKas.Enabled = false;
                this.labelKembalian.Enabled = false;
                this.labelkembali.Enabled = false;
                this.labelDp.Text = "Pembayaran Awal :";
            }
            else
            {
                this.dateTimePickerJatuhTempo.Enabled = false;
                this.comboBoxJenisKas.Enabled = true;
                this.labelKembalian.Enabled = true;
                this.labelkembali.Enabled = true;
                this.labelDp.Text = "Cash :";
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
            this.ActiveControl = this.textBoxNamaItem;
            this.dateTimePickerJatuhTempo.Value = DateTime.Now.AddDays(7);
            this.dateTimePickerJatuhTempo.Enabled = false;
            this.comboJenisBayar.SelectedIndex = 0;
            this.labelNoTransaksi.Text = this.GenerateNoTransaksi();

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
            //this.dataGridView1.Columns[7].FillWeight = 20;
            //this.dataGridView1.Columns[7].

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

        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            float harga = 0;
            if (comboJenisBayar.Text == "KREDIT")
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
                //this.textBoxPembayaranAwal.Text = harga.ToString("C");
            }
            
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxNamaItem.Text))
            {
                MessageBox.Show("Pilih Nama Barang dulu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.ActiveControl = this.textBoxNamaItem;
            }
            else
            {
                int idBarang = Convert.ToInt32(textBoxNamaItem.SelectedValue.ToString());
                double qty = Convert.ToDouble(textBoxQty.Text);
                var satuan = comboSatuan.Text;
                var dt = GetDataPartial.Barang(idBarang, qty, satuan);
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

                this.textBoxNamaItem.Text = string.Empty;
                this.textBoxQty.Text = "1";
                double SubTotalPrice = this.ListBarang.AsEnumerable().Sum(row => row.Field<double>("Total"));
                this.labelSubTotal.Text = SubTotalPrice.ToString("C");
            }
        }

        private string GenerateNoTransaksi()
        {
            var tgl = DateTime.Now.ToString();
            return tgl;
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Delete"].Index)
            {
                //Do something with your button.
                int row_idx = e.RowIndex;
                MessageBox.Show("delete butt clicked" + row_idx);
            }
        }

        private void RefreshDataPelanggan()
        {

            this.DataPelanggan = DbSearchLoadData.Pelanggan();
            this.comboBoxNamaPelangganAtas.DataSource = this.DataPelanggan;
            this.comboBoxNamaPelangganAtas.DisplayMember = "NAMA";
            this.comboBoxNamaPelangganAtas.ValueMember = "ID";
            this.comboBoxNamaPelangganAtas.SelectedIndex= 0;
            this.comboBoxNamaPelangganAtas.AutoCompleteSource = AutoCompleteSource.CustomSource;
            this.comboBoxNamaPelangganAtas.AutoCompleteMode= AutoCompleteMode.SuggestAppend;
        }

        private void RefreshDataBarang()
        {
            this.DataBarang = DbSearchLoadData.Barang();
            this.textBoxNamaItem.DataSource = this.DataBarang;
            this.textBoxNamaItem.DisplayMember = "NAMA_BARANG";
            this.textBoxNamaItem.ValueMember = "ID_BARANG";
            this.textBoxNamaItem.Text = "";
        }

        private void RefreshDataKas()
        {
            this.DataKas = DbSearchLoadData.Kas();
            this.comboBoxJenisKas.DataSource = this.DataKas;
            this.comboBoxJenisKas.DisplayMember = "NAMA";
            this.comboBoxJenisKas.ValueMember = "ID";
            this.comboBoxJenisKas.Text = "";
        }

        private void RefreshKodeBarang()
        {
            this.DataBarang = DbSearchLoadData.Barang();
            this.textBoxKodeItem.DataSource = this.DataBarang;
            this.textBoxKodeItem.DisplayMember = "KODE";
            this.textBoxKodeItem.ValueMember = "ID_BARANG";
            this.textBoxKodeItem.Text = "";
        }

        //private 

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


        /*private void comboBoxNamaPelangganAtas_TextChanged(object sender, EventArgs e)
        {
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
        }*/
        #region AutoCompletePelanggan
        private bool _canUpdate = false;
        private bool _needUpdate = false;
        private void comboBoxNamaPelangganAtas_TextChanged(object sender, EventArgs e)
        {
            _needUpdate = true;
            if (_needUpdate)
            {
                _canUpdate= true;
                if (_canUpdate)
                {
                    _canUpdate = false;
                    UpdateData();
                }
                else
                {
                    RestartTimer();
                }
            }
        }
        private void UpdateData()
        {
            if (comboBoxNamaPelangganAtas.Text.Length > 1)
            {
                //List<string> searchData = this.DataPenjualan.GetData(comboBoxNamaPelangganAtas.Text);
                DataView dataView = new DataView(this.DataPelanggan);
                dataView.RowFilter = "NAMA LIKE '%" + comboBoxNamaPelangganAtas.Text + "%'";
                var searchData = dataView.ToTable();
                HandleTextChanged(searchData);
            }
        }

        //While timer is running don't start search
        //timer1.Interval = 1500;
        private void RestartTimer()
        {
            /*timer1.Stop();
            _canUpdate = false;
            timer1.Start();*/
        }

        //Update data when timer stops
        private void timer1_Tick(object sender, EventArgs e)
        {
            /*_canUpdate = true;
            timer1.Stop();
            UpdateData();*/
        }

        //Update combobox with new data
        private void HandleTextChanged(DataTable dataSource)
        {
            var text = comboBoxNamaPelangganAtas.Text;

            if (dataSource.Rows.Count > 0)
            {
                this.comboBoxNamaPelangganAtas.DataSource = dataSource;
/*
                //var sText = comboBoxNamaPelangganAtas.Items[0].ToString();
                //comboBoxNamaPelangganAtas.SelectionStart = text.Length;
                //comboBoxNamaPelangganAtas.SelectionLength = sText.Length - text.Length;*/
                comboBoxNamaPelangganAtas.DroppedDown = true;
                return;
            }
            else
            {
                comboBoxNamaPelangganAtas.DroppedDown = false;
                comboBoxNamaPelangganAtas.SelectionStart = text.Length;
            }
        }
        #endregion

        private void textBoxKodeItem_TextChanged_1(object sender, EventArgs e)
        {
            
            /*DataView dataView = new DataView(this.DataBarang);
            dataView.RowFilter = "KODE LIKE '%" + textBoxKodeItem.Text + "%'";
            var searchData = dataView.ToTable();
            if (searchData.Rows.Count > 0)
            {
                this.textBoxKodeItem.DataSource = searchData;
                this.textBoxKodeItem.DroppedDown = true;
            }
            else
            {
                this.textBoxKodeItem.DroppedDown = false;
            }*/
        }
    }
}
