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
    public partial class FormMasterViewPelanggan : Form
    {
        private FbConnection DbConnection = ConnectDB.Connetc();
        public DataTable dataTableBase { get; set; }

        public Form formData;

        public FormMasterViewPelanggan()
        {
            InitializeComponent();
        }


        private void FormMasterDataViewer_Load(object sender, EventArgs e)
        {
            this.ActiveControl = this.buttonAddData;
            this.dataTableBase = new DataTable();
            //initTableRakKasKategoriPenerbitMaster();
            this.dataTableBase = DbLoadData.Pelanggan(this.DbConnection);
            this.dataGridView1.DataSource = this.dataTableBase;
            this.dataGridView1.Columns[0].Width = 0;
            this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[1].FillWeight = 20;
            this.dataGridView1.Columns[2].FillWeight = 20;
            this.dataGridView1.Columns[3].FillWeight = 15;
            this.dataGridView1.Columns[4].FillWeight = 15;
            this.dataGridView1.Columns[5].FillWeight = 30;
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

        /// <summary>
        /// Add Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAddData_Click(object sender, EventArgs e)
        {
            bool Loop = true;
            while (Loop)
            {
                using (var form = FormInput.Pelanggan())
                {
                    var result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        var nama = form.inputNama;
                        var alamat = form.inputALamat;
                        var no_hp = form.inputNoHP.Replace("-", "").Replace("(","").Replace(")", "").Replace("+","").Replace(" ", "");
                        var email = form.inputEmail;
                        var keterangan = form.inputKeterangan;
                        var status = form.inputStatus;
                        int ids;
                        try
                        {
                            ids = DbSaveData.Pelanggan(nama: nama, alamat: alamat, email: email,
                            no_hp: no_hp, keterangan: keterangan, status: status);

                            DataRow dataRow = this.dataTableBase.NewRow();
                            dataRow["ID"] = ids;
                            dataRow["NAMA"] = nama;
                            dataRow["ALAMAT"] = alamat;
                            dataRow["EMAIL"] = email;
                            dataRow["NO_HP"] = no_hp;
                            dataRow["KETERANGAN"] = keterangan;
                            dataRow["STATUS"] = "AKTIF";

                            this.dataTableBase.Rows.Add(dataRow);

                            var lanjut = MessageBox.Show("Data Berhasil disimpan.\nAnda mau menambah data lagi?", "Success.",MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (lanjut != DialogResult.Yes)
                            {
                                Loop = false;
                                break;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                int ids = Convert.ToInt32(row.Cells[0].Value.ToString());
                string nama = row.Cells[1].Value.ToString();
                try
                {
                    DbDeleteData.Pelanggan(ids);
                    MessageBox.Show($"Data {nama} berhasil dihapus.");

                    DataRow rows = ((DataRowView)row.DataBoundItem).Row;
                    this.dataTableBase.Rows.Remove(rows);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //throw;
                }
            }
        }

        /*private void TampilTambahData()
        {
            DialogResult results = MessageBox.Show("DATA BERHASIL DISIMPAN.\nANDA MAU MENAMBAH DATA LAGI?", "Success.", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            while (results == DialogResult.Yes)
            {
                this.formData.ShowDialog();
                results = MessageBox.Show("DATA BERHASIL DISIMPAN.\nANDA MAU MENAMBAH DATA LAGI?", "Success.", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            }
        }*/

        /*private void TampilkanBerhasilSimpan(string message)
        {
            MessageBox.Show(message, "Succes.", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }*/

        private void buttonEditData_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                int Ids = Convert.ToInt32(row.Cells[0].Value.ToString());
                var namaAwal = row.Cells[1].Value.ToString();
                using (var form = FormEdit.Pelanggan(row))
                {
                    var result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        var nama = form.inputNama;
                        var alamat = form.inputALamat;
                        var no_hp = form.inputNoHP.Replace("-", "").Replace("(", "").Replace(")", "").Replace("+", "").Replace(" ", "");
                        var email = form.inputEmail;
                        var keterangan = form.inputKeterangan;
                        var status = form.inputStatus;
                        try
                        {
                            DbEditData.Pelanggan(Ids: Ids, nama: nama, alamat: alamat, no_hp: no_hp, email: email, keterangan: keterangan);
                            MessageBox.Show($"Data berhasil di update.", "Success.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.dataTableBase = DbLoadData.Pelanggan(this.DbConnection);
                            this.dataGridView1.DataSource = this.dataTableBase;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message + ex, "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            throw;
                        }
                    }
                }
            }
        }
    }
}

