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

namespace TokoBuku.BaseForm
{
    public partial class FormMasterDataViewer : Form
    {
        /*
        private FbConnection DbConnection = new ConnectDB().Connetc();
        public DataTable dataTableBase { get; set; }

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
            //this.dataTableBase = new DataTable();
            this.ActiveControl = this.buttonAddData;
        }

        private void FormMasterDataViewer_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }

        public void SetJenisForm(string jenisForm)
        {
            this.dataTableBase = new DataTable();
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
                    //initTableRakKasKategoriPenerbitMaster();
                    this.dataTableBase = DbLoadData.Kategori(this.DbConnection);
                    this.dataGridView1.DataSource = this.dataTableBase;
                    this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    this.dataGridView1.Columns[1].FillWeight = 80;
                    this.dataGridView1.Columns[2].FillWeight = 20;
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
            this.dataTableBase.Columns.Add("KETERANGAN");

            this.dataGridView1.DataSource = this.dataTableBase;
            this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[0].FillWeight = 100;
            this.dataGridView1.Columns[9].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[9].FillWeight = 50;
        }

        private void initTableKasir()
        {
            this.dataTableBase.Clear();
            this.dataTableBase.Columns.Add("NAMA");
            this.dataTableBase.Columns.Add("USERNAME");
            this.dataTableBase.Columns.Add("PASSWORD");
            this.dataTableBase.Columns.Add("ALAMAT");
            this.dataTableBase.Columns.Add("NO. HP");
            this.dataTableBase.Columns.Add("KETERANGAN");
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
            this.dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[5].FillWeight = 50;
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
        ///Akhir block init table
        ///

        private void buttonAddData_Click(object sender, EventArgs e)
        {

            switch (this.jenisForm)
            {
                case EnumJenisForm.Barang:
                    bool Loop = true;
                    while (Loop)
                    {
                        using (var form = new FormDataBarang())
                        {
                            var result = form.ShowDialog();
                            if (result == DialogResult.OK)
                            {
                                var namaBarang = form.NamaBarang;
                                var harga = form.Harga;
                                var diskon = form.Diskon;
                                var rak = form.Rak;
                                var kategori = form.Kategori;
                                var penerbit = form.Penerbit;
                                var penulis = form.Penulis;
                                var isbn = form.ISBN;
                                var barCode = form.BarCode;
                                var status = form.Status;

                                string messages = "DATA SAVED FROM MASTER DATA VIEWER FORM.\n" +
                                    $"Nama Barang: {namaBarang}" +
                                    $"Harga: {harga} Rupiah\n" +
                                    $"Diskon {diskon} %\n";
                                TampilkanBerhasilSimpan(messages);


                                var results = MessageBox.Show("DATA BERHASIL DISIMPAN.\nANDA MAU MENAMBAH DATA LAGI?", "Success.", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                if (results != DialogResult.Yes)
                                {
                                    Loop = false;
                                }
                            }
                            else
                            {
                                Loop = false;
                                break;
                            }
                        }
                    }

                    //TampilTambahData();
                    break;
                case EnumJenisForm.Kasir:
                    this.formData = new FormDataKasir();
                    this.formData.ShowDialog();
                    //TampilTambahData();
                    break;
                case EnumJenisForm.KasMaster:
                    Loop = true;
                    while (Loop)
                    {
                        using (var form = f)
                        {
                            var result = form.ShowDialog();
                            if (result == DialogResult.OK)
                            {

                                var namaInput = form.ValueName;

                                string messages = "DATA SAVED FROM MASTER DATA VIEWER FORM.\n" +
                                    $"Nama Kas: {namaInput}";
                                TampilkanBerhasilSimpan(messages);

                                var results = MessageBox.Show("DATA BERHASIL DISIMPAN.\nANDA MAU MENAMBAH DATA LAGI?", "Success.", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                if (results != DialogResult.Yes)
                                {
                                    Loop = false;
                                }
                            }
                            else
                            {
                                Loop = false;
                                break;
                            }
                        }
                    }
                    break;

                case EnumJenisForm.Kategori:
                    Loop = true;
                    while (Loop)
                    {
                        using (var form = new FormDataRakKasKategoriPenerbitMaster("kategori"))
                        {
                            var result = form.ShowDialog();
                            if (result == DialogResult.OK)
                            {
                                var namaInput = form.ValueName;
                                //MessageBox.Show("Eksekusi atas....");
                                bool hasilll = SuccessSaveToDbRakKategoriKasPenerbit(namaInput);
                                if (hasilll)
                                {
                                    var results = MessageBox.Show("DATA BERHASIL DISIMPAN.\nANDA MAU MENAMBAH DATA LAGI?", "Success.", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                    if (results != DialogResult.Yes)
                                    {
                                        Loop = false;
                                    }
                                }
                                else
                                {
                                    var results = MessageBox.Show($"DATA KATEGORI {namaInput} SUDAH ADA.\nANDA MAU ULANGI MENAMBAH DATA LAGI?", "Error.", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
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
                    //TampilTambahData();
                    break;
                case EnumJenisForm.Pelanggan:
                    this.formData = new FormDataPelangganSupplier("pelanggan");
                    this.formData.ShowDialog();
                    //TampilTambahData();
                    break;
                case EnumJenisForm.Pembelian:
                    break;
                case EnumJenisForm.Penerbit:
                    Loop = true;
                    while (Loop)
                    {
                        using (var form = new FormDataRakKasKategoriPenerbitMaster("penerbit"))
                        {
                            var result = form.ShowDialog();
                            if (result == DialogResult.OK)
                            {

                                var namaInput = form.ValueName;

                                string messages = "DATA SAVED FROM MASTER DATA VIEWER FORM.\n" +
                                    $"Nama Penerbit: {namaInput}";
                                TampilkanBerhasilSimpan(messages);

                                var results = MessageBox.Show("DATA BERHASIL DISIMPAN.\nANDA MAU MENAMBAH DATA LAGI?", "Success.", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                if (results != DialogResult.Yes)
                                {
                                    Loop = false;
                                }
                            }
                            else
                            {
                                Loop = false;
                                break;
                            }
                        }
                    }
                    //TampilTambahData();
                    break;
                case EnumJenisForm.Penjual:
                    break;
                case EnumJenisForm.Rak:
                    Loop = true;
                    while (Loop)
                    {
                        using (var form = new FormDataRakKasKategoriPenerbitMaster("rak"))
                        {
                            var result = form.ShowDialog();
                            if (result == DialogResult.OK)
                            {

                                var namaInput = form.ValueName;

                                string messages = "DATA SAVED FROM MASTER DATA VIEWER FORM.\n" +
                                    $"Nama Rak: {namaInput}";
                                TampilkanBerhasilSimpan(messages);

                                var results = MessageBox.Show("DATA BERHASIL DISIMPAN.\nANDA MAU MENAMBAH DATA LAGI?", "Success.", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                if (results != DialogResult.Yes)
                                {
                                    Loop = false;
                                }
                            }
                            else
                            {
                                Loop = false;
                                break;
                            }
                        }
                    }
                    //TampilTambahData();
                    break;
                case EnumJenisForm.Supplier:
                    this.formData = new FormDataPelangganSupplier("supplier");
                    this.formData.ShowDialog();
                    //TampilTambahData();
                    break;
                default:
                    break;
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                string selectedName = row.Cells[0].Value.ToString();
                using (var con = new ConnectDB().Connetc())
                {
                    //MessageBox.Show("eksekusi awal try 1");
                    var strSql = "DELETE FROM KATEGORI WHERE NAMA=@nama";
                    using (var cmd = new FbCommand(strSql, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add("@nama", selectedName);
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }
                    MessageBox.Show($"{selectedName} deleted.");
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

        private bool SuccessSaveToDbRakKategoriKasPenerbit(string namaInput)
        {
            ///Connect DB
            ///
            bool hasil;
            try
            {
                using (var con = new ConnectDB().Connetc())
                {
                    /// first impression to insert value
                    *//*var strSql = "INSERT INTO KATEGORI (NAMA, STATUS) VALUES (@nama, @status)";
                    using (var cmd = new FbCommand(strSql, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add("@nama", namaInput);
                        cmd.Parameters.Add("@status", "AKTIF");
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }*//*

                    int ids;
                    var strSql = "INSERT INTO KATEGORI (NAMA, STATUS) VALUES (@nama, @status) returning Id;";
                    using (var cmd = new FbCommand(strSql, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add("@nama", namaInput);
                        cmd.Parameters.Add("@status", "AKTIF");
                        ids = (int)cmd.ExecuteScalar();
                        cmd.Dispose();
                    }


                    //MessageBox.Show("Eksekusi setelah simpan data ke db. Lanjut tambah data ke data tabel");
                    *//*string messages = "DATA SAVED FROM MASTER DATA VIEWER FORM.\n" +
                            $"Nama Kategori: {namaInput}";
                    TampilkanBerhasilSimpan(messages);*//*

                    DataRow dataRow = this.dataTableBase.NewRow();
                    dataRow["ID"] = ids;
                    dataRow["NAMA"] = namaInput;
                    dataRow["STATUS"] = "AKTIF";
                    this.dataTableBase.Rows.Add(dataRow);
                    //MessageBox.Show("hasil eksekusi tambah data tabel. lanjut tambah data row");
                    //this.dataGridView1.Rows.Add(dataRow);
                    
                    //MessageBox.Show("hasil eksekusi setelah tambah data grid view.");
                    
                }
                //return hasil;
                hasil = true;
                //MessageBox.Show($"hasil eksekusi setelah try 1 {hasil}");
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
            switch (this.jenisForm)
            {
                case EnumJenisForm.Barang:
                    break;
                case EnumJenisForm.Kasir:
                    break;
                case EnumJenisForm.KasMaster:
                    break;
                case EnumJenisForm.Kategori:
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        string selectedName = row.Cells[0].Value.ToString();
                        using (var form = new FormEditDatasKategori(selectedName))
                        {
                            var result = form.ShowDialog();
                            if (result == DialogResult.OK)
                            {
                                var changedName = form.ChangedName;
                                using (var con = new ConnectDB().Connetc())
                                {
                                    /// Lanjutkan dulu
                                }
                            }
                        }
                        
                        this.dataGridView1.Rows.Remove(row);
                    }
                    break;
                case EnumJenisForm.Pelanggan:
                    break;
                case EnumJenisForm.Pembelian:
                    break;
                case EnumJenisForm.Penerbit:
                    break;
                case EnumJenisForm.Penjual:
                    break;
                case EnumJenisForm.Rak:
                    break;
                case EnumJenisForm.Supplier:
                    break;
                default:
                    break;
            }
        }
    }*/
    }
}

