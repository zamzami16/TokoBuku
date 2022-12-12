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
    public partial class FormMasterDataViewer : Form
    {
        
        DataTable dataTableBase = new DataTable();

        public enum EnumJenisForm
        {
            Barang,
            Kasir,
            KasMaster,
            Kategori,
            Pelanggan,
            Pembelian,
            Penerbit,
            Penjual,
            Rak,
            Supplier
        }

        public EnumJenisForm jenisForm = EnumJenisForm.Barang;
        public Form formData;

        public FormMasterDataViewer()
        {
            InitializeComponent();
        }


        private void FormMasterDataViewer_Load(object sender, EventArgs e)
        {
            //this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        }

        private void FormMasterDataViewer_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }

        public void SetJenisForm(string jenisForm)
        {

            switch (jenisForm)
            {
                case "barang":
                    this.jenisForm = EnumJenisForm.Barang;
                    this.Text = "DATA BARANG";
                    this.DataViewTitle.Text = "DATA BARANG";
                    initTableBarang();
                    break;

                case "kasir":
                    this.jenisForm = EnumJenisForm.Kasir;
                    this.Text = "DATA KASIR";
                    this.DataViewTitle.Text = "DATA KASIR";
                    initTableKasir();
                    break;

                case "kas":
                    this.jenisForm = EnumJenisForm.KasMaster;
                    this.Text = "DATA KAS";
                    this.DataViewTitle.Text = "DATA KAS";
                    initTableRakKasKategoriPenerbitMaster();
                    break;

                case "kategori":
                    this.jenisForm = EnumJenisForm.Kategori;
                    this.Text = "DATA KATEGORI BARANG";
                    this.DataViewTitle.Text = "DATA KATEGORI BARANG";
                    initTableRakKasKategoriPenerbitMaster();
                    break;

                case "pelanggan":
                    this.jenisForm = EnumJenisForm.Pelanggan;
                    this.Text = "DATA PELANGGAN";
                    this.DataViewTitle.Text = "DATA PELANGGAN";
                    initTablePelangganSupplier();
                    break;

                case "pembelian":
                    this.jenisForm = EnumJenisForm.Pembelian;
                    this.Text = "DATA PEMBELIAN BARANG";
                    this.DataViewTitle.Text = "DATA PEMBELIAN BARANG";
                    break;

                case "penerbit":
                    this.jenisForm = EnumJenisForm.Penerbit;
                    this.Text = "DATA PENERBIT";
                    this.DataViewTitle.Text = "DATA PENERBIT";
                    initTableRakKasKategoriPenerbitMaster();
                    break;

                case "rak":
                    this.jenisForm = EnumJenisForm.Rak;
                    this.Text = "DATA RAK";
                    this.DataViewTitle.Text = "DATA RAK";
                    initTableRakKasKategoriPenerbitMaster();
                    break;

                case "supplier":
                    this.jenisForm = EnumJenisForm.Supplier;
                    this.Text = "DATA SUPPLIER BARANG";
                    this.DataViewTitle.Text = "DATA SUPPLIER BARANG";
                    initTablePelangganSupplier();
                    break;
            }

        }

        private void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
            
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

        private void initTableBarang()
        {
            this.dataTableBase.Clear();
            this.dataTableBase.Columns.Add("NAMA");
            this.dataTableBase.Columns.Add("RAK");
            this.dataTableBase.Columns.Add("KATEGORI");
            this.dataTableBase.Columns.Add("PENERBIT");
            this.dataTableBase.Columns.Add("PENULIS");
            this.dataTableBase.Columns.Add("ISBN");
            this.dataTableBase.Columns.Add("STOCK");
            this.dataTableBase.Columns.Add("HARGA");
            this.dataTableBase.Columns.Add("DISKON");

            this.dataGridView1.DataSource = this.dataTableBase;
            this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[0].FillWeight = 100;
        }

        private void initTableKasir()
        {
            this.dataTableBase.Clear();
            this.dataTableBase.Columns.Add("NAMA");
            this.dataTableBase.Columns.Add("USERNAME");
            this.dataTableBase.Columns.Add("PASSWORD");
            this.dataTableBase.Columns.Add("ALAMAT");
            this.dataTableBase.Columns.Add("NO. HP");
            this.dataTableBase.Columns.Add("STATUS");

            this.dataGridView1.DataSource = this.dataTableBase;
            this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[0].FillWeight = 30;
            this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[1].FillWeight = 30;
            this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[2].FillWeight = 30;
            this.dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[3].FillWeight = 30;
            this.dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[4].FillWeight = 30;
        }

        private void initTableRakKasKategoriPenerbitMaster()
        {
            this.dataTableBase.Clear();
            this.dataTableBase.Columns.Add("NAMA");
            this.dataTableBase.Columns.Add("STATUS");

            this.dataGridView1.DataSource = this.dataTableBase;
            this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[0].FillWeight = 100;
        
        }

        private void initTablePelangganSupplier()
        {
            this.dataTableBase.Clear();
            this.dataTableBase.Columns.Add("NAMA");
            this.dataTableBase.Columns.Add("ALAMAT");
            this.dataTableBase.Columns.Add("NO. HP");
            this.dataTableBase.Columns.Add("EMAIL");
            this.dataTableBase.Columns.Add("STATUS");

            this.dataGridView1.DataSource = this.dataTableBase;
            this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[0].FillWeight = 30;
            this.dataGridView1.Columns[1].FillWeight = 40;
            this.dataGridView1.Columns[2].FillWeight = 15;
            this.dataGridView1.Columns[3].FillWeight = 15;
        }



        private void buttonAddData_Click(object sender, EventArgs e)
        {
            switch (this.jenisForm)
            {
                case EnumJenisForm.Barang:
                    this.formData = new FormDataBarang();
                    this.formData.ShowDialog();
                    TampilTambahData();
                    break;
                case EnumJenisForm.Kasir:
                    this.formData = new FormDataKasir();
                    this.formData.ShowDialog();
                    TampilTambahData();
                    break;
                case EnumJenisForm.KasMaster:
                    this.formData =  new FormDataRakKasKategoriPenerbitMaster("kas");
                    this.formData.ShowDialog();
                    TampilTambahData();
                    break;
                case EnumJenisForm.Kategori:
                    this.formData = new FormDataRakKasKategoriPenerbitMaster("kategori");
                    this.formData.ShowDialog();
                    TampilTambahData();
                    break;
                case EnumJenisForm.Pelanggan:
                    this.formData = new FormDataPelangganSupplier("pelanggan");
                    this.formData.ShowDialog();
                    TampilTambahData();
                    break;
                case EnumJenisForm.Pembelian:
                    break;
                case EnumJenisForm.Penerbit:
                    this.formData = new FormDataRakKasKategoriPenerbitMaster("penerbit");
                    this.formData.ShowDialog();
                    TampilTambahData();
                    break;
                case EnumJenisForm.Penjual:
                    break;
                case EnumJenisForm.Rak:
                    this.formData = new FormDataRakKasKategoriPenerbitMaster("rak");
                    this.formData.ShowDialog();
                    TampilTambahData();
                    break;
                case EnumJenisForm.Supplier:
                    this.formData = new FormDataPelangganSupplier("supplier");
                    this.formData.ShowDialog();
                    TampilTambahData();
                    break;
                default:
                    break;
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {

        }

        private void TampilTambahData()
        {
            DialogResult results = MessageBox.Show("DATA BERHASIL DISIMPAN.\nANDA MAU MENAMBAH DATA LAGI?", "Success.", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            while (results == DialogResult.Yes)
            {
                this.formData.ShowDialog();
                results = MessageBox.Show("DATA BERHASIL DISIMPAN.\nANDA MAU MENAMBAH DATA LAGI?", "Success.", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            }
        }
    }
}
