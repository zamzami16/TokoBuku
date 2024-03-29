﻿
namespace TokoBuku.BaseForm.Master.Input
{
    partial class FormInputDataBarang
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormInputDataBarang));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonGenerateBarCode = new System.Windows.Forms.Button();
            this.buttonGenerateKode = new System.Windows.Forms.Button();
            this.textBoxKode = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.richTextBoxKeterangan = new System.Windows.Forms.RichTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.numericStock = new System.Windows.Forms.NumericUpDown();
            this.buttonTambahPenerbit = new System.Windows.Forms.Button();
            this.buttonTambahKategori = new System.Windows.Forms.Button();
            this.buttonTambahRak = new System.Windows.Forms.Button();
            this.textBoxBarCode = new System.Windows.Forms.TextBox();
            this.textBoxISBN = new System.Windows.Forms.TextBox();
            this.textBoxPenulis = new System.Windows.Forms.TextBox();
            this.comboBoxRak = new System.Windows.Forms.ComboBox();
            this.comboBoxPenerbit = new System.Windows.Forms.ComboBox();
            this.comboBoxKategori = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxDiskon = new System.Windows.Forms.TextBox();
            this.textBoXHarga = new System.Windows.Forms.TextBox();
            this.textBoxNamaBarang = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelBarang = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonSaveData = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxHargaBeli = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericStock)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.labelTitle, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(577, 420);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.Location = new System.Drawing.Point(3, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(571, 40);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "TAMBAH DATA BARANG";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBoxHargaBeli);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.buttonGenerateBarCode);
            this.panel1.Controls.Add(this.buttonGenerateKode);
            this.panel1.Controls.Add(this.textBoxKode);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.richTextBoxKeterangan);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.numericStock);
            this.panel1.Controls.Add(this.buttonTambahPenerbit);
            this.panel1.Controls.Add(this.buttonTambahKategori);
            this.panel1.Controls.Add(this.buttonTambahRak);
            this.panel1.Controls.Add(this.textBoxBarCode);
            this.panel1.Controls.Add(this.textBoxISBN);
            this.panel1.Controls.Add(this.textBoxPenulis);
            this.panel1.Controls.Add(this.comboBoxRak);
            this.panel1.Controls.Add(this.comboBoxPenerbit);
            this.panel1.Controls.Add(this.comboBoxKategori);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.textBoxDiskon);
            this.panel1.Controls.Add(this.textBoXHarga);
            this.panel1.Controls.Add(this.textBoxNamaBarang);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.labelBarang);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 43);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(571, 334);
            this.panel1.TabIndex = 1;
            // 
            // buttonGenerateBarCode
            // 
            this.buttonGenerateBarCode.Image = ((System.Drawing.Image)(resources.GetObject("buttonGenerateBarCode.Image")));
            this.buttonGenerateBarCode.Location = new System.Drawing.Point(521, 252);
            this.buttonGenerateBarCode.Name = "buttonGenerateBarCode";
            this.buttonGenerateBarCode.Size = new System.Drawing.Size(29, 23);
            this.buttonGenerateBarCode.TabIndex = 32;
            this.buttonGenerateBarCode.TabStop = false;
            this.toolTip1.SetToolTip(this.buttonGenerateBarCode, "Generate");
            this.buttonGenerateBarCode.UseVisualStyleBackColor = true;
            this.buttonGenerateBarCode.Click += new System.EventHandler(this.buttonGenerateBarCode_Click);
            // 
            // buttonGenerateKode
            // 
            this.buttonGenerateKode.Image = ((System.Drawing.Image)(resources.GetObject("buttonGenerateKode.Image")));
            this.buttonGenerateKode.Location = new System.Drawing.Point(224, 88);
            this.buttonGenerateKode.Name = "buttonGenerateKode";
            this.buttonGenerateKode.Size = new System.Drawing.Size(29, 23);
            this.buttonGenerateKode.TabIndex = 31;
            this.buttonGenerateKode.TabStop = false;
            this.toolTip1.SetToolTip(this.buttonGenerateKode, "OTOMATIS");
            this.buttonGenerateKode.UseVisualStyleBackColor = true;
            this.buttonGenerateKode.Click += new System.EventHandler(this.buttonGenerateKode_Click);
            // 
            // textBoxKode
            // 
            this.textBoxKode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxKode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxKode.Location = new System.Drawing.Point(25, 88);
            this.textBoxKode.Name = "textBoxKode";
            this.textBoxKode.Size = new System.Drawing.Size(193, 23);
            this.textBoxKode.TabIndex = 1;
            this.textBoxKode.TabStop = false;
            this.textBoxKode.Text = "--OTOMATIS--";
            this.textBoxKode.Leave += new System.EventHandler(this.textBoxKode_Leave);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(25, 72);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(37, 13);
            this.label13.TabIndex = 29;
            this.label13.Text = "KODE";
            // 
            // richTextBoxKeterangan
            // 
            this.richTextBoxKeterangan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxKeterangan.Location = new System.Drawing.Point(151, 307);
            this.richTextBoxKeterangan.Name = "richTextBoxKeterangan";
            this.richTextBoxKeterangan.Size = new System.Drawing.Size(399, 23);
            this.richTextBoxKeterangan.TabIndex = 11;
            this.richTextBoxKeterangan.Text = "";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(148, 291);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(81, 13);
            this.label12.TabIndex = 28;
            this.label12.Text = "KETERANGAN";
            // 
            // numericStock
            // 
            this.numericStock.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericStock.Location = new System.Drawing.Point(25, 307);
            this.numericStock.Name = "numericStock";
            this.numericStock.Size = new System.Drawing.Size(108, 23);
            this.numericStock.TabIndex = 5;
            this.numericStock.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // buttonTambahPenerbit
            // 
            this.buttonTambahPenerbit.Image = ((System.Drawing.Image)(resources.GetObject("buttonTambahPenerbit.Image")));
            this.buttonTambahPenerbit.Location = new System.Drawing.Point(521, 88);
            this.buttonTambahPenerbit.Name = "buttonTambahPenerbit";
            this.buttonTambahPenerbit.Size = new System.Drawing.Size(29, 24);
            this.buttonTambahPenerbit.TabIndex = 26;
            this.buttonTambahPenerbit.TabStop = false;
            this.toolTip1.SetToolTip(this.buttonTambahPenerbit, "Tambah Data");
            this.buttonTambahPenerbit.UseVisualStyleBackColor = true;
            this.buttonTambahPenerbit.Click += new System.EventHandler(this.buttonTambahPenerbit_Click);
            // 
            // buttonTambahKategori
            // 
            this.buttonTambahKategori.Image = ((System.Drawing.Image)(resources.GetObject("buttonTambahKategori.Image")));
            this.buttonTambahKategori.Location = new System.Drawing.Point(521, 35);
            this.buttonTambahKategori.Name = "buttonTambahKategori";
            this.buttonTambahKategori.Size = new System.Drawing.Size(29, 24);
            this.buttonTambahKategori.TabIndex = 25;
            this.buttonTambahKategori.TabStop = false;
            this.toolTip1.SetToolTip(this.buttonTambahKategori, "Tambah Data");
            this.buttonTambahKategori.UseVisualStyleBackColor = true;
            this.buttonTambahKategori.Click += new System.EventHandler(this.buttonTambahKategori_Click);
            // 
            // buttonTambahRak
            // 
            this.buttonTambahRak.Image = ((System.Drawing.Image)(resources.GetObject("buttonTambahRak.Image")));
            this.buttonTambahRak.Location = new System.Drawing.Point(224, 252);
            this.buttonTambahRak.Name = "buttonTambahRak";
            this.buttonTambahRak.Size = new System.Drawing.Size(29, 24);
            this.buttonTambahRak.TabIndex = 24;
            this.buttonTambahRak.TabStop = false;
            this.toolTip1.SetToolTip(this.buttonTambahRak, "Tambah Data");
            this.buttonTambahRak.UseVisualStyleBackColor = true;
            this.buttonTambahRak.Click += new System.EventHandler(this.buttonTambahRak_Click);
            // 
            // textBoxBarCode
            // 
            this.textBoxBarCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxBarCode.Location = new System.Drawing.Point(289, 252);
            this.textBoxBarCode.Name = "textBoxBarCode";
            this.textBoxBarCode.Size = new System.Drawing.Size(226, 23);
            this.textBoxBarCode.TabIndex = 10;
            // 
            // textBoxISBN
            // 
            this.textBoxISBN.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxISBN.Location = new System.Drawing.Point(289, 197);
            this.textBoxISBN.Name = "textBoxISBN";
            this.textBoxISBN.Size = new System.Drawing.Size(226, 23);
            this.textBoxISBN.TabIndex = 9;
            // 
            // textBoxPenulis
            // 
            this.textBoxPenulis.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxPenulis.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPenulis.Location = new System.Drawing.Point(289, 143);
            this.textBoxPenulis.Name = "textBoxPenulis";
            this.textBoxPenulis.Size = new System.Drawing.Size(226, 23);
            this.textBoxPenulis.TabIndex = 8;
            // 
            // comboBoxRak
            // 
            this.comboBoxRak.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxRak.FormattingEnabled = true;
            this.comboBoxRak.Location = new System.Drawing.Point(25, 252);
            this.comboBoxRak.Name = "comboBoxRak";
            this.comboBoxRak.Size = new System.Drawing.Size(193, 24);
            this.comboBoxRak.TabIndex = 4;
            // 
            // comboBoxPenerbit
            // 
            this.comboBoxPenerbit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxPenerbit.FormattingEnabled = true;
            this.comboBoxPenerbit.Location = new System.Drawing.Point(289, 88);
            this.comboBoxPenerbit.Name = "comboBoxPenerbit";
            this.comboBoxPenerbit.Size = new System.Drawing.Size(226, 24);
            this.comboBoxPenerbit.TabIndex = 7;
            // 
            // comboBoxKategori
            // 
            this.comboBoxKategori.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxKategori.FormattingEnabled = true;
            this.comboBoxKategori.Location = new System.Drawing.Point(289, 35);
            this.comboBoxKategori.Name = "comboBoxKategori";
            this.comboBoxKategori.Size = new System.Drawing.Size(226, 24);
            this.comboBoxKategori.TabIndex = 6;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(233, 202);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(20, 17);
            this.label11.TabIndex = 14;
            this.label11.Text = "%";
            // 
            // textBoxDiskon
            // 
            this.textBoxDiskon.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxDiskon.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDiskon.Location = new System.Drawing.Point(188, 199);
            this.textBoxDiskon.Name = "textBoxDiskon";
            this.textBoxDiskon.Size = new System.Drawing.Size(41, 23);
            this.textBoxDiskon.TabIndex = 14;
            this.textBoxDiskon.TabStop = false;
            this.textBoxDiskon.Text = "0";
            this.textBoxDiskon.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTip1.SetToolTip(this.textBoxDiskon, "Diskon");
            // 
            // textBoXHarga
            // 
            this.textBoXHarga.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoXHarga.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoXHarga.Location = new System.Drawing.Point(25, 198);
            this.textBoXHarga.Name = "textBoXHarga";
            this.textBoXHarga.Size = new System.Drawing.Size(156, 23);
            this.textBoXHarga.TabIndex = 3;
            // 
            // textBoxNamaBarang
            // 
            this.textBoxNamaBarang.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxNamaBarang.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxNamaBarang.Location = new System.Drawing.Point(25, 35);
            this.textBoxNamaBarang.Name = "textBoxNamaBarang";
            this.textBoxNamaBarang.Size = new System.Drawing.Size(228, 23);
            this.textBoxNamaBarang.TabIndex = 0;
            this.textBoxNamaBarang.Leave += new System.EventHandler(this.textBoxNamaBarang_Leave);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(22, 236);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 13);
            this.label10.TabIndex = 10;
            this.label10.Text = "RAK";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(286, 72);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 13);
            this.label9.TabIndex = 9;
            this.label9.Text = "PENERBIT";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(286, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "KATEGORI";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(286, 236);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "BARCODE";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(185, 183);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "DISKON (%)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(286, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "PENULIS";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 291);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "JUMLAH STOCK";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(286, 181);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "ISBN";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 182);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "HARGA JUAL";
            // 
            // labelBarang
            // 
            this.labelBarang.AutoSize = true;
            this.labelBarang.Location = new System.Drawing.Point(22, 19);
            this.labelBarang.Name = "labelBarang";
            this.labelBarang.Size = new System.Drawing.Size(86, 13);
            this.labelBarang.TabIndex = 0;
            this.labelBarang.Text = "NAMA BARANG";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.buttonSaveData);
            this.flowLayoutPanel1.Controls.Add(this.buttonCancel);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 383);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(571, 34);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // buttonSaveData
            // 
            this.buttonSaveData.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSaveData.Location = new System.Drawing.Point(25, 3);
            this.buttonSaveData.Margin = new System.Windows.Forms.Padding(25, 3, 3, 3);
            this.buttonSaveData.Name = "buttonSaveData";
            this.buttonSaveData.Size = new System.Drawing.Size(75, 32);
            this.buttonSaveData.TabIndex = 12;
            this.buttonSaveData.Text = "SIMPAN";
            this.toolTip1.SetToolTip(this.buttonSaveData, "SIMPAN DATA");
            this.buttonSaveData.UseVisualStyleBackColor = true;
            this.buttonSaveData.Click += new System.EventHandler(this.buttonSaveData_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancel.Location = new System.Drawing.Point(106, 3);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 32);
            this.buttonCancel.TabIndex = 13;
            this.buttonCancel.Text = "BATAL";
            this.toolTip1.SetToolTip(this.buttonCancel, "KELUAR");
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(25, 127);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 13);
            this.label6.TabIndex = 33;
            this.label6.Text = "HARGA BELI";
            // 
            // textBoxHargaBeli
            // 
            this.textBoxHargaBeli.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxHargaBeli.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxHargaBeli.Location = new System.Drawing.Point(25, 143);
            this.textBoxHargaBeli.Name = "textBoxHargaBeli";
            this.textBoxHargaBeli.Size = new System.Drawing.Size(228, 23);
            this.textBoxHargaBeli.TabIndex = 2;
            // 
            // FormInputDataBarang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 420);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormInputDataBarang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TAMBAH DATA BARANG";
            this.Load += new System.EventHandler(this.FormDataBarang_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericStock)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelBarang;
        private System.Windows.Forms.TextBox textBoXHarga;
        private System.Windows.Forms.TextBox textBoxNamaBarang;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxDiskon;
        private System.Windows.Forms.ComboBox comboBoxPenerbit;
        private System.Windows.Forms.ComboBox comboBoxKategori;
        private System.Windows.Forms.ComboBox comboBoxRak;
        private System.Windows.Forms.TextBox textBoxBarCode;
        private System.Windows.Forms.TextBox textBoxISBN;
        private System.Windows.Forms.TextBox textBoxPenulis;
        private System.Windows.Forms.Button buttonTambahRak;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button buttonTambahPenerbit;
        private System.Windows.Forms.Button buttonTambahKategori;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.NumericUpDown numericStock;
        private System.Windows.Forms.RichTextBox richTextBoxKeterangan;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button buttonGenerateBarCode;
        private System.Windows.Forms.Button buttonGenerateKode;
        private System.Windows.Forms.TextBox textBoxKode;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button buttonSaveData;
        private System.Windows.Forms.TextBox textBoxHargaBeli;
        private System.Windows.Forms.Label label6;
    }
}