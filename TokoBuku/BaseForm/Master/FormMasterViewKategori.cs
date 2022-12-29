using FirebirdSql.Data.FirebirdClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using TokoBuku.BaseForm.EditForm;
using TokoBuku.BaseForm.Master.Input;
using TokoBuku.DbUtility;

namespace TokoBuku.BaseForm.Master
{
    public partial class FormMasterViewKategori : Form
    {
        private FbConnection DbConnection = ConnectDB.Connetc();
        public DataTable dataTableBase { get; set; }

        public Form formData;

        public FormMasterViewKategori()
        {
            InitializeComponent();
        }


        private void FormMasterDataViewer_Load(object sender, EventArgs e)
        {
            this.ActiveControl = this.buttonAddData;
            this.dataTableBase = new DataTable();
            //initTableRakKasKategoriPenerbitMaster();
            this.RefreshData();
            this.dataGridView1.Columns[0].Visible = false;
            this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[1].FillWeight = 60;
            this.dataGridView1.Columns[2].FillWeight = 40;
        }

        private void FormMasterDataViewer_Deactivate(object sender, EventArgs e) { this.Close(); }

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
                using (var form = FormInput.Kategori())
                {
                    var result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        var namaInput = form.ValueName;
                        var keterangan = form.ValueKeterangan;
                        //MessageBox.Show("Eksekusi atas....");
                        try
                        {
                            var ids = DbSaveData.Kategori(nama: namaInput, keterangan: keterangan);
                            var results = MessageBox.Show("DATA BERHASIL DISIMPAN.\nANDA MAU MENAMBAH DATA LAGI?", "Success.", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            this.RefreshData();
                            if (results != DialogResult.Yes) { Loop = false; }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error: {ex.Message}", "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                var selectedId = Convert.ToInt32(row.Cells[0].Value.ToString());
                var nama_ = row.Cells[1].Value.ToString();
                var results = MessageBox.Show($"Hapus Data {nama_}?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (results == DialogResult.Yes)
                {
                    try
                    {
                        DbDeleteData.Kategori(Ids: selectedId);
                        this.RefreshData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}", "Errro.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //throw;
                    }
                }
            }
        }

        private void buttonEditData_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                string selectedName = row.Cells[1].Value.ToString();
                int Ids = Convert.ToInt32(row.Cells[0].Value.ToString());
                using (var form = FormEdit.Kategori(selectedName))
                {
                    var result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        var changedName = form.ChangedName;
                        var keterangan = form.Keterangan;
                        //MessageBox.Show("Changed Name: "+changedName);
                        try
                        {
                            DbEditData.Kategori(Ids: Ids, nama: changedName, keterangan: keterangan);
                            this.RefreshData();
                            MessageBox.Show("Data Berhasil di update.", "Success.", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void RefreshData()
        {
            this.dataTableBase = DbLoadData.Kategori();
            this.dataGridView1.DataSource = this.dataTableBase;
        }
    }
}

