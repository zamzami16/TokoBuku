using FirebirdSql.Data.FirebirdClient;
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
    public partial class FormMasterViewSupplier : Form
    {
        private FbConnection DbConnection = ConnectDB.Connetc();
        public DataTable dataTableBase { get; set; }

        public Form formData;

        public FormMasterViewSupplier()
        {
            InitializeComponent();
        }


        private void FormMasterDataViewer_Load(object sender, EventArgs e)
        {
            this.ActiveControl = this.buttonAddData;
            this.RefreshDataSupplier();
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
                using (var form = FormInput.Supplier())
                {
                    var result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        var nama = form.inputNama;
                        var alamat = form.inputALamat;
                        var no_hp = form.inputNoHP.Replace("-", "").Replace("(", "").Replace(")", "").Replace("+", "").Replace(" ", "");
                        if (no_hp.Length < 6)
                        {
                            no_hp = string.Empty;
                        }
                        var email = form.inputEmail;
                        var keterangan = form.inputKeterangan;
                        var status = form.inputStatus;
                        int ids;
                        try
                        {
                            ids = DbSaveData.Supplier(nama: nama, alamat: alamat, email: email,
                            no_hp: no_hp, keterangan: keterangan, status: status);

                            this.RefreshDataSupplier();
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

        private void RefreshDataSupplier()
        {
            this.dataTableBase = new DataTable();
            this.dataTableBase = TokoBuku.DbUtility.Master.Supplier.GetDataSupplier();
            var xxx = this.dataTableBase;
            this.dataGridView1.DataSource = this.dataTableBase;
            this.dataGridView1.Columns[0].Visible = false;
            this.dataGridView1.Columns[this.dataGridView1.ColumnCount-1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; // Keterangan
            this.dataGridView1.Columns[this.dataGridView1.ColumnCount - 1].MinimumWidth = 75;
            for (int i = 1; i < this.dataGridView1.ColumnCount-1; i++)
            {
                this.dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            this.dataGridView1.Columns["total_hutang"].DefaultCellStyle.Format = "C";
            this.dataGridView1.Columns["total_hutang"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                int ids = Convert.ToInt32(row.Cells[0].Value.ToString());
                string nama = row.Cells[1].Value.ToString();
                try
                {
                    DbDeleteData.Supplier(ids);
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
        }

        private void TampilkanBerhasilSimpan(string message)
        {
            MessageBox.Show(message, "Succes.", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }*/

        private void buttonEditData_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                int Ids = Convert.ToInt32(row.Cells[0].Value.ToString());
                var namaAwal = row.Cells[1].Value.ToString();
                using (var form = FormEdit.Supplier(row))
                {
                    var result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        var nama = form.inputNama;
                        var alamat = form.inputALamat;
                        var no_hp = form.inputNoHP.Replace("-", "").Replace("(", "").Replace(")", "").Replace("+", "").Replace(" ", "");
                        if (no_hp.Length < 6) no_hp = string.Empty;
                        var email = form.inputEmail;
                        var keterangan = form.inputKeterangan;
                        var status = form.inputStatus;
                        try
                        {
                            TokoBuku.DbUtility.Master.Supplier.EditSupplier(Ids: Ids, nama: nama, 
                                alamat: alamat, no_hp: no_hp, email: email, keterangan: keterangan);

                            MessageBox.Show($"Data berhasil di update.", "Success.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.RefreshDataSupplier();
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
            /// TODO: Tambahkan metode bayar hutang
            foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
            {
                int id_supplier = Convert.ToInt32(row.Cells[0].Value.ToString());
                double total_hutang;
                if (double.TryParse(row.Cells["total_hutang"].Value.ToString(), out total_hutang))
                {
                    using (var form = new FormBayarHutangSupplier())
                    {
                        form.IdSupplier = id_supplier;
                        form.NamaSupplier = row.Cells[1].Value.ToString();
                        form.TotalHutang = total_hutang;
                        form.ShowDialog();
                    }
                } 

            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            
        }
    }
}

