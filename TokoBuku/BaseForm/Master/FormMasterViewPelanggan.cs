﻿using FirebirdSql.Data.FirebirdClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using TokoBuku.BaseForm.EditForm;
using TokoBuku.BaseForm.Master.Input;
using TokoBuku.BaseForm.Transaksi.HutangPiutang;
using TokoBuku.DbUtility;

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
            this.RefreshDataPelanggan();
        }

        private void RefreshDataPelanggan()
        {
            this.dataTableBase = new DataTable();
            //initTableRakKasKategoriPenerbitMaster();
            this.dataTableBase = DbLoadData.Pelanggan();
            this.Dgv1.DataSource = this.dataTableBase;
            this.Dgv1.DefaultCellStyle.NullValue = "-";
            this.Dgv1.Columns[0].Visible = false;
            this.Dgv1.Columns[5].DefaultCellStyle.Format = "c";
            this.Dgv1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.Dgv1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            for (int i = 1; i < this.Dgv1.ColumnCount; i++) { this.Dgv1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells; }
            this.Dgv1.Columns[this.Dgv1.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.Dgv1.Columns[this.Dgv1.ColumnCount - 1].MinimumWidth = 75;
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
                        var no_hp = form.inputNoHP.Replace("-", "").Replace("(", "").Replace(")", "").Replace("+", "").Replace(" ", "");
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

                            var lanjut = MessageBox.Show("Data Berhasil disimpan.\nAnda mau menambah data lagi?", "Success.", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
            foreach (DataGridViewRow row in Dgv1.SelectedRows)
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


        private void buttonEditData_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in Dgv1.SelectedRows)
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
                            this.dataTableBase = DbLoadData.Pelanggan();
                            this.Dgv1.DataSource = this.dataTableBase;
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

        private void buttonBayarHutang_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.Dgv1.SelectedRows)
            {
                int id_pelanggan = Convert.ToInt32(row.Cells[0].Value.ToString());
                double total_hutang = 0;
                if (double.TryParse(row.Cells["total_hutang"].Value.ToString(), out total_hutang) && total_hutang > 0)
                {
                    using (var form = new FormBayarHutangPelanggan())
                    {
                        form.IdPelanggan = id_pelanggan;
                        form.NamaPelanggan = row.Cells[1].Value.ToString();
                        form.TotalHutang = total_hutang;
                        form.ShowDialog();
                    }
                }

            }
        }

        private void Dgv1_SelectionChanged(object sender, EventArgs e) { }

        private void Dgv1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            double total_hutang = 0;
            if (!double.TryParse(this.Dgv1[5, e.RowIndex].Value.ToString(), out total_hutang) || total_hutang <= 0) { this.buttonBayarHutang.Enabled = false; }
            else { this.buttonBayarHutang.Enabled = true; }
        }
    }
}

