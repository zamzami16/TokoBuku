using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TokoBuku.BaseForm.EditForm
{
    public partial class FormEditKategori : Form
    {
        //private bool isEdit = true;
        //private string Type_OF = "kas";
        //private string strConn = @"ServerType=1;User=SYSDBA;Password=masterkey;Dialect=3;Database=C:\Users\yusuf\OneDrive\Desktop\Axata\DB\TOKOBUKU.FDB";


        public string ChangedName { get; set; }
        public string Keterangan { get; set; }
        public string OriginalName { get; set; }
        public string FormName { get; set; }
        public string FormTitle { get; set; }

        public FormEditKategori(string OriginalName, string keterangan="")
        {
            InitializeComponent();
            this.OriginalName = OriginalName;
            this.Keterangan = keterangan;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void SaveData()
        {
            if (string.IsNullOrWhiteSpace(textBoxNamaGanti.Text))
            {
                MessageBox.Show("KOLOM NAMA PENGGANTI TIDAK BOLEH KOSONG", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.ChangedName = textBoxNamaGanti.Text;
                this.Close();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.ActiveControl = this.buttonSave;
                SaveData();
            }
        }

        private void FormEditDatasKategori_Load(object sender, EventArgs e)
        {
            this.textNamaAwal.Text = this.OriginalName;
            this.textNamaAwal.Enabled = false;
            this.textBoxNamaGanti.Text = this.OriginalName;
            this.Text = this.FormName;
            this.labelTitle.Text = this.FormTitle;
            this.richTextBoxKeterangan.Text = this.Keterangan;
        }
    }
}
