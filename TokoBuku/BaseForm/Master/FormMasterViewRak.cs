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
    public partial class FormMasterViewRak : Form
    {
        private FbConnection DbConnection = new ConnectDB().Connetc();
        public DataTable dataTableBase { get; set; }

        public Form formData;

        public FormMasterViewRak()
        {
            InitializeComponent();
        }


        private void FormMasterDataViewer_Load(object sender, EventArgs e)
        {
            this.ActiveControl = this.buttonAddData;
            this.dataTableBase = new DataTable();
            //initTableRakKasKategoriPenerbitMaster();
            this.dataTableBase = DbLoadData.Kategori(this.DbConnection);
            this.dataGridView1.DataSource = this.dataTableBase;
            this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[1].FillWeight = 80;
            this.dataGridView1.Columns[2].FillWeight = 20;
        }

        private void FormMasterDataViewer_Deactivate(object sender, EventArgs e)
        {
            this.Close();
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

        private void buttonAddData_Click(object sender, EventArgs e)
        {

            bool Loop = true;
            while (Loop)
            {
                using (var form = FormInput.Rak())
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


                    //MessageBox.Show("Eksekusi setelah simpan data ke db. Lanjut tambah data ke data tabel");
                    /*string messages = "DATA SAVED FROM MASTER DATA VIEWER FORM.\n" +
                            $"Nama Kategori: {namaInput}";
                    TampilkanBerhasilSimpan(messages);*/

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

