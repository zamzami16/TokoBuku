using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace TokoBuku.BaseForm.Transaksi
{
    public partial class FormUbahHarga : Form
    {
        private DataTable DataBarang;
        private DataTable DataKategori;
        private DataTable DataRak;
        private DataTable DataPenerbit;
        private BindingSource bindingSource;
        private string DumpValueDGV;
        public FormUbahHarga()
        {
            InitializeComponent();
        }

        private void UbahHarga_Load(object sender, EventArgs e)
        {
            this.GetDataBarang();
            this.bindingSource = new BindingSource();
            this.bindingSource.DataSource = this.DataBarang;
            this.DGV.DataSource = this.bindingSource;
            this.DGV.Columns[0].ReadOnly = true;
            this.DGV.Columns[1].ReadOnly = true;
            this.DGV.Columns[2].ReadOnly = true;
            this.DGV.Columns[3].ReadOnly = true;
            this.DGV.Columns[4].ReadOnly = false;
            this.DGV.Columns[5].ReadOnly = false;
            this.DGV.Columns[6].ReadOnly = false;
            this.DGV.Columns[0].Visible = false;
            this.DGV.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            this.DGV.Columns[1].HeaderText = "Kode";
            this.DGV.Columns[2].HeaderText = "Nama Barang";
            this.DGV.Columns[3].HeaderText = "Harga Beli";
            this.DGV.Columns[4].HeaderText = "Harga Jual";
            this.DGV.Columns[5].HeaderText = "Diskon";
            this.DGV.Columns[6].HeaderText = "Diskon (%)";

            this.DGV.Columns[3].DefaultCellStyle.Format = "C";
            this.DGV.Columns[4].DefaultCellStyle.Format = "C";
            this.DGV.Columns[5].DefaultCellStyle.Format = "C";
            this.DGV.Columns[6].DefaultCellStyle.Format = "0.00";

            this.DGV.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.DGV.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.DGV.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.DGV.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            this.RefreshKategori();
            this.RefreshRak();
            this.RefreshPenerbit();
        }

        private void GetDataBarang()
        {
            this.DataBarang = new DataTable();
            this.DataBarang.Columns.Add("id", typeof(int));
            this.DataBarang.Columns.Add("kode", typeof(string));
            this.DataBarang.Columns.Add("nama_barang", typeof(string));
            this.DataBarang.Columns.Add("harga_beli", typeof(double));
            this.DataBarang.Columns.Add("harga_jual", typeof(double));
            this.DataBarang.Columns.Add("diskon_rp", typeof(double));
            this.DataBarang.Columns.Add("diskon_", typeof(double));
            this.DataBarang.Merge(TokoBuku.DbUtility.Transactions.UbahHarga.ListHargaBarang());
        }

        private void DGV_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(e.ColumnIndex.ToString());
        }

        private void DGV_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            double val;
            if (!double.TryParse(this.DGV[e.ColumnIndex, e.RowIndex].Value.ToString(), out val))
            {
                this.DGV[e.ColumnIndex, e.RowIndex].Value = this.DumpValueDGV;
                this.DumpValueDGV = string.Empty;
            }
            else
            {
                this.DGV[e.ColumnIndex, e.RowIndex].Value = val;
                this.DumpValueDGV = string.Empty;
                if (e.ColumnIndex == 4)
                {
                    this.OnHargaChange(harga: val, row: e.RowIndex);
                }
                else if (e.ColumnIndex == 5)
                {
                    this.OnDiskonRpChanged(diskon: val, row: e.RowIndex);
                }
                else if (e.ColumnIndex == 6)
                {
                    this.OnDIskonPercentChanged(diskon_: (decimal)val, row: e.RowIndex);
                }
            }
        }

        private void DGV_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            this.DumpValueDGV = this.DGV[e.ColumnIndex, e.RowIndex].Value.ToString();
        }

        private void OnHargaChange(double harga, int row)
        {
            double diskon;
            var x_ = double.TryParse(this.DGV[5, row].Value.ToString(), out diskon);
            double diskon_ = diskon / harga * 100;
            this.DGV[6, row].Value = diskon_;
            int ids = Convert.ToInt32(this.DGV[0, row].Value.ToString());
            TokoBuku.DbUtility.Transactions.UbahHarga.UpdateHargaJual(id: ids, hargaJual: harga);
        }
        private void OnDiskonRpChanged(double diskon, int row)
        {
            double harga;
            var x_ = double.TryParse(this.DGV[4, row].Value.ToString(), out harga);
            decimal diskon_ = (decimal)diskon / (decimal)harga * 100;
            this.DGV[6, row].Value = diskon_;
            int ids = Convert.ToInt32(this.DGV[0, row].Value.ToString());
            TokoBuku.DbUtility.Transactions.UbahHarga.UpdateDiskon(id: ids, diskon: diskon_);
        }

        private void OnDIskonPercentChanged(decimal diskon_, int row)
        {
            double harga;
            var x_ = double.TryParse(this.DGV[4, row].Value.ToString(), out harga);
            decimal diskon = (decimal)harga * diskon_ / 100;
            this.DGV[5, row].Value = diskon_;
            int ids = Convert.ToInt32(this.DGV[0, row].Value.ToString());
            TokoBuku.DbUtility.Transactions.UbahHarga.UpdateDiskon(id: ids, diskon: diskon_);
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

        private void button1_Click(object sender, EventArgs e)
        {
            int id_kategori = Convert.ToInt32(this.comboKategori.SelectedValue.ToString());
            int id_Rak = Convert.ToInt32(this.comboRak.SelectedValue.ToString());
            int id_Penerbit = Convert.ToInt32(this.comboPenerbit.SelectedValue.ToString());
            this.DataBarang.Clear();
            this.DataBarang.Merge(TokoBuku.DbUtility.Transactions.UbahHarga.FilterDataBarang(id_kategori: id_kategori, id_rak: id_Rak, id_penerbit: id_Penerbit));
            this.DGV.DataSource = this.DataBarang;
        }

        private void RefreshKategori()
        {
            this.DataKategori = new DataTable();
            this.DataKategori.Columns.Add("id", typeof(int));
            this.DataKategori.Columns.Add("nama", typeof(string));

            DataRow row = this.DataKategori.NewRow();
            row["id"] = -1;
            row["nama"] = "SEMUA";
            this.DataKategori.Rows.Add(row);
            this.DataKategori.Merge(TokoBuku.DbUtility.Transactions.UbahHarga.GetDataKategori());
            this.comboKategori.DataSource = this.DataKategori;
            this.comboKategori.DisplayMember = "nama";
            this.comboKategori.ValueMember = "id";
            this.comboKategori.SelectedIndex = 0;
            this.comboKategori.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void RefreshRak()
        {
            this.DataRak = new DataTable();
            this.DataRak.Columns.Add("id", typeof(int));
            this.DataRak.Columns.Add("nama", typeof(string));

            DataRow row = this.DataRak.NewRow();
            row["id"] = -1;
            row["nama"] = "SEMUA";
            this.DataRak.Rows.Add(row);
            this.DataRak.Merge(TokoBuku.DbUtility.Transactions.UbahHarga.GetDataRak());
            this.comboRak.DataSource = this.DataRak;
            this.comboRak.DisplayMember = "nama";
            this.comboRak.ValueMember = "id";
            this.comboRak.SelectedIndex = 0;
            this.comboRak.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void RefreshPenerbit()
        {
            this.DataPenerbit = new DataTable();
            this.DataPenerbit.Columns.Add("id", typeof(int));
            this.DataPenerbit.Columns.Add("nama", typeof(string));

            DataRow row = this.DataPenerbit.NewRow();
            row["id"] = -1;
            row["nama"] = "SEMUA";
            this.DataPenerbit.Rows.Add(row);
            this.DataPenerbit.Merge(TokoBuku.DbUtility.Transactions.UbahHarga.GetDataPenerbit());
            this.comboPenerbit.DataSource = this.DataPenerbit;
            this.comboPenerbit.DisplayMember = "nama";
            this.comboPenerbit.ValueMember = "id";
            this.comboPenerbit.SelectedIndex = 0;
            this.comboPenerbit.DropDownStyle = ComboBoxStyle.DropDownList;
        }
    }
}
