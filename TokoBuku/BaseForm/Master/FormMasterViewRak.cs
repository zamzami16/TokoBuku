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
        private FbConnection DbConnection = ConnectDB.Connetc();
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
            this.dataTableBase = DbLoadData.Rak();
            this.dataGridView1.DataSource = this.dataTableBase;
            this.dataGridView1.Columns[0].Visible= false;   
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
                        var keterangan = form.ValueKeterangan;

                        try
                        {
                            var ids = TokoBuku.DbUtility.DbSaveData.Rak(nama: namaInput, keterangan: keterangan);

                            // update grid view and data table
                            this.dataTableBase = DbLoadData.Rak();
                            this.dataGridView1.DataSource = this.dataTableBase;

                            var results = MessageBox.Show("DATA BERHASIL DISIMPAN.\nANDA MAU MENAMBAH DATA LAGI?", "Success.", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (results != DialogResult.Yes)
                            {
                                Loop = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            if (ex.Message.ToLower().Contains("primary or unique"))
                            {
                                var results = MessageBox.Show("Data Sudah Ada.\nMau menambah data yang lain?", "Error.", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                                if (results != DialogResult.Yes)
                                {
                                    Loop = false;
                                }
                            }
                            else
                            {
                                MessageBox.Show($"Error: {ex.Message}");    
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
                int selectedId = Convert.ToInt32(row.Cells[0].Value.ToString());
                string selectedName = row.Cells[1].Value.ToString();
                try
                {
                    TokoBuku.DbUtility.DbDeleteData.Rak(selectedId);
                    MessageBox.Show($"Data {selectedName} deleted.", "Succes.");
                }
                catch (Exception ex)
                {
                    if (ex.Message.ToLower().Contains("foreign key"))
                    {
                        MessageBox.Show("Data tidak bisa dihapus\n.Data sudah dipakai di Data Barang.");
                    }
                    else
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                this.dataGridView1.Rows.Remove(row);
            }
        }

        private void buttonEditData_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                string selectedName = row.Cells[1].Value.ToString();
                int Ids = Convert.ToInt32(row.Cells[0].Value.ToString());
                var keterangan_ = row.Cells[2].Value.ToString();
                using (var form = FormEdit.Rak(selectedName, keterangan_))
                {
                    var result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        var changedName = form.ChangedName;
                        var keterangan = form.Keterangan;
                        //MessageBox.Show("Changed Name: "+changedName);
                        try
                        {
                            TokoBuku.DbUtility.DbEditData.Rak(Ids: Ids, nama: changedName, keterangan: keterangan);
                            MessageBox.Show("Data berhasil di update.", "Success.", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            this.dataTableBase = TokoBuku.DbUtility.DbLoadData.Rak();
                            this.dataGridView1.DataSource = this.dataTableBase;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error: {ex.Message}", "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //throw;
                        }
                    }
                }
            }
        }
    }
}

