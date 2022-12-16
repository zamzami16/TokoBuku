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

            this.dataTableBase = DbLoadData.Barang(this.DbConnection);
            this.dataGridView1.DataSource = this.dataTableBase;
            //this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //this.dataGridView1.Columns[1].FillWeight = 80;
            //this.dataGridView1.Columns[2].FillWeight = 20;
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
                        var namaBarang = form.NamaBarang;
                        var stock = form.Stock;
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


                        bool hasilll = SuccessSaveToDb(inIdKategori: kategori, inIdPenerbit: penerbit, inIdRak: rak,
                            inNama: namaBarang, inStock: stock, inHarga: harga, inIsbn: isbn, inPenulis: penulis, inDiskon: diskon,
                            inStatus: status, inBarCode: barCode, inKeterngan: keterangan);
                        if (hasilll)
                        {
                            var results = MessageBox.Show("DATA BERHASIL DISIMPAN.\nANDA MAU MENAMBAH DATA LAGI?", "Success.", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            this.DbRefresh();
                            if (results != DialogResult.Yes)
                            {
                                Loop = false;
                            }
                        }
                        else
                        {
                            var results = MessageBox.Show($"DATA BARANG {namaBarang} SUDAH ADA.\nANDA MAU ULANGI MENAMBAH DATA LAGI?", "Error.", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                            if (results != DialogResult.Yes)
                            {
                                Loop = false;
                            }
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
                string selectedName = row.Cells[1].Value.ToString();
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
                    MessageBox.Show($"Data {selectedName} deleted.", "Success.");
                }
                this.dataGridView1.Rows.Remove(row);
            }
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

        private void TampilkanBerhasilSimpan(string message)
        {
            MessageBox.Show(message, "Succes.", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private bool SuccessSaveToDb(int inIdKategori, int inIdPenerbit, int inIdRak, string inNama, 
            int inStock, double inHarga, string inIsbn, string inPenulis, double inDiskon, 
            string inStatus, string inBarCode, string inKeterngan)
        {
            bool hasil;
            try
            {
                using (var con = ConnectDB.Connetc())
                {
                    int ids;
                    var strSql = "INSERT INTO BARANG (ID_KATEGORI, ID_PENERBIT, ID_RAK, NAMA_BARANG, STOCK, HARGA, ISBN, PENULIS, DISKON, STATUS, BARCODE, KETERANGAN) " +
                        "VALUES (@kategori, @penerbit, @rak, @nama, @stock, @harga, @isbn, @penulis, @diskon, @status, @barcode, @keterangan) returning ID_BARANG;";
                    using (var cmd = new FbCommand(strSql, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add("@kategori", inIdKategori);
                        cmd.Parameters.Add("@penerbit", inIdPenerbit);
                        cmd.Parameters.Add("@rak", inIdRak);
                        cmd.Parameters.Add("@nama", inNama);
                        cmd.Parameters.Add("@stock", inStock);
                        cmd.Parameters.Add("@harga", inHarga);
                        cmd.Parameters.Add("@isbn", inIsbn);
                        cmd.Parameters.Add("@penulis", inPenulis);
                        cmd.Parameters.Add("@diskon", inDiskon);
                        cmd.Parameters.Add("@status", "AKTIF");
                        cmd.Parameters.Add("@barcode", inBarCode);
                        cmd.Parameters.Add("@keterangan", inKeterngan);
                        ids = (int)cmd.ExecuteScalar();
                        cmd.Dispose();
                    }

                    /*DataRow dataRow = this.dataTableBase.NewRow();
                    dataRow["ID"] = ids;
                    dataRow["NAMA"] = inNama;
                    dataRow["STATUS"] = "AKTIF";
                    this.dataTableBase.Rows.Add(dataRow);*/
                }
                hasil = true;
                return hasil;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("eksekusi awal catch 1" + ex.Message);
                if (ex.Message.Contains("PRIMARY or UNIQUE"))
                {
                    hasil = false;
                    MessageBox.Show($"eksekusi awal {hasil}");
                    return hasil;
                }
                else
                {
                    hasil = false;
                    MessageBox.Show(ex.Message);
                    return hasil;
                }
            }
        }

        private void buttonEditData_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                string selectedName = row.Cells[1].Value.ToString();
                int selectedId = Convert.ToInt32(row.Cells[0].Value.ToString());
                using (var form = FormEdit.Barang(row))
                {
                    var result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        var kategori = Convert.ToInt32(form.Kategori);
                        var penerbit = Convert.ToInt32(form.Penerbit);
                        var rak = Convert.ToInt32(form.Rak);
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
                            MessageBox.Show($"{selectedName} Updated.", "Success.");
                        }
                        this.dataGridView1.Rows[row.Index].Cells[1].Value = namaBarang;
                        this.dataGridView1.Rows[row.Index].Cells[2].Value = form.KategoriText;
                        this.dataGridView1.Rows[row.Index].Cells[3].Value = form.PenerbitText;
                        this.dataGridView1.Rows[row.Index].Cells[4].Value = form.RakText;
                        this.dataGridView1.Rows[row.Index].Cells[5].Value = stock;
                        this.dataGridView1.Rows[row.Index].Cells[6].Value = harga;
                        this.dataGridView1.Rows[row.Index].Cells[7].Value = isbn;
                        this.dataGridView1.Rows[row.Index].Cells[8].Value = penulis;
                        this.dataGridView1.Rows[row.Index].Cells[9].Value = diskon;
                        this.dataGridView1.Rows[row.Index].Cells[11].Value = barCode;
                        this.dataGridView1.Rows[row.Index].Cells[12].Value = keterangan;
                    }
                }
            }
        }

        private void DbRefresh()
        {
            this.dataTableBase = DbLoadData.Barang(ConnectDB.Connetc());
            this.dataGridView1.DataSource = this.dataTableBase;
        }
    }
}

