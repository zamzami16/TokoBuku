using System;
using System.Windows.Forms;
using TokoBuku.BaseForm.TipeData.DataBase;

namespace TokoBuku.BaseForm.Master.Input
{
    public partial class FormInputDataKas : Form
    {
        public TKas Kas { get; set; }

        public FormInputDataKas() { InitializeComponent(); }

        private void buttonCancel_Click(object sender, EventArgs e) { this.Close(); }

        private void buttonSave_Click(object sender, EventArgs e) { SaveData(); }

        private void SaveData()
        {
            double saldo;
            if (string.IsNullOrWhiteSpace(textBoxNama.Text))
            {
                MessageBox.Show("KOLOM NAMA TIDAK BOLEH KOSONG", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.ActiveControl = this.textBoxNama;
            }
            else if (!double.TryParse(this.textBoxSaldo.Text, out saldo))
            {
                MessageBox.Show("Cek kolom saldo terlebih dahulu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.ActiveControl = this.textBoxSaldo;
            }
            else
            {
                this.DialogResult = DialogResult.OK;

                this.Kas.Nama = textBoxNama.Text;
                this.Kas.Saldo = saldo;
                this.Kas.Keterangan = richTextBoxKeterangan.Text;
                this.Close();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.ActiveControl = this.textBoxSaldo;
            }
        }

        private void FormDataRakKasKategoriPenerbitMaster_Load(object sender, EventArgs e) { this.Kas = new TKas(); }

        private void textBoxSaldo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.ActiveControl = this.buttonSave;
                SaveData();
            }
        }
    }
}
