using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TokoBuku.BaseForm.Transaksi
{
    public partial class HistoriPenjualan : Form
    {
        public HistoriPenjualan()
        {
            InitializeComponent();
        }
        /// TODO: tambahkan filter
        private void HistoriPembelian_Load(object sender, EventArgs e)
        {
            this.dgv.DataSource = TokoBuku.DbUtility.Transactions.Penjualan.GetHistoriPenjualan();
            this.dgv.Columns[0].Visible= false;
            for (int i = 1; i < this.dgv.ColumnCount; i++)
            {
                this.dgv.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            this.dgv.Columns["keterangan"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dgv.Columns[1].HeaderText = "No Transaksi";
            this.dgv.Columns[2].HeaderText = "kasir";
            this.dgv.Columns[3].HeaderText = "pelanggan";
            this.dgv.Columns[4].HeaderText = "total";
            this.dgv.Columns[5].HeaderText = "tanggal";
            this.dgv.Columns[6].HeaderText = "pembayaran";
            this.dgv.Columns[7].HeaderText = "kas";

            this.dgv.Columns[4].DefaultCellStyle.Format = "c";
            this.dgv.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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
    }
}
