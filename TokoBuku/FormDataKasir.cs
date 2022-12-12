using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TokoBuku
{
    public partial class FormDataKasir : Form
    {
        public FormDataKasir()
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
                string message = "DATA TERSIMPAN\n" +
                    $"NAMA PETUGAS KASIR: {textBoxNama.Text}\n" +
                    $"USERNAME: {textBoxUserName.Text}";
                ShowSaved(message);
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
