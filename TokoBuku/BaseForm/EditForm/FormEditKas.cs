using System;
using System.Windows.Forms;
using TokoBuku.BaseForm.TipeData.DataBase;

namespace TokoBuku.BaseForm.EditForm
{
    public partial class FormEditKas : Form
    {
        public TKas Kas { get; set; }

        public FormEditKas()  { InitializeComponent(); }

        public FormEditKas(TKas kas)
        {
            InitializeComponent();
            this.Kas = kas;
        }

        private void buttonCancel_Click(object sender, EventArgs e){this.Close();}

        private void buttonSave_Click(object sender, EventArgs e){SaveData();}

        private void SaveData()
        {
            double nominal;
            if (string.IsNullOrWhiteSpace(textNominal.Text))
            {
                MessageBox.Show("KOLOM NAMA KAS TIDAK BOLEH KOSONG", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.ActiveControl = this.textNama;
            }
            else if (!double.TryParse(this.textNominal.Text, out nominal))
            {
                MessageBox.Show("CEK NOMINAL KAS. HANYA BOLEH BERUPA ANGKA SAJA.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.ActiveControl = this.textNominal;
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Kas.Nama = this.textNama.Text;
                this.Kas.Saldo = nominal;
                this.Kas.Keterangan = this.richTextBoxKeterangan.Text;
                this.Close();
            }
        }

        private void FormEditDatasKategori_Load(object sender, EventArgs e)
        {
            this.textNama.Text = this.Kas.Nama;
            this.textNominal.Text = this.Kas.Saldo.ToString();
            this.richTextBoxKeterangan.Text = this.Kas.Keterangan;
        }

        private void textNominal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.ActiveControl = this.buttonSave;
                this.SaveData();
            }
        }
    }
}
