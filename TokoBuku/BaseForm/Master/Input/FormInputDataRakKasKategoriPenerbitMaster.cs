using System;
using System.Windows.Forms;

namespace TokoBuku.BaseForm.Master.Input
{
    public partial class FormInputDataRakKasKategoriPenerbitMaster : Form
    {
        //private bool isEdit = true;
        //private string Type_OF = "kas";
        //private string strConn = @"ServerType=1;User=SYSDBA;Password=masterkey;Dialect=3;Database=C:\Users\yusuf\OneDrive\Desktop\Axata\DB\TOKOBUKU.FDB";

        public enum tipeForm
        {
            Kas,
            Kategori,
            Penerbit,
            Rak
        }

        public string ValueName { get; set; }
        public string ValueKeterangan { get; set; }
        public tipeForm type_of { get; set; }
        public tipeForm type_of_kas { get; set; }
        public tipeForm type_of_kategori { get; set; }
        public tipeForm type_of_penerbit { get; set; }
        public tipeForm type_of_rak { get; set; }

        public FormInputDataRakKasKategoriPenerbitMaster()
        {
            InitializeComponent();
            type_of_kas = tipeForm.Kas;
            type_of_kategori = tipeForm.Kategori;
            type_of_penerbit = tipeForm.Penerbit;
            type_of_rak = tipeForm.Rak;
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
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("KOLOM NAMA TIDAK BOLEH KOSONG", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.ValueName = textBox1.Text;
                this.ValueKeterangan = richTextBoxKeterangan.Text;
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

        private void FormDataRakKasKategoriPenerbitMaster_Load(object sender, EventArgs e)
        {
            switch (type_of)
            {
                case tipeForm.Kas:
                    this.Text = "DATA KAS";
                    this.labelTitle.Text = "DATA KAS";
                    break;
                case tipeForm.Kategori:
                    this.Text = "DATA KATEGORI";
                    this.labelTitle.Text = "DATA KATEGORI BUKU";
                    break;
                case tipeForm.Penerbit:
                    this.Text = "DATA PENERBIT";
                    this.labelTitle.Text = "DATA PENERBIT BUKU";
                    break;
                case tipeForm.Rak:
                    this.Text = "DATA RAK";
                    this.labelTitle.Text = "DATA RAK BUKU";
                    break;
                default:
                    break;
            }
        }
    }
}
