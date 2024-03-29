﻿using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using TokoBuku.BaseForm.EditForm;
using TokoBuku.BaseForm.Master.Input;
using TokoBuku.BaseForm.TipeData.DataBase;
using TokoBuku.BaseForm.Transaksi.HutangPiutang;
using TokoBuku.DbUtility;

namespace TokoBuku.BaseForm.Master
{
    public partial class FormMasterViewPelanggan : Form
    {
        public DataTable dataTableBase { get; set; }

        public FormMasterViewPelanggan formData;

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
            this.Dgv1.Columns["total_hutang"].DefaultCellStyle.Format = "c";
            this.Dgv1.Columns["piutang_sudah_dibayar"].DefaultCellStyle.Format = "c";
            this.Dgv1.Columns["piutang_belum_dibayar"].DefaultCellStyle.Format = "c";
            this.Dgv1.Columns["total_hutang"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.Dgv1.Columns["piutang_sudah_dibayar"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.Dgv1.Columns["piutang_belum_dibayar"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            for (int i = 1; i < this.Dgv1.ColumnCount; i++) { this.Dgv1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells; }
            this.Dgv1.Columns[this.Dgv1.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.Dgv1.Columns[this.Dgv1.ColumnCount - 1].MinimumWidth = 75;

            this.Dgv1.Columns["total_hutang"].HeaderText = "Total Hutang";
            this.Dgv1.Columns["piutang_sudah_dibayar"].HeaderText = "Sudah Bayar";
            this.Dgv1.Columns["piutang_belum_dibayar"].HeaderText = "Belum Bayar";
            this.Dgv1.Columns["tenggat_bayar_terdekat"].HeaderText = "Tenggat Bayar Terdekat";
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
                        var pelanggan = form.Pelanggan;
                        int ids;
                        try
                        {
                            ids = DbUtility.Master.Pelanggan.SavePelanggan(pelanggan: pelanggan);
                            this.RefreshDataPelanggan();

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
                if (MessageBox.Show($"Apakah anda yakin mau menghapus Data Pelanggan {nama}?", "Hapus Data?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        DbDeleteData.Pelanggan(ids);
                        MessageBox.Show($"Data {nama} berhasil dihapus.", "Success.");
                        this.RefreshDataPelanggan();
                        /*DataRow rows = ((DataRowView)row.DataBoundItem).Row;
                        this.dataTableBase.Rows.Remove(rows);*/
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        throw;
                    }
                }
            }
        }


        private void buttonEditData_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in Dgv1.SelectedRows)
            {
                int Ids = Convert.ToInt32(row.Cells[0].Value.ToString());
                var namaAwal = row.Cells[1].Value.ToString();
                using (var form = FormEdit.Pelanggan(this.ConvertRowToPelanggan(row)))
                {
                    var result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        var pelanggan = form.Pelanggan;
                        pelanggan.Id = Ids;
                        try
                        {
                            DbUtility.Master.Pelanggan.EditPelanggan(pelanggan);
                            MessageBox.Show($"Data berhasil di update.", "Success.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.RefreshDataPelanggan();
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

        private TPelanggan ConvertRowToPelanggan(DataGridViewRow row)
        {
            TPelanggan pelanggan = new TPelanggan();
            pelanggan.Id = Convert.ToInt32(row.Cells[0].Value.ToString());
            pelanggan.Nama = row.Cells[1].Value.ToString();
            pelanggan.Alamat = row.Cells[2].Value.ToString();
            pelanggan.NoHp = row.Cells[3].Value.ToString();
            pelanggan.Email = row.Cells[4].Value.ToString();
            pelanggan.Keterangan = row.Cells["keterangan"].Value.ToString();
            return pelanggan;
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
                this.RefreshDataPelanggan();
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

