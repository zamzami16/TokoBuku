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
        private FbConnection DbConnection = new ConnectDB().Connetc();
        public DataTable dataTableBase { get; set; }

        public Form formData;

        public FormMasterViewBarang()
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
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {

            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                string selectedName = row.Cells[1].Value.ToString();
                MessageBox.Show(selectedName);
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
            bool hasil;
            try
            {
                using (var con = new ConnectDB().Connetc())
                {

                    /*var strSql = "INSERT INTO KATEGORI (NAMA, STATUS) VALUES (@nama, @status)";
                    using (var cmd = new FbCommand(strSql, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add("@nama", namaInput);
                        cmd.Parameters.Add("@status", "AKTIF");
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }*/

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

                    DataRow dataRow = this.dataTableBase.NewRow();
                    dataRow["ID"] = ids;
                    dataRow["NAMA"] = namaInput;
                    dataRow["STATUS"] = "AKTIF";
                    this.dataTableBase.Rows.Add(dataRow);
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
                string selectedName = row.Cells[0].Value.ToString();
                using (var form = new FormEditKategori(selectedName))
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
        }
    }
}

