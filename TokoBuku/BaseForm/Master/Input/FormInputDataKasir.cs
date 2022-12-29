using System;
using System.Windows.Forms;

namespace TokoBuku.BaseForm.Master.Input
{
    public partial class FormInputDataKasir : Form
    {
        public string Namavalue { get; set; }
        public string UserNameValue { get; set; }
        public string PasswordValue { get; set; }
        public string KeteranganValue { get; set; }
        public string AlamatValue { get; set; }
        public string NoHpValue { get; set; }
        public string StatusValue { get; set; }
        public FormInputDataKasir()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxNama.Text))
            {
                ShowErrorPrompt("KOLOM NAMA TIDAK BOLEH KOSONG");
            }
            else if (string.IsNullOrWhiteSpace(textBoxUserName.Text))
            {
                ShowErrorPrompt("KOLOM USERNAME TIDAK BOLEH KOSONG");
            }
            else if (string.IsNullOrWhiteSpace(textBoxPassword.Text))
            {
                ShowErrorPrompt("KOLOM PASSWORD TIDAK BOLEH KOSONG");
            }
            else if (string.IsNullOrWhiteSpace(textBoxAlamat.Text))
            {
                ShowErrorPrompt("KOLOM ALAMAT TIDAK BOLEH KOSONG");
            }
            else if (string.IsNullOrWhiteSpace(maskedTextBoxNoHP.Text))
            {
                ShowErrorPrompt("KOLOM NO. HP TIDAK BOLEH KOSONG");
            }
            else if (string.IsNullOrWhiteSpace(comboBoxStatus.Text))
            {
                comboBoxStatus.Text = "AKTIF";
            }
            else
            {
                this.Namavalue = this.textBoxNama.Text;
                this.UserNameValue = this.textBoxUserName.Text;
                this.PasswordValue = this.textBoxPassword.Text;
                this.AlamatValue = this.textBoxAlamat.Text;
                this.NoHpValue = this.maskedTextBoxNoHP.Text;
                this.KeteranganValue = this.richTextBox1.Text;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void ShowErrorPrompt(string message)
        {
            MessageBox.Show(message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void ShowSaved(string message)
        {
            MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
