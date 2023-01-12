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
    public partial class FormMasterViewBarang : Form
    {
        private FbConnection DbConnection = ConnectDB.Connetc();
        public DataTable dataTableBase { get; set; }

        public Form formData;

        public FormMasterViewBarang()
        {
            InitializeComponent();
        }


        private void FormMasterDataViewer_Load(object sender, EventArgs e)
        {
            this.RefreshDataBarang();
        }

        private void RefreshDataBarang()
        {
            this.dataTableBase = new DataTable();
            this.ActiveControl = this.buttonAddData;

            this.dataTableBase = DbUtility.Master.Barang.GetDataBarang();
            this.dataGridView1.DataSource = this.dataTableBase;
            this.dataGridView1.Columns[0].Visible = false;      // id
            for (int i = 1; i < this.dataGridView1.ColumnCount; i++)
            {
                this.dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            this.dataGridView1.Columns["nama_barang"].MinimumWidth = 75;
            this.dataGridView1.Columns["nama_barang"].HeaderText = "Nama Barang";
            this.dataGridView1.Columns["keterangan"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns["keterangan"].MinimumWidth = 75;

            /// Format Grid View
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            // Harga jual
            this.dataGridView1.Columns["harga_jual"].DefaultCellStyle.Format = "C";
            this.dataGridView1.Columns["harga_jual"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridView1.Columns["harga_jual"].HeaderText = "Harga Jual";
            // Harga Beli
            this.dataGridView1.Columns["harga_beli"].DefaultCellStyle.Format = "C";
            this.dataGridView1.Columns["harga_beli"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridView1.Columns["harga_beli"].HeaderText = "Harga Beli";

            // Diskon
            this.dataGridView1.Columns["diskon"].DefaultCellStyle.Format = "0.00'%'";
            this.dataGridView1.Columns["diskon"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            // Stock
            this.dataGridView1.Columns["stock"].DefaultCellStyle.Format = "0' pcs'";
            // status
            this.dataGridView1.Columns["status"].Visible = false;

            this.labelPersediaan.Text = "Total Persediaan: " +
                TokoBuku.DbUtility.MasterDataBarang.GetPersediaan().ToString("C");
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
                using (var form = new FormInputDataBarang())
                {
                    var result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        /*var kategori = Convert.ToInt32(form.Kategori);
                        var penerbit = Convert.ToInt32(form.Penerbit);
                        var rak = Convert.ToInt32(form.Rak);
                        var kode = form.KodeBarang;
                        var namaBarang = form.NamaBarang;
                        var stock = form.Stock;
                        double hargaBeli = form.HargaBeli;
                        double harga = form.Harga;
                        var isbn = form.ISBN;
                        var penulis = form.Penulis;
                        double diskon = form.Diskon;
                        var status = form.Status;
                        var barCode = form.BarCode;
                        var keterangan = form.Keterangan;*/

                        var dbBarang = form.DbBarang;

                        try
                        {
                            int ids = DbUtility.Master.Barang.SaveBarang(dbBarang: dbBarang);

                            var results = MessageBox.Show("DATA BERHASIL DISIMPAN.\nANDA MAU MENAMBAH DATA LAGI?", "Success.", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            this.DbRefresh();
                            if (results != DialogResult.Yes)
                            {
                                Loop = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                string selectedId = row.Cells[0].Value.ToString();
                string selectedName = row.Cells[2].Value.ToString();
                var dialogs = MessageBox.Show($"Anda yakin mau menghapus data {selectedName}?", "Hapus Data?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogs == DialogResult.Yes)
                {
                    using (var con = ConnectDB.Connetc())
                    {
                        //MessageBox.Show("eksekusi awal try 1");
                        var strSql = "DELETE FROM barang WHERE id_barang=@Id";
                        using (var cmd = new FbCommand(strSql, con))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.Add("@id", selectedId);
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                        }
                    }
                    this.DbRefresh();
                }
            }
        }

        private void buttonEditData_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                int selectedId = Convert.ToInt32(row.Cells[0].Value.ToString());
                using (var form = FormEdit.Barang(row))
                {
                    var result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        var barang = form.DbBarang;
                        barang.IdBarang = selectedId;
                        try
                        {
                            DbUtility.Master.Barang.UpdateDataBarang(dbBarang: barang);
                            MessageBox.Show("Data berhasil diupdated.", "Success.",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DbRefresh();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error @Edit barang :" + ex.Message, "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            throw;
                        }
                    }
                }
            }
        }

        private void DbRefresh()
        {
            this.dataTableBase = DbUtility.Master.Barang.GetDataBarang();
            this.dataGridView1.DataSource = this.dataTableBase;
            this.labelPersediaan.Text = "Total Persediaan: " +
                TokoBuku.DbUtility.MasterDataBarang.GetPersediaan().ToString("C");
        }
    }
}

