using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using TokoBuku.BaseForm.EditForm;
using TokoBuku.BaseForm.Master.Input;
using TokoBuku.BaseForm.TipeData.DataBase;

namespace TokoBuku.BaseForm.Master
{
    public partial class FormMasterViewKasMaster : Form
    {
        public DataTable dataTableBase { get; set; }

        public Form formData;

        public FormMasterViewKasMaster()
        {
            InitializeComponent();
        }


        private void FormMasterDataViewer_Load(object sender, EventArgs e)
        {
            this.ActiveControl = this.buttonAddData;
            this.RefreshDataKas();
        }

        private void RefreshDataKas()
        {
            this.dataTableBase = new DataTable();
            //initTableRakKasKategoriPenerbitMaster();
            this.dataTableBase = DbUtility.Master.Kas.LoadKas();
            this.dataGridView1.DataSource = this.dataTableBase;
            this.dataGridView1.Columns[0].Visible = false;
            this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridView1.Columns[2].DefaultCellStyle.Format = "C";
            this.dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[1].FillWeight = 40;
            this.dataGridView1.Columns[3].FillWeight = 60;
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
                using (var form = FormInput.Kas())
                {
                    var result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        var kas = form.Kas;
                        int Ids;
                        try
                        {
                            Ids = DbUtility.Master.Kas.SaveKas(kas);
                            this.RefreshDataKas();

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
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                int ids = Convert.ToInt32(row.Cells[0].Value.ToString());
                string nama = row.Cells[1].Value.ToString();
                var res_ = MessageBox.Show($"Apakah anda yakin mau menghapus data {nama}?", "Hapus Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res_ == DialogResult.Yes)
                {
                    try
                    {
                        DbUtility.Master.Kas.DeleteKas(ids);
                        MessageBox.Show($"Data {nama} berhasil dihapus.");
                        this.RefreshDataKas();
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
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                using (var form = FormEdit.Kas(this.ConvertRowToKas(row)))
                {
                    var result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        var Kas = form.Kas;
                        try
                        {
                            DbUtility.Master.Kas.EditKas(Kas);
                            MessageBox.Show("Data berhasil di update.", "Success.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.RefreshDataKas();
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

        private TKas ConvertRowToKas(DataGridViewRow row)
        {
            TKas kas = new TKas();
            kas.Id = Convert.ToInt32(row.Cells[0].Value.ToString());
            kas.Nama = row.Cells[1].Value.ToString();
            kas.Saldo = Convert.ToDouble(row.Cells[2].Value.ToString());
            kas.Keterangan = row.Cells["keterangan"].Value.ToString();
            return kas;
        }
    }
}

