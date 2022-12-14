
namespace TokoBuku.Transaksi
{
    partial class Pembelian
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Pembelian));
            this.tableLayoutPanelUtama = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelAtas = new System.Windows.Forms.TableLayoutPanel();
            this.textTotalBayar = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonAdmin = new System.Windows.Forms.Button();
            this.buttonPelanggan = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.labelKasir = new System.Windows.Forms.Label();
            this.labelTanggalTransaksi = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.buttonJam = new System.Windows.Forms.Button();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxKodeItem = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxNamaItem = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxQty = new System.Windows.Forms.TextBox();
            this.comboSatuan = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Kode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NamaBarang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Jumlah = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Satuan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Harga = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Diskon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Hapus = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label9 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboMetodeBayar = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonBawahProcess = new System.Windows.Forms.Button();
            this.buttonBawahCancel = new System.Windows.Forms.Button();
            this.tableLayoutPanelUtama.SuspendLayout();
            this.tableLayoutPanelAtas.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelUtama
            // 
            this.tableLayoutPanelUtama.ColumnCount = 1;
            this.tableLayoutPanelUtama.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelUtama.Controls.Add(this.tableLayoutPanelAtas, 0, 0);
            this.tableLayoutPanelUtama.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanelUtama.Controls.Add(this.dataGridView1, 0, 2);
            this.tableLayoutPanelUtama.Controls.Add(this.tableLayoutPanel1, 0, 3);
            this.tableLayoutPanelUtama.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelUtama.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelUtama.MinimumSize = new System.Drawing.Size(700, 400);
            this.tableLayoutPanelUtama.Name = "tableLayoutPanelUtama";
            this.tableLayoutPanelUtama.RowCount = 4;
            this.tableLayoutPanelUtama.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanelUtama.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanelUtama.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelUtama.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanelUtama.Size = new System.Drawing.Size(799, 496);
            this.tableLayoutPanelUtama.TabIndex = 0;
            // 
            // tableLayoutPanelAtas
            // 
            this.tableLayoutPanelAtas.ColumnCount = 2;
            this.tableLayoutPanelAtas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.35309F));
            this.tableLayoutPanelAtas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 59.64691F));
            this.tableLayoutPanelAtas.Controls.Add(this.textTotalBayar, 1, 0);
            this.tableLayoutPanelAtas.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanelAtas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelAtas.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelAtas.Name = "tableLayoutPanelAtas";
            this.tableLayoutPanelAtas.RowCount = 1;
            this.tableLayoutPanelAtas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelAtas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 114F));
            this.tableLayoutPanelAtas.Size = new System.Drawing.Size(793, 114);
            this.tableLayoutPanelAtas.TabIndex = 0;
            // 
            // textTotalBayar
            // 
            this.textTotalBayar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textTotalBayar.Font = new System.Drawing.Font("Microsoft Sans Serif", 45F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textTotalBayar.Location = new System.Drawing.Point(322, 3);
            this.textTotalBayar.Name = "textTotalBayar";
            this.textTotalBayar.Size = new System.Drawing.Size(468, 75);
            this.textTotalBayar.TabIndex = 0;
            this.textTotalBayar.Text = "0";
            this.textTotalBayar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.label5, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.buttonAdmin, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.buttonPelanggan, 2, 3);
            this.tableLayoutPanel3.Controls.Add(this.label3, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.labelKasir, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.labelTanggalTransaksi, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.comboBox1, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.buttonJam, 2, 2);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 4;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(313, 108);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "Kasir :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 27);
            this.label2.TabIndex = 1;
            this.label2.Text = "No. Transaksi :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 27);
            this.label4.TabIndex = 3;
            this.label4.Text = "Tanggal :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 27);
            this.label5.TabIndex = 4;
            this.label5.Text = "Pelanggan :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // buttonAdmin
            // 
            this.buttonAdmin.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonAdmin.Location = new System.Drawing.Point(194, 3);
            this.buttonAdmin.Name = "buttonAdmin";
            this.buttonAdmin.Size = new System.Drawing.Size(64, 21);
            this.buttonAdmin.TabIndex = 5;
            this.buttonAdmin.Text = "admin";
            this.buttonAdmin.UseVisualStyleBackColor = true;
            // 
            // buttonPelanggan
            // 
            this.buttonPelanggan.Location = new System.Drawing.Point(194, 84);
            this.buttonPelanggan.Name = "buttonPelanggan";
            this.buttonPelanggan.Size = new System.Drawing.Size(64, 21);
            this.buttonPelanggan.TabIndex = 8;
            this.buttonPelanggan.Text = "Cari";
            this.buttonPelanggan.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.tableLayoutPanel3.SetColumnSpan(this.label3, 2);
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(113, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(197, 27);
            this.label3.TabIndex = 9;
            this.label3.Text = "Auto";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelKasir
            // 
            this.labelKasir.AutoSize = true;
            this.labelKasir.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelKasir.Location = new System.Drawing.Point(113, 0);
            this.labelKasir.Name = "labelKasir";
            this.labelKasir.Size = new System.Drawing.Size(75, 27);
            this.labelKasir.TabIndex = 10;
            this.labelKasir.Text = "Nama Kasir";
            this.labelKasir.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelTanggalTransaksi
            // 
            this.labelTanggalTransaksi.AutoSize = true;
            this.labelTanggalTransaksi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTanggalTransaksi.Location = new System.Drawing.Point(113, 54);
            this.labelTanggalTransaksi.Name = "labelTanggalTransaksi";
            this.labelTanggalTransaksi.Size = new System.Drawing.Size(75, 27);
            this.labelTanggalTransaksi.TabIndex = 11;
            this.labelTanggalTransaksi.Text = "Tanggal";
            this.labelTanggalTransaksi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBox1
            // 
            this.comboBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Umum",
            "Pelanggan 1",
            "Pelanggan 2"});
            this.comboBox1.Location = new System.Drawing.Point(113, 84);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(75, 21);
            this.comboBox1.TabIndex = 12;
            this.comboBox1.Text = "Umum";
            // 
            // buttonJam
            // 
            this.buttonJam.Enabled = false;
            this.buttonJam.Location = new System.Drawing.Point(194, 57);
            this.buttonJam.Name = "buttonJam";
            this.buttonJam.Size = new System.Drawing.Size(64, 21);
            this.buttonJam.TabIndex = 7;
            this.buttonJam.Text = "jam";
            this.buttonJam.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62.5F));
            this.tableLayoutPanel4.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 135);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(793, 52);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label6);
            this.flowLayoutPanel1.Controls.Add(this.textBoxKodeItem);
            this.flowLayoutPanel1.Controls.Add(this.label7);
            this.flowLayoutPanel1.Controls.Add(this.textBoxNamaItem);
            this.flowLayoutPanel1.Controls.Add(this.label8);
            this.flowLayoutPanel1.Controls.Add(this.textBoxQty);
            this.flowLayoutPanel1.Controls.Add(this.comboSatuan);
            this.flowLayoutPanel1.Controls.Add(this.button1);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(787, 32);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.label6.Location = new System.Drawing.Point(3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 32);
            this.label6.TabIndex = 0;
            this.label6.Text = "Kode Item :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxKodeItem
            // 
            this.textBoxKodeItem.Location = new System.Drawing.Point(70, 3);
            this.textBoxKodeItem.Name = "textBoxKodeItem";
            this.textBoxKodeItem.Size = new System.Drawing.Size(37, 20);
            this.textBoxKodeItem.TabIndex = 1;
            this.textBoxKodeItem.TextChanged += new System.EventHandler(this.textBoxKodeItem_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Left;
            this.label7.Location = new System.Drawing.Point(113, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 32);
            this.label7.TabIndex = 2;
            this.label7.Text = "Nama Item :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxNamaItem
            // 
            this.textBoxNamaItem.Location = new System.Drawing.Point(183, 3);
            this.textBoxNamaItem.Name = "textBoxNamaItem";
            this.textBoxNamaItem.Size = new System.Drawing.Size(125, 20);
            this.textBoxNamaItem.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Left;
            this.label8.Location = new System.Drawing.Point(314, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 32);
            this.label8.TabIndex = 4;
            this.label8.Text = "Qty :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxQty
            // 
            this.textBoxQty.Location = new System.Drawing.Point(349, 3);
            this.textBoxQty.Name = "textBoxQty";
            this.textBoxQty.Size = new System.Drawing.Size(66, 20);
            this.textBoxQty.TabIndex = 5;
            // 
            // comboSatuan
            // 
            this.comboSatuan.FormattingEnabled = true;
            this.comboSatuan.Items.AddRange(new object[] {
            "Pcs",
            "Packs"});
            this.comboSatuan.Location = new System.Drawing.Point(421, 3);
            this.comboSatuan.Name = "comboSatuan";
            this.comboSatuan.Size = new System.Drawing.Size(54, 21);
            this.comboSatuan.TabIndex = 6;
            this.comboSatuan.Text = "Pcs";
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(481, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(34, 26);
            this.button1.TabIndex = 1;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Kode,
            this.NamaBarang,
            this.Jumlah,
            this.Satuan,
            this.Harga,
            this.Diskon,
            this.Total,
            this.Hapus});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 193);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(793, 150);
            this.dataGridView1.TabIndex = 2;
            // 
            // Kode
            // 
            this.Kode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Kode.FillWeight = 10F;
            this.Kode.HeaderText = "Kode";
            this.Kode.Name = "Kode";
            this.Kode.ReadOnly = true;
            this.Kode.ToolTipText = "Kode";
            // 
            // NamaBarang
            // 
            this.NamaBarang.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NamaBarang.FillWeight = 30F;
            this.NamaBarang.HeaderText = "Nama Barang";
            this.NamaBarang.Name = "NamaBarang";
            this.NamaBarang.ReadOnly = true;
            // 
            // Jumlah
            // 
            this.Jumlah.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Jumlah.FillWeight = 10F;
            this.Jumlah.HeaderText = "Jumlah";
            this.Jumlah.Name = "Jumlah";
            this.Jumlah.ReadOnly = true;
            // 
            // Satuan
            // 
            this.Satuan.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Satuan.FillWeight = 10F;
            this.Satuan.HeaderText = "Satuan";
            this.Satuan.Name = "Satuan";
            this.Satuan.ReadOnly = true;
            // 
            // Harga
            // 
            this.Harga.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Harga.FillWeight = 10F;
            this.Harga.HeaderText = "Harga";
            this.Harga.Name = "Harga";
            this.Harga.ReadOnly = true;
            // 
            // Diskon
            // 
            this.Diskon.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Diskon.FillWeight = 10F;
            this.Diskon.HeaderText = "Diskon";
            this.Diskon.Name = "Diskon";
            this.Diskon.ReadOnly = true;
            // 
            // Total
            // 
            this.Total.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Total.FillWeight = 10F;
            this.Total.HeaderText = "Total";
            this.Total.Name = "Total";
            this.Total.ReadOnly = true;
            // 
            // Hapus
            // 
            this.Hapus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Hapus.HeaderText = "Hapus";
            this.Hapus.Name = "Hapus";
            this.Hapus.ReadOnly = true;
            this.Hapus.Width = 50;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.98361F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 59.01639F));
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBox1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 349);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 38.65546F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 61.34454F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(793, 144);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(3, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(129, 20);
            this.label9.TabIndex = 0;
            this.label9.Text = "PEMBAYARAN :";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.comboMetodeBayar);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 28);
            this.panel1.Name = "panel1";
            this.tableLayoutPanel1.SetRowSpan(this.panel1, 2);
            this.panel1.Size = new System.Drawing.Size(319, 73);
            this.panel1.TabIndex = 2;
            // 
            // comboMetodeBayar
            // 
            this.comboMetodeBayar.FormattingEnabled = true;
            this.comboMetodeBayar.Location = new System.Drawing.Point(124, 10);
            this.comboMetodeBayar.Name = "comboMetodeBayar";
            this.comboMetodeBayar.Size = new System.Drawing.Size(184, 21);
            this.comboMetodeBayar.TabIndex = 2;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 10);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(111, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Metode Pembayaran :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label11);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(328, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(462, 19);
            this.panel2.TabIndex = 4;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(3, 2);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(94, 17);
            this.label11.TabIndex = 3;
            this.label11.Text = "Jumlah Bayar";
            this.label11.Click += new System.EventHandler(this.label11_Click);
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(328, 28);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(462, 38);
            this.textBox1.TabIndex = 5;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.buttonBawahProcess);
            this.flowLayoutPanel2.Controls.Add(this.buttonBawahCancel);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(328, 107);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(462, 34);
            this.flowLayoutPanel2.TabIndex = 6;
            // 
            // buttonBawahProcess
            // 
            this.buttonBawahProcess.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonBawahProcess.Location = new System.Drawing.Point(3, 3);
            this.buttonBawahProcess.Name = "buttonBawahProcess";
            this.buttonBawahProcess.Size = new System.Drawing.Size(91, 31);
            this.buttonBawahProcess.TabIndex = 0;
            this.buttonBawahProcess.Text = "LANJUTKAN";
            this.buttonBawahProcess.UseVisualStyleBackColor = true;
            // 
            // buttonBawahCancel
            // 
            this.buttonBawahCancel.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonBawahCancel.Location = new System.Drawing.Point(100, 3);
            this.buttonBawahCancel.Name = "buttonBawahCancel";
            this.buttonBawahCancel.Size = new System.Drawing.Size(91, 31);
            this.buttonBawahCancel.TabIndex = 1;
            this.buttonBawahCancel.Text = "BATAL";
            this.buttonBawahCancel.UseVisualStyleBackColor = true;
            // 
            // Pembelian
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 496);
            this.Controls.Add(this.tableLayoutPanelUtama);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.Name = "Pembelian";
            this.Text = "Penjualan";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tableLayoutPanelUtama.ResumeLayout(false);
            this.tableLayoutPanelAtas.ResumeLayout(false);
            this.tableLayoutPanelAtas.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelUtama;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelAtas;
        private System.Windows.Forms.TextBox textTotalBayar;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonAdmin;
        private System.Windows.Forms.Button buttonJam;
        private System.Windows.Forms.Button buttonPelanggan;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelKasir;
        private System.Windows.Forms.Label labelTanggalTransaksi;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxKodeItem;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxNamaItem;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxQty;
        private System.Windows.Forms.ComboBox comboSatuan;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kode;
        private System.Windows.Forms.DataGridViewTextBoxColumn NamaBarang;
        private System.Windows.Forms.DataGridViewTextBoxColumn Jumlah;
        private System.Windows.Forms.DataGridViewTextBoxColumn Satuan;
        private System.Windows.Forms.DataGridViewTextBoxColumn Harga;
        private System.Windows.Forms.DataGridViewTextBoxColumn Diskon;
        private System.Windows.Forms.DataGridViewTextBoxColumn Total;
        private System.Windows.Forms.DataGridViewButtonColumn Hapus;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox comboMetodeBayar;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button buttonBawahProcess;
        private System.Windows.Forms.Button buttonBawahCancel;
    }
}