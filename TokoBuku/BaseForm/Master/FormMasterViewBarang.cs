using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FirebirdSql.Data.FirebirdClient;
using TokoBuku.DbUtility;
using TokoBuku.BaseForm.EditForm;
using TokoBuku.BaseForm.Master.Input;

namespace TokoBuku.BaseForm.Master
{
    public partial class FormMasterViewBarang : Form
    {
        private FbConnection DbConnection = ConnectDB.Connetc();
        public DataTable dataTableBase { get; set; }

        public Form formData;

        public FormMasterViewBarang()
        {
            InitializeComponent();
        }


        private void FormMasterDataViewer_Load(object sender, EventArgs e)
        {
            //this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.dataTableBase = new DataTable();
            this.ActiveControl = this.buttonAddData;

            this.dataTableBase = DbLoadData.Barang();
            this.dataGridView1.DataSource = this.dataTableBase;
            this.dataGridView1.Columns[0].Visible = false;      // id
            this.dataGridView1.Columns[11].Visible = false;     // Status
            this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells; // kode
            this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;           // nama
            this.dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells; // 
            this.dataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridView1.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[9].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[10].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[11].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[12].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridView1.Columns[13].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[1].FillWeight = 5;
            this.dataGridView1.Columns[2].FillWeight = 20;
            this.dataGridView1.Columns[3].FillWeight = 10;
            this.dataGridView1.Columns[4].FillWeight = 10;
            this.dataGridView1.Columns[5].FillWeight = 10;
            this.dataGridView1.Columns[6].FillWeight = 5;
            this.dataGridView1.Columns[7].FillWeight = 10;
            this.dataGridView1.Columns[8].FillWeight = 10;
            this.dataGridView1.Columns[9].FillWeight = 10;
            this.dataGridView1.Columns[10].FillWeight = 5;
            this.dataGridView1.Columns[11].FillWeight = 5;
            this.dataGridView1.Columns[12].FillWeight = 10;
            this.dataGridView1.Columns[13].FillWeight = 20;

            /// Format Grid View
            //this.dataGridView1.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Sunken;
            //this.dataGridView1.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            // Harga
            this.dataGridView1.Columns[7].DefaultCellStyle.Format = "C";
            this.dataGridView1.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            // Diskon
            this.dataGridView1.Columns[10].DefaultCellStyle.Format = "0.00'%'";
            this.dataGridView1.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            this.labelPersediaan.Text = "Total Persediaan: " +
                TokoBuku.DbUtility.MasterDataBarang.GetPersediaan().ToString("C");
        }

        private void FormMasterDataViewer_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }

       
        ///Iki bagian init tabel besti
        ///
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

        private void buttonAddData_Click(object sender, EventArgs e)
        {
            bool Loop = true;
            while (Loop)
            {
                using (var form = new FormInputDataBarang())
                {
                    var result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        var kategori = Convert.ToInt32(form.Kategori);
                        var penerbit = Convert.ToInt32(form.Penerbit);
                        var rak = Convert.ToInt32(form.Rak);
                        var kode = form.KodeBarang;
                        var namaBarang = form.NamaBarang;
                        var stock = form.Stock;
                        double hargaBeli = form.HargaBeli;
                        double harga = form.Harga;
                        var isbn = form.ISBN;
                        var penulis = form.Penulis;
                        double diskon = form.Diskon;
                        var status = form.Status;
                        var barCode = form.BarCode;
                        var keterangan = form.Keterangan;

                        /*string messages = "DATA SAVED FROM MASTER DATA VIEWER FORM.\n" +
                            $"Nama Barang: {namaBarang}\n" +
                            $"Harga: {harga} Rupiah\n" +
                            $"Diskon {diskon} %\n"+
                            $"Id Rak: {rak}.";
                        TampilkanBerhasilSimpan(messages);*/

                        try
                        {
                            int ids = DbSaveData.Barang(inIdKategori: kategori, inIdPenerbit: penerbit, inHargaBeli: hargaBeli,
                                inIdRak: rak, inKode: kode, inNama: namaBarang, inStock: stock,
                                inHarga: harga, inIsbn: isbn, inPenulis: penulis, inDiskon: diskon,
                                inStatus: status, inBarCode: barCode, inKeterngan: keterangan);

                            var results = MessageBox.Show("DATA BERHASIL DISIMPAN.\nANDA MAU MENAMBAH DATA LAGI?", "Success.", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            this.DbRefresh();
                            if (results != DialogResult.Yes)
                            {
                                Loop = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //throw;
                        }
                    }
                    else
                    {
                        Loop = false;
                        break;
                    }
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {

            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                string selectedId = row.Cells[0].Value.ToString();
                string selectedName = row.Cells[2].Value.ToString();
                var dialogs = MessageBox.Show($"Anda yakin mau menghapus data {selectedName}?", "Hapus Data?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogs == DialogResult.Yes)
                {
                    using (var con = ConnectDB.Connetc())
                    {
                        //MessageBox.Show("eksekusi awal try 1");
                        var strSql = "DELETE FROM barang WHERE id_barang=@Id";
                        using (var cmd = new FbCommand(strSql, con))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.Add("@id", selectedId);
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                        }
                    }
                    this.DbRefresh();
                }            
            }
        }
                
        private void buttonEditData_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                string selectedName = row.Cells[2].Value.ToString();
                int selectedId = Convert.ToInt32(row.Cells[0].Value.ToString());
                using (var form = FormEdit.Barang(row))
                {
                    var result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        var kategori = Convert.ToInt32(form.Kategori);
                        var penerbit = Convert.ToInt32(form.Penerbit);
                        var rak = Convert.ToInt32(form.Rak);
                        var kode = form.KodeBarang;
                        var namaBarang = form.NamaBarang;
                        var stock = form.Stock;
                        double harga = form.Harga;
                        var isbn = form.ISBN;
                        var penulis = form.Penulis;
                        double diskon = form.Diskon;
                        var status = form.Status;
                        var barCode = form.BarCode;
                        var keterangan = form.Keterangan;

                        
                        using (var con = ConnectDB.Connetc())
                        {
                            var strSql = "UPDATE barang " +
                                "SET NAMA_BARANG=@nama, " +
                                "Kode=@kode, " +
                                "ID_KATEGORI=@kategori, " +
                                "id_penerbit=@penerbit, " +
                                "id_rak=@rak, " +
                                "stock=@Stock, " +
                                "harga=@Harga, " +
                                "isbn=@Isbn, " +
                                "penulis=@Penulis, " +
                                "diskon=@Diskon, " +
                                "barcode=@Barcode, " +
                                "keterangan=@Keterangan " +
                                "where id_barang=@Id";
                            using (var cmd = new FbCommand(strSql, con))
                            {
                                cmd.CommandType = CommandType.Text;
                                cmd.Parameters.Add("@nama", namaBarang);
                                cmd.Parameters.Add("@kode", kode);
                                cmd.Parameters.Add("@kategori", kategori);
                                cmd.Parameters.Add("@penerbit", penerbit);
                                cmd.Parameters.Add("@rak", rak);
                                cmd.Parameters.Add("@Stock", stock);
                                cmd.Parameters.Add("@Harga", harga);
                                cmd.Parameters.Add("@Isbn", isbn);
                                cmd.Parameters.Add("@Penulis", penulis);
                                cmd.Parameters.Add("@Diskon", diskon);
                                cmd.Parameters.Add("@Barcode", barCode);
                                cmd.Parameters.Add("@Keterangan", keterangan);
                                cmd.Parameters.Add("@Id", selectedId);
                                cmd.ExecuteNonQuery();
                                cmd.Dispose();
                            }
                            MessageBox.Show("Data berhasil diupdated.", "Success.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        this.DbRefresh();
                        /*this.dataGridView1.Rows[row.Index].Cells[1].Value = kode;
                        this.dataGridView1.Rows[row.Index].Cells[2].Value = namaBarang;
                        this.dataGridView1.Rows[row.Index].Cells[3].Value = form.KategoriText;
                        this.dataGridView1.Rows[row.Index].Cells[4].Value = form.PenerbitText;
                        this.dataGridView1.Rows[row.Index].Cells[5].Value = form.RakText;
                        this.dataGridView1.Rows[row.Index].Cells[6].Value = stock;
                        this.dataGridView1.Rows[row.Index].Cells[7].Value = harga;
                        this.dataGridView1.Rows[row.Index].Cells[8].Value = isbn;
                        this.dataGridView1.Rows[row.Index].Cells[9].Value = penulis;
                        this.dataGridView1.Rows[row.Index].Cells[10].Value = diskon;
                        this.dataGridView1.Rows[row.Index].Cells[12].Value = barCode;
                        this.dataGridView1.Rows[row.Index].Cells[13].Value = keterangan;*/
                    }
                }
            }
        }

        private void DbRefresh()
        {
            this.dataTableBase = DbLoadData.Barang();
            this.dataGridView1.DataSource = this.dataTableBase;
        }
    }
}

