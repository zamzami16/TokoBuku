﻿namespace TokoBuku.BaseForm.EditForm
{
    partial class FormEditKategori
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textNamaAwal = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxNamaGanti = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
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
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(346, 229);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.Location = new System.Drawing.Point(3, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(340, 40);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "TITLE";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textNamaAwal);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textBoxNamaGanti);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 43);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(340, 133);
            this.panel1.TabIndex = 1;
            // 
            // textNamaAwal
            // 
            this.textNamaAwal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textNamaAwal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textNamaAwal.Location = new System.Drawing.Point(48, 30);
            this.textNamaAwal.Name = "textNamaAwal";
            this.textNamaAwal.Size = new System.Drawing.Size(241, 23);
            this.textNamaAwal.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "DIGANTI DENGAN";
            this.label2.Visible = false;
            // 
            // textBoxNamaGanti
            // 
            this.textBoxNamaGanti.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxNamaGanti.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxNamaGanti.Location = new System.Drawing.Point(48, 87);
            this.textBoxNamaGanti.Name = "textBoxNamaGanti";
            this.textBoxNamaGanti.Size = new System.Drawing.Size(241, 23);
            this.textBoxNamaGanti.TabIndex = 1;
            this.textBoxNamaGanti.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "NAMA AWAL";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.buttonSave);
            this.flowLayoutPanel1.Controls.Add(this.buttonCancel);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 182);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(340, 44);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // buttonSave
            // 
            this.buttonSave.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonSave.Location = new System.Drawing.Point(48, 5);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(48, 5, 3, 3);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Padding = new System.Windows.Forms.Padding(3);
            this.buttonSave.Size = new System.Drawing.Size(75, 32);
            this.buttonSave.TabIndex = 0;
            this.buttonSave.Text = "SIMPAN";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonCancel.Location = new System.Drawing.Point(136, 5);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(10, 5, 3, 3);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Padding = new System.Windows.Forms.Padding(3);
            this.buttonCancel.Size = new System.Drawing.Size(75, 32);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "BATAL";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // FormEditDatasKategori
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 229);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormEditDatasKategori";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DATA MASTER";
            this.Load += new System.EventHandler(this.FormEditDatasKategori_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxNamaGanti;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox textNamaAwal;
    }
}